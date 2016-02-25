using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Activos.Clases;
using Ext.Net;
using Activos.Entidades;
using System.IO;

namespace Activos.Modulo
{
    public partial class ValorResidual : SigcWeb.API.Shared.Page
    {
        private Parametrizacion _doc = new Parametrizacion();
        protected override void Page_Load(object sender, EventArgs e)
        {
           // base.Page_Load(sender, e);
            Global.path2 = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.LastIndexOf("/"));
            rangoFechasControl();
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

        [DirectMethod(ShowMask = true, Msg = "Guardando el  Valor Residual...", Target = MaskTarget.Page)]
        public void GrabarValorResidual()
        {
            string fecha_evaluado = Convert.ToDateTime(dfd_fecha.Text).ToString("yyyy-MM-dd");
            Conexion_Mysql conexion = new Conexion_Mysql();
            List<string> sql = new List<string>();
            List<Entrada> ResidualEntrada = (List<Entrada>)Session["DeterioroEntrada"];
            int Codigo = _doc.AutoGeneradorID(14);
            string Rcontrol = "VR00" + Codigo;

            sql.Add(_doc.InsertarDocumentoResidual(Rcontrol, "Residual", fecha_evaluado, txt_conceptos.Text.ToUpper()));

            foreach (Entrada activoResidual in ResidualEntrada)
            {
                DataSet dt = _doc.ConsultarPorcentajeParticipacionComponente(activoResidual.id);

                sql.Add(""
                         + "INSERT "
                         + "INTO "
                         + "    valorresidual "
                         + "    ( "
                         + "        fecha, "
                         + "        idactivo, "
                         + "        ValorResidual,"
                         + "        Ncontrol, "
                         + "        FyH_Real, "
                         + "        login, "
                         + "        IdGrupoAsignacionCentroCosto "
                         + "    ) "
                         + "    SELECT "
                         + "        '" + fecha_evaluado + "', "
                         + "        '" + activoResidual.id + "', "
                         + "        " + activoResidual.residual + ", "
                         + "       '" + Rcontrol + "', "
                         + "        now(), "
                         + "        '" + HttpContext.Current.User.Identity.Name.ToUpper() + "', "
                         + "       (SELECT a.IdgrupoAsignacionCentro from activos_fijos.asignacionactivo a WHERE a.idactivo ='" + activoResidual.id + "' AND a.estado='A')");

                string idcomponente = "";
                foreach (DataRow row in dt.Tables[0].Rows)
                {
                    idcomponente = Convert.ToString(row["id"]);
                    int porcentaje = Convert.ToInt32(row["porc"].ToString());
                    int deterioroComponente = activoResidual.valor;
                    sql.Add(""
                                  + "UPDATE "
                                  + "    activo_componente ac "
                                  + "SET "
                                  + "    ac.ajus_vr_residual = ac.ajus_vr_residual + ((" + activoResidual.valor + " * ac.Porcentaje_ci) / 100), "
                                  + "    ac.vr_importe_libros = ac.vr_importe_libros - ((" + activoResidual.valor + " * ac.Porcentaje_ci) / 100 )  "
                                  + "WHERE "
                                  + "    ac.id_Componente= '" + idcomponente + "'");

                    sql.Add("INSERT "
                                  + "INTO "
                                  + "    detalle_deterioro "
                                  + "    ( "
                                  + "        idResidual, "
                                  + "        idcomponente, "
                                  + "        valorResidual, "
                                  + "        baseDepreciable, "
                                  + "        ImporteLibros, "
                                  + "        login, "
                                  + "        FyH_Real "
                                  + "    ) "
                                  + "    VALUES "
                                  + "    ( "
                                  + "        ( "
                                  + "            SELECT "
                                  + "                MAX(idResidual) "
                                  + "            FROM "
                                  + "                valorresidual "
                                  + "        ) "
                                  + "        , "
                                  + "        '" + idcomponente + "', "
                                  + "        '" + (activoResidual.valor * porcentaje) / 100 + "', "
                                  + "        ( "
                                  + "            SELECT "
                                  + "                base_deprec "
                                  + "            FROM "
                                  + "                activo_componente ac "
                                  + "            WHERE "
                                  + "                ac.id_Componente='" + idcomponente + "' "
                                  + "        ) "
                                  + "        , "
                                  + "        ( "
                                  + "            SELECT "
                                  + "                vr_importe_libros "
                                  + "            FROM "
                                  + "                activo_componente ac "
                                  + "            WHERE "
                                  + "                ac.id_Componente='" + idcomponente + "' "
                                  + "        ) "
                                  + "        , "
                                  + "        '" + HttpContext.Current.User.Identity.Name.ToUpper() + "', "
                                  + "        now() "
                                  + "    )");
                }

                sql.Add(" UPDATE "
                               + "    activo a, "
                               + "    ( "
                               + "        SELECT "
                               + "            ac.id_activo, "
                               + "            SUM(ac.vr_importe_libros)  AS vr_importe_libros, "
                               + "            SUM(ac.base_deprec)        AS base_deprec "
                               + "        FROM "
                               + "            activo_componente ac "
                               + "        WHERE "
                               + "            ac.idtiponorma ='1' "
                               + "        AND ac.id_activo = '" + activoResidual.id + "' "
                               + "        GROUP BY "
                               + "            ac.id_activo "
                               + "    ) "
                               + "    tac "
                               + "SET "
                               + "    a.vr_residual =  a.vr_residual + " + activoResidual.valor + ","
                               + "    a.BaseDepreciable = tac.base_deprec, "
                               + "    a.Importe_libros = tac.vr_importe_libros,"
                               + "    a.Fecha_UDeterioro = '" + fecha_evaluado + "' "
                               + "WHERE "
                               + "    a.idActivo = tac.id_activo");

                sql.Add("  UPDATE "
                            + "    valorresidual d, "
                            + "    ( "
                            + "        SELECT "
                            + "            a.idActivo, "
                            + "            a.BaseDepreciable, "
                            + "            a.Importe_libros "
                            + "        FROM "
                            + "            activo a "
                            + "        WHERE "
                            + "            a.idActivo='" + activoResidual.id + "' "
                            + "    ) "
                            + "    a, "
                            + "    (SELECT MAX(idResidual)AS id_residual FROM valorresidual) det "
                            + "SET "
                            + "    d.BaseDepreciable= a.BaseDepreciable , "
                            + "    d.ImporteLibros= a.Importe_libros "
                            + "WHERE "
                            + "   d.idResidual = det.id_residual");

            }
            sql.Add("update registro_control set Numero=" + (Codigo + 1) + " where Codigo=14");
            conexion.EjecutarTransaccion(sql);
                   dfd_fecha.Reset();
                   txt_conceptos.Reset();
        }
        private Boolean ConsultarSiexisteIDResidual()
        {   
            Conexion_Mysql conexion= new Conexion_Mysql();
            DataSet dt= conexion.EjecutarSelectMysql( string.Format("SELECT id as codigo FROM valorresidual WHERE idactivo='{0}'",codigo.Text));

            if (dt.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            else return true;
        }
        public double valorasignado(double valor, double porcentaje)
        {
            return ((valor * porcentaje) / 100);
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
                    List<Entrada> ResidualEntrada = new List<Entrada>();

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
            List<Entrada> ResidualEntrada = (List<Entrada>)Session["ResidualEntrada"];
            if (ResidualEntrada != null)
            {
                foreach (ValorAvaluado item in EntradaActivos)
                {
                    if (ResidualEntrada.Where(x => x.placa == item.placa).Count() <= 0)
                    {
                        DataSet dt = _doc.ConsultarDatosActivoValorResidual(item.placa);
                        foreach (DataRow activo in dt.Tables[0].Rows)
                        {
                            ResidualEntrada.Add(
                                new Entrada
                                {
                                    id = activo["idActivo"].ToString(),
                                    valor = item.valor,
                                    placa = item.placa,
                                    nombre = activo["Nombre"].ToString(),
                                    residual = Convert.ToInt32(activo["vr_residual"].ToString()) + item.valor,
                                    basedepreciable = Convert.ToInt32(activo["BaseDepreciable"].ToString()) + item.valor,
                                    importelibros = Convert.ToInt32(activo["Importe_libros"].ToString()) + item.valor,
                                });
                        }
                    }
                }

            }
            else
            {
                List<Entrada> Residual = new List<Entrada>();
                foreach (ValorAvaluado item in EntradaActivos)
                {
                    DataSet dt = _doc.ConsultarDatosActivoValorResidual(item.placa);
                    foreach (DataRow activo in dt.Tables[0].Rows)
                    {
                        Residual.Add(
                            new Entrada
                            {
                                id = activo["idActivo"].ToString(),
                                valor = item.valor,
                                placa = item.placa,
                                nombre = activo["Nombre"].ToString(),
                                residual = Convert.ToInt32(activo["vr_residual"].ToString()) + item.valor,
                                basedepreciable = Convert.ToInt32(activo["BaseDepreciable"].ToString()) + item.valor,
                                importelibros = Convert.ToInt32(activo["Importe_libros"].ToString()) + item.valor,
                            });
                    }
                }
                ResidualEntrada = Residual;
            }
            Session["ResidualEntrada"] = (List<Entrada>)ResidualEntrada;
            RefreshData();
        }
        [DirectMethod]
        public void RefreshData()
        {
            List<Entrada> ResidualEntrada = (List<Entrada>)Session["ResidualEntrada"];
            GRESIDUAL.GetStore().DataSource = ResidualEntrada.ToList();
            GRESIDUAL.GetStore().DataBind();
            GRESIDUAL.GetStore().CommitChanges();
        }
        [DirectMethod]
        public void QuitarItems(string idactivo)
        {
            List<Entrada> ResidualEntrada = (List<Entrada>)Session["ResidualEntrada"];
            List<Entrada> ItemsEliminar = ResidualEntrada.Where(x => x.placa == idactivo).ToList();
            foreach (var item in ItemsEliminar)
            {
                ResidualEntrada.Remove(item);
            }
            Session["ResidualEntrada"] = ResidualEntrada;
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
            public int residual { get; set; }
            public int basedepreciable { get; set; }
            public int importelibros { get; set; }
        }

        #endregion


    }
}