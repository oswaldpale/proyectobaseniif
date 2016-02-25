using Activos.Clases;
using Activos.Entidades;
using Ext.Net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Activos.Modulo
{
    public partial class Revaluacion : System.Web.UI.Page
    {
        private Parametrizacion _doc = new Parametrizacion();
        private ValorAvaluado EntradaActivo = new ValorAvaluado();
        protected void Page_Load(object sender, EventArgs e)
        {
            rangoFechasControl();
        }
          
        [DirectMethod(ShowMask = true, Msg = "Guardando ...", Target = MaskTarget.Page)]
        public void GrabarRevaluacion()
        {
            try
            {
                List<string> sql = new List<string>();
                Conexion_Mysql conexion = new Conexion_Mysql();
                string fechaRevaluacion = Convert.ToDateTime(dfd_fecha.Text).ToString("yyyy-MM-dd");
               
                int Codigo = _doc.AutoGeneradorID(12);
                string Rcontrol = "DT00" + Codigo;

                RevaluacionActivo r = new RevaluacionActivo();
                DataSet dt = conexion.EjecutarSelectMysql("SELECT ajus_vr_razonable,"
                                                        + " base_deprec,vr_importe_libros,costo_inicial,"
                                                        + " id_Componente as id,Porcentaje_ci as porc  FROM activo_componente "
                                                        + " WHERE idtiponorma=1 AND id_activo=" + codigo.Text);


                r.idactivo = codigo.Text;
                //int razonableUnitaria = Convert.ToInt32(txt_avaluo.Text.Replace(".", ""));
                r.RevComponente = new List<RevaluacionComponente>();
                foreach (DataRow row in dt.Tables[0].Rows)
                {
                    RevaluacionComponente Rc = new RevaluacionComponente();
                    Rc.id_componente = Convert.ToString(row["id"]);
                    Rc.idactivo = codigo.Text;
                    Rc.Porcentaje_ci = Convert.ToInt32(row["porc"]);
                    Rc.costo_inicial = Convert.ToInt32(row["costo_inicial"]);
                    //Rc.ajust_vr_razonable = Convert.ToInt32(row["ajus_vr_razonable"]) + (razonableUnitaria * Rc.Porcentaje_ci / 100);
                    //Rc.base_deprec = Convert.ToInt32(row["base_deprec"]) + (razonableUnitaria* Rc.Porcentaje_ci / 100);
                    //Rc.vr_importe_libros = Convert.ToInt32(row["vr_importe_libros"]) + (razonableUnitaria * Rc.Porcentaje_ci / 100);

                    r.RevComponente.Add(Rc);
                }
                r.VrRazonable = Convert.ToInt32(r.RevComponente.Where(item => item.idactivo == r.idactivo).Sum(y => y.ajust_vr_razonable));
                r.BaseDepreciable = Convert.ToInt32(r.RevComponente.Where(item => item.idactivo == r.idactivo).Sum(y => y.base_deprec));
                r.Importe_libros = Convert.ToInt32(r.RevComponente.Where(item => item.idactivo == r.idactivo).Sum(y => y.vr_importe_libros));

                sql.Add("UPDATE activo  SET  vr_razonable='" + r.VrRazonable + "', "
                                        + " BaseDepreciable ='" + r.BaseDepreciable + "',"
                                        + " Importe_libros ='" + r.Importe_libros + "',"
                                        + " Fecha_URevaluacion='" + fechaRevaluacion + "' "
                                        + " WHERE idActivo='" + codigo.Text + "'");

                sql.Add(
                string.Format(" INSERT INTO "
                                    + " revaluacion "
                                    + "( "
                                    + "idactivo,fecha, "
                                    + "idempleado,concepto, "
                                    + " vr_revaluacion,BaseDepreciable,ImporteLibros,FyH_Real,login "
                                    + ") VALUES "
                                    + "('" + codigo.Text + "',"
                                    + "'" + Convert.ToDateTime(fechaRevaluacion).ToString("yyyy-MM-dd") + "',"
                                    + "'" + idresponsable.Text.ToString() + "',"
                                    + "'" + txt_conceptos.Text.ToString() + "',"
                                    + "'" + r.VrRazonable + "',"
                                    + "'" + r.BaseDepreciable + "',"
                                    + "'" + r.Importe_libros + "',"
                                    + " now(),"
                                    + "'" + HttpContext.Current.User.Identity.Name.ToUpper() + "'"
                                    + ")"));

                foreach (RevaluacionComponente row in r.RevComponente)
                {
                    sql.Add("UPDATE activo_componente  Set "
                                   + " ajus_vr_razonable='" + row.ajust_vr_razonable + "', "
                                   + " base_deprec ='" + row.base_deprec + "', "
                                   + " vr_importe_libros = '" + row.vr_importe_libros + "' " 
                                   + "WHERE id_Componente='" + row.id_componente + "'");

                    sql.Add(
                    string.Format(" INSERT INTO "
                                        + " detalle_revaluacion "
                                        + "( "
                                        + " idrevaluacion, "
                                        + " idcomponente, "
                                        + " vr_revaluacion, "
                                        + " BaseDepreciable, "
                                        + " ImporteLibros,"
                                        + " FyH_Real,login "
                                        + ") "
                                        + "SELECT "
                                        + " ( "
                                        + " SELECT MAX(idrevaluacion) "
                                        + " FROM revaluacion), "
                                        + "'" + row.id_componente + "',"
                                        + "'" + Convert.ToInt32(row.base_deprec) + "',"
                                        + "'" + Convert.ToInt32(row.vr_importe_libros) + "',"
                                        + " now(),"
                                        + "'" + HttpContext.Current.User.Identity.Name.ToUpper() + "'"
                       ));
                }
                conexion.EjecutarTransaccion(sql);
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Registro Exitoso.",
                    Message = "Revaluacion Realizada ",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.INFO,
                    Closable = false
                });
            }
            catch (Exception s)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Sigc.",
                    Message = "Error: " + s.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR,
                    Closable = false
                });
            }
        }

        private void rangoFechasControl()
        {
            string FechaRegistroUDocumento = _doc.ConsultarFechaDepreciacionMin();

            if (FechaRegistroUDocumento != "")
            {
                DateTime FechaTemp = Convert.ToDateTime(DateTime.Parse(FechaRegistroUDocumento).ToString("yyyy-MM-dd")).AddDays(1);
                dfd_fecha.MinDate = FechaTemp;
                // Los rangos de fecha se dan deacuerdo a la depreciacion actual.
                DateTime fecha1 = new DateTime(FechaTemp.Year, FechaTemp.Month, 1).AddMonths(1).AddDays(-1);
                dfd_fecha.MaxDate = fecha1;
            }
            else
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Información!.",
                    Message = "No Existe Activos Dado De Alta.",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.INFO,
                    Closable = true
                });
                dfd_fecha.Reset();
            }
        }

        #region SUBIR PLANO
        [DirectMethod(ShowMask = true, Msg = "Cargando")]
        public void CargarArchivo()
        {
            try
            {
                string tpl = "Archivo cargado: {0}";

                if (this.FileUploadField1.HasFile)
                {
                    List<string[]> parsedData = new List<string[]>();
                    List<ValorAvaluado> EntradaActivo = new List<ValorAvaluado>();
                    List<Entrada> RevaluacionEntrada = new List<Entrada>();

                    using (StreamReader readFile = new StreamReader(FileUploadField1.FileContent))
                    {
                        string archivo = readFile.ReadToEnd();
                        string s = txt_separador_f.Value.ToString();
                        if (s.Equals("\\n"))
                        {
                            s = "\n";
                        }
                        string[] filas = archivo.Split(new string[] { s }, StringSplitOptions.RemoveEmptyEntries);
                        if (filas.Length > 0)
                        {

                            AtributosDepreciacion dep = new AtributosDepreciacion();
                            for (int i = 0; i < filas.Length; i++)
                            {
                                ValorAvaluado valor = new ValorAvaluado();
                                string[] columnas = filas[i].Split(new string[] { txt_separador_c.Value.ToString() }, StringSplitOptions.RemoveEmptyEntries);

                                if (columnas.Length < 1)
                                {
                                    throw new Exception("Las columnas del archivo no corresponden a las seleccionadas, por favor revise el archivo e intente de nuevo.");
                                }
                                EntradaActivo.Add(new ValorAvaluado { placa = columnas[0].ToString(), valor = Convert.ToInt32(columnas[1].ToString()) });
                            }

                            Session["EntradaActivos"] = EntradaActivo;
                            VerificarEntradas();
                        }
                        else
                        {
                            throw new Exception("No ha sido posible leer el archivo, revise los separadores.");
                        }

                    }

                    X.Msg.Show(new MessageBoxConfig
                    {
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.INFO,
                        Title = "Archivo cargado con éxito",
                        Message = string.Format(tpl, this.FileUploadField1.PostedFile.FileName)
                    });
                }
                else
                {
                    X.Msg.Show(new MessageBoxConfig
                    {
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.ERROR,
                        Title = "Error",
                        Message = "No ha sido posible leer el archivo, revise los separadores."
                    });
                }
            }
            catch (Exception exc)
            {
                X.Msg.Show(new MessageBoxConfig
                {
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR,
                    Title = "Error",
                    Message = exc.Message
                });
            }
        }

        public void VerificarEntradas()
        {
            List<ValorAvaluado> EntradaActivos = (List<ValorAvaluado>)Session["EntradaActivos"];
            List<Entrada> RevaluacionEntrada = (List<Entrada>)Session["RevaluacionEntrada"];
            if (RevaluacionEntrada != null)
            {
                foreach (ValorAvaluado item in EntradaActivos)
                {
                    if (RevaluacionEntrada.Where(x => x.placa == item.placa).Count() <= 0)
                    {
                        DataSet dt = _doc.ConsultarDatosActivoRevaluacion(item.placa);
                        foreach (DataRow activo in dt.Tables[0].Rows)
                        {
                            RevaluacionEntrada.Add(
                                new Entrada
                                {
                                    id = activo["idActivo"].ToString(),
                                    valor = item.valor,
                                    placa = item.placa,
                                    nombre = activo["Nombre"].ToString(),
                                    revaluacion = Convert.ToInt32(activo["vr_razonable"].ToString()) + item.valor,
                                    basedepreciable = Convert.ToInt32(activo["BaseDepreciable"].ToString()) + item.valor,
                                    importelibros = Convert.ToInt32(activo["Importe_libros"].ToString()) + item.valor,
                                });
                        }
                    }
                }

            }
            else
            {
                List<Entrada> Revaluacion = new List<Entrada>();
                foreach (ValorAvaluado item in EntradaActivos)
                {
                    DataSet dt = _doc.ConsultarDatosActivoDeterioro(item.placa);
                    foreach (DataRow activo in dt.Tables[0].Rows)
                    {
                        Revaluacion.Add(
                            new Entrada
                            {
                                id = activo["idActivo"].ToString(),
                                valor = item.valor,
                                placa = item.placa,
                                nombre = activo["Nombre"].ToString(),
                                revaluacion = Convert.ToInt32(activo["vr_razonable"].ToString()) + item.valor,
                                basedepreciable = Convert.ToInt32(activo["BaseDepreciable"].ToString()) + item.valor,
                                importelibros = Convert.ToInt32(activo["Importe_libros"].ToString()) + item.valor,
                            });
                    }
                }
                RevaluacionEntrada = Revaluacion;
            }
            Session["RevaluacionEntrada"] = (List<Entrada>)RevaluacionEntrada;
            RefreshData();
        }
        [DirectMethod]
        public void RefreshData()
        {
            List<Entrada> RevaluacionEntrada = (List<Entrada>)Session["RevaluacionEntrada"];
            GREVALUACION.GetStore().DataSource = RevaluacionEntrada.ToList();
            GREVALUACION.GetStore().DataBind();
            GREVALUACION.GetStore().CommitChanges();
        }
        [DirectMethod]
        public void QuitarItems(string idactivo)
        {
            List<Entrada> RevaluacionEntrada = (List<Entrada>)Session["RevaluacionEntrada"];
            List<Entrada> ItemsEliminar = RevaluacionEntrada.Where(x => x.placa == idactivo).ToList();
            foreach (var item in ItemsEliminar)
            {
                RevaluacionEntrada.Remove(item);
            }
            Session["RevaluacionEntrada"] = RevaluacionEntrada;
            RefreshData();
        }
        #endregion
        #region Entidad
        public class Entrada
        {
            public string placa { get; set; }
            public string id { get; set; }
            public string nombre { get; set; }
            public int valor { get; set; }
            public int revaluacion { get; set; }
            public int basedepreciable { get; set; }
            public int importelibros { get; set; }
        }

        #endregion

    }
}