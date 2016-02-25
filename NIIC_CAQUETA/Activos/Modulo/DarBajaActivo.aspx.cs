using Activos.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;

namespace Activos.Modulo
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.path2 = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.LastIndexOf("/"));
            CargarlistaActivosProducccion();

        }
        [DirectMethod(ShowMask = true, Msg = "Cargando lista Activos..", Target = MaskTarget.Page)]
        public void CargarlistaActivosProducccion() {
            Conexion_Mysql conexion = new Conexion_Mysql();
            string sql = "SELECT "
                            + "    idActivo        AS IDACTIVO, "
                            + "    placa           AS PLACA, "
                            + "    Nombre          AS NOMBREACTIVO, "
                            + "    CostoInicial    AS COSTOINICIAL, "
                            + "    BaseDepreciable AS BASEDEPRECIABLE, "
                            + "    Importe_Libros  AS IMPORTELIBROS "
                            + "FROM "
                            + "    activo "
                            + "WHERE "
                            + "    Fecha_Alta IS NOT NULL "
                            + "AND id_empleado IS NOT NULL "
                            + "AND Fecha_Baja IS  NULL "
                            + "AND Estado ='A'";
            DataSet dt = conexion.EjecutarSelectMysql(sql);
            SACTIVOUT.DataSource = dt;
            SACTIVOUT.DataBind();
        }
        [DirectMethod(ShowMask = true, Msg = "Guardando..", Target = MaskTarget.Page)]
        public void GuardarBaja(Newtonsoft.Json.Linq.JArray recordActivo) {
            Conexion_Mysql conexion = new Conexion_Mysql();
            List<string> sentencias = new List<string>();
            for (int j = 0; j < recordActivo.Count; j++)
            {
                string sql = "UPDATE "
                            + "    activo "
                            + "SET "
                            + "    FyH_Real = now(), "
                            + "    Fecha_Baja = '" + Convert.ToDateTime(DFECHA.Text).ToString("yyyy-MM-dd") + "', "
                            + "    Estado = 'I' "
                            + "WHERE "
                            + "    idActivo = '" + recordActivo[j]["IDACTIVO"] + "'";
                sentencias.Add(sql);
            }
            if (conexion.EjecutarTransaccion(sentencias))
            {
                Notification.Show(new NotificationConfig
                {
                    Title = "Notificación",
                    AlignCfg = new NotificationAlignConfig
                    {
                        ElementAnchor = AnchorPoint.TopRight,
                        TargetAnchor = AnchorPoint.TopRight,
                        OffsetX = -20,
                        OffsetY = 20
                    },
                    ShowFx = new SlideIn { Anchor = AnchorPoint.TopRight, Options = new FxConfig { Easing = Easing.BounceOut } },
                    HideFx = new Ghost { Anchor = AnchorPoint.TopRight },
                    Html = "Bajas Exitosamente."
                });
            }
            else
            {
                Notification.Show(new NotificationConfig
                {
                    Title = "Notificación",
                    AlignCfg = new NotificationAlignConfig
                    {
                        ElementAnchor = AnchorPoint.TopRight,
                        TargetAnchor = AnchorPoint.TopRight,
                        OffsetX = -20,
                        OffsetY = 20
                    },
                    ShowFx = new SlideIn { Anchor = AnchorPoint.TopRight, Options = new FxConfig { Easing = Easing.BounceOut } },
                    HideFx = new Ghost { Anchor = AnchorPoint.TopRight },
                    Html = "Error!, A ocurrido un incoveniente en la transacción.."
                });
            }
        }

        [DirectMethod(ShowMask = true, Msg = "Creando Informe de Baja en PDF ...", Target = MaskTarget.Page)]
        public void GenerarReporteBaja(Newtonsoft.Json.Linq.JArray recordActivo)
        {

            try
            {
                Conexion_Mysql connect = new Conexion_Mysql();
                ReportePDF pdf = new ReportePDF("rptBaja");
                List<string> htm = pdf.Template;

                string cad = "";

                //** Encabezado

                htm[1] = string.Format(htm[1], Global.Empresa, Global.Nit, CTIPOBAJA.Text, "BA001",
                DateTime.Parse(DFECHA.Text).ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));
                cad = pdf.Inicio;
                //**
             
                cad += htm[2];
                int TOTALBASEDEPRECIABLE = 0;
                int TOTALIMPORTELIBROS = 0;
                int i = 1;
                for (int j = 0; j < recordActivo.Count; j++)
                {
                    cad += string.Format(htm[3], j + 1, recordActivo[j]["PLACA"].ToString().ToUpper(),
                                                 recordActivo[j]["NOMBREACTIVO"].ToString().ToUpper(),
                                                 string.Format("{0:n}", recordActivo[j]["COSTOINICIAL"]),
                                                 string.Format("{0:n}", recordActivo[j]["BASEDEPRECIABLE"]),
                                                 string.Format("{0:n}", recordActivo[j]["IMPORTELIBROS"]));
                        TOTALBASEDEPRECIABLE = TOTALBASEDEPRECIABLE + Convert.ToInt32(recordActivo[j]["BASEDEPRECIABLE"].ToString());
                        TOTALIMPORTELIBROS = TOTALIMPORTELIBROS + Convert.ToInt32(recordActivo[j]["IMPORTELIBROS"].ToString());
                }
                cad += string.Format(htm[4], string.Format("{0:n}", TOTALBASEDEPRECIABLE), string.Format("{0:n}", TOTALIMPORTELIBROS));
                cad += htm[5];
                cad += string.Format(htm[6], TOBSERVACION.Text.ToUpper());
                cad += htm[7];
                cad += htm[8];
                cad += pdf.Fin;

                pdf.createPDF(cad, htm[1], "Informe_Baja_", new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 25f, 25f, 20f, 20f));
            }
            catch (Exception exc)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Sigc. #_Error.",
                    Message = "Error: " + exc.Message + exc.StackTrace,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR,
                    Closable = false
                });
            }
        }
       
    }
}