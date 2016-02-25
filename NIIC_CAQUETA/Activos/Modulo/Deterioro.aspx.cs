using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Activos.Clases;
using Ext.Net;
using System.Globalization;
using System.IO;
using Activos.Entidades;

namespace Activos.Modulo
{
    public partial class Deterioro : System.Web.UI.Page
    {
        private Parametrizacion doc = new Parametrizacion();
        private ValorAvaluado EntradaActivo = new ValorAvaluado();
      
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.path2 = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.LastIndexOf("/"));
            Conexion_Mysql conexion = new Conexion_Mysql();
          
            DataSet dt = conexion.EjecutarSelectMysql("SELECT id,descripcion FROM tb_motivo_deterioro");
            cbx_motivo.GetStore().DataSource = dt;
            dt = conexion.EjecutarSelectMysql("SELECT id_tipo, descripcion FROM tb_tipo_deterioro ");
            cbx_tipo.GetStore().DataSource = dt;
            rangoFechasDeterioro();
        
          
        }
       
       

        private void rangoFechasDeterioro()
        {
            string FechaRegistroUDocumento = doc.ConsultarFechaDepreciacionMin();

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
        protected void TriggerField1_Click(object sender, DirectEventArgs e)
        {

        }
        [DirectMethod(ShowMask = true, Msg = "Consultando...", Target = MaskTarget.Page)]
        public void ConsultarNoIngreso()
        {
            SRECUPERACION.DataSource = doc.ConsultarNoIngresoDeterioro();
            SRECUPERACION.DataBind();
        }
        public void ConsultarActivosAsociadosNoingreso(string IdControl) { 
            
        }
        protected void GRRECUPERACIONDETERIORO(object sender, DirectEventArgs e)
        {
            TRECUPERARASIGNACION.Text = e.ExtraParams["HNcontrol"].ToString();
            dfd_fecha.Text = e.ExtraParams["HFecha"].ToString();
            cbx_tipo.SetValue(e.ExtraParams["HTipoDeterioro"].ToString());
            cbx_motivo.SetValue(e.ExtraParams["HMotivoDeterioro"].ToString());
            txt_conceptos.Text = e.ExtraParams["HObservacion"].ToString();
            
        }
           

        [DirectMethod(ShowMask = true, Msg = "Guardando ...", Target = MaskTarget.Page)]
        public void GrabarDeterioro()
        {
            Conexion_Mysql conexion = new Conexion_Mysql();
            List<Entrada> DeterioroEntrada = (List<Entrada>)Session["DeterioroEntrada"];
            List<string> sql = new List<string>();
            string fechaDeterioro = Convert.ToDateTime(dfd_fecha.Text).ToString("yyyy-MM-dd");
            int Codigo = doc.AutoGeneradorID(11);
            string Rcontrol = "DT00" + Codigo;
            sql.Add(doc.InsertarDocumentoDeterioro(Rcontrol, "Deterioro", fechaDeterioro, cbx_tipo.Text, cbx_motivo.Text, txt_conceptos.Text.ToUpper()));
            foreach (Entrada activoDeterioro in DeterioroEntrada)
            {
                DataSet dt = doc.ConsultarPorcentajeParticipacionComponente(activoDeterioro.id);

                sql.Add(""
                         + "INSERT "
                         + "INTO "
                         + "    deterioro "
                         + "    ( "
                         + "        fecha, "
                         + "        idactivo, "
                         + "        Deterioro,"
                         + "        Ncontrol, "
                         + "        FyH_Real, "
                         + "        login, "
                         + "        IdGrupoAsignacionCentroCosto "
                         + "    ) "
                         + "    SELECT "
                         + "        '" + fechaDeterioro + "', "
                         + "        '" + activoDeterioro.id + "', "
                         + "        " + activoDeterioro.deterioro + ", "
                         + "       '" + Rcontrol + "', "
                         + "        now(), "
                         + "        '" + HttpContext.Current.User.Identity.Name.ToUpper() + "', "
                         + "       (SELECT a.IdgrupoAsignacionCentro from activos_fijos.asignacionactivo a WHERE a.idactivo ='" + activoDeterioro.id + "' AND a.estado='A')");
                       
                string idcomponente = "";
                foreach (DataRow row in dt.Tables[0].Rows)
                {
                    idcomponente = Convert.ToString(row["id"]);
                    int porcentaje = Convert.ToInt32(row["porc"].ToString());
                    int deterioroComponente = activoDeterioro.valor;
                    sql.Add(""
                                  + "UPDATE "
                                  + "    activo_componente ac "
                                  + "SET "
                                  + "    ac.ajus_vr_deterioro = ac.ajus_vr_deterioro + ((" + activoDeterioro.valor + " * ac.Porcentaje_ci) / 100), "
                                  + "    ac.base_deprec = ac.base_deprec - ((" + activoDeterioro.valor + " * ac.Porcentaje_ci) / 100 ), "
                                  + "    ac.vr_importe_libros = ac.vr_importe_libros - ((" + activoDeterioro.valor + " * ac.Porcentaje_ci) / 100 )  "
                                  + "WHERE "
                                  + "    ac.id_Componente= '" + idcomponente + "'");

                    sql.Add("INSERT "
                                  + "INTO "
                                  + "    detalle_deterioro "
                                  + "    ( "
                                  + "        id_deterioro, "
                                  + "        id_componente, "
                                  + "        Deterioro, "
                                  + "        BaseDepreciable, "
                                  + "        ImporteLibros, "
                                  + "        login, "
                                  + "        FyH_Real "
                                  + "    ) "
                                  + "    VALUES "
                                  + "    ( "
                                  + "        ( "
                                  + "            SELECT "
                                  + "                MAX(id_deterioro) "
                                  + "            FROM "
                                  + "                deterioro "
                                  + "        ) "
                                  + "        , "
                                  + "        '" + idcomponente + "', "
                                  + "        '" + (activoDeterioro.valor * porcentaje) / 100 + "', "
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
                               + "        AND ac.id_activo = '" + activoDeterioro.id + "' "
                               + "        GROUP BY "
                               + "            ac.id_activo "
                               + "    ) "
                               + "    tac "
                               + "SET "
                               + "    a.vr_deterioro =  a.vr_deterioro + " + activoDeterioro.valor + ","
                               + "    a.BaseDepreciable = tac.base_deprec, "
                               + "    a.Importe_libros = tac.vr_importe_libros,"
                               + "    a.Fecha_UDeterioro = '" + fechaDeterioro + "' "
                               + "WHERE "
                               + "    a.idActivo = tac.id_activo");

                sql.Add("  UPDATE "
                            + "    deterioro d, "
                            + "    ( "
                            + "        SELECT "
                            + "            a.idActivo, "
                            + "            a.BaseDepreciable, "
                            + "            a.Importe_libros "
                            + "        FROM "
                            + "            activo a "
                            + "        WHERE "
                            + "            a.idActivo='" + activoDeterioro.id + "' "  
                            + "    ) "
                            + "    a, "
                            + "    (SELECT MAX(id_deterioro)AS id_deterioro FROM deterioro) det "
                            + "SET "
                            + "    d.BaseDepreciable= a.BaseDepreciable , "
                            + "    d.ImporteLibros= a.Importe_libros "
                            + "WHERE "
                            + "   d.id_deterioro = det.id_deterioro");

            }
            sql.Add("update registro_control set Numero=" + (Codigo + 1) + " where Codigo=11");
            if (conexion.EjecutarTransaccion(sql))
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Notificación",
                    Message = "Guardado Exitosamente.",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.INFO,
                    Closable = false
                });
            }
            else
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Notificación.",
                    Message = "Ha ocurrido un error al guardar.",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR,
                    Closable = false
                });
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
                    List<Entrada> DeterioroEntrada = new List<Entrada>();

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
            List<Entrada> DeterioroEntrada = (List<Entrada>)Session["DeterioroEntrada"];
            if (DeterioroEntrada != null)
            {
                foreach (ValorAvaluado item in EntradaActivos)
                {
                    if (DeterioroEntrada.Where(x => x.placa == item.placa).Count() <= 0)
                    {
                        DataSet dt = doc.ConsultarDatosActivoDeterioro(item.placa);
                        foreach (DataRow activo in dt.Tables[0].Rows)
                        {
                            DeterioroEntrada.Add(
                                new Entrada
                                {
                                    id = activo["idActivo"].ToString(),
                                    valor = item.valor,
                                    placa = item.placa,
                                    nombre = activo["Nombre"].ToString(),
                                    deterioro = Convert.ToInt32(activo["vr_deterioro"].ToString()) + item.valor,
                                    basedepreciable = Convert.ToInt32(activo["BaseDepreciable"].ToString()) - item.valor,
                                    importelibros = Convert.ToInt32(activo["Importe_libros"].ToString()) - item.valor,
                                });
                        }
                    }
                }
            
            }
            else
            {
                List<Entrada> Deterioro = new List<Entrada>();
                foreach (ValorAvaluado item in EntradaActivos)
                {
                    DataSet dt = doc.ConsultarDatosActivoDeterioro(item.placa);
                    foreach (DataRow activo in dt.Tables[0].Rows)
                    {
                        Deterioro.Add(
                            new Entrada
                            {
                                id = activo["idActivo"].ToString(),
                                valor = item.valor,
                                placa = item.placa,
                                nombre = activo["Nombre"].ToString(),
                                deterioro = Convert.ToInt32(activo["vr_deterioro"].ToString()) - item.valor,
                                basedepreciable = Convert.ToInt32(activo["BaseDepreciable"].ToString()) - item.valor,
                                importelibros = Convert.ToInt32(activo["Importe_libros"].ToString()) - item.valor,
                            });
                    }
                }
                DeterioroEntrada = Deterioro;
            }
            Session["DeterioroEntrada"] = (List<Entrada>)DeterioroEntrada;
            RefreshData();
        }
        [DirectMethod]
        public void RefreshData()
        {
            List<Entrada> DeterioroEntrada = (List<Entrada>)Session["DeterioroEntrada"];
            GDETERIORO.GetStore().DataSource = DeterioroEntrada.ToList();
            GDETERIORO.GetStore().DataBind();
            GDETERIORO.GetStore().CommitChanges();
        }
        [DirectMethod]
        public void QuitarItems(string idactivo)
        {
            List<Entrada> DeterioroEntrada = (List<Entrada>)Session["DeterioroEntrada"];
            List<Entrada> ItemsEliminar = DeterioroEntrada.Where(x => x.placa == idactivo).ToList();
            foreach (var item in ItemsEliminar)
            {
                DeterioroEntrada.Remove(item);
            }
            Session["DeterioroEntrada"] = DeterioroEntrada;
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
            public int deterioro { get; set; }
            public int basedepreciable { get; set; }
            public int importelibros { get; set; }
        }

        #endregion


    }
}
