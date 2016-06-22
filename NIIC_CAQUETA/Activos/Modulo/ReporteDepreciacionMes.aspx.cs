using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Activos.Clases;
using System.Data;
using System.IO;

namespace Activos.Modulo.Reporte
{
    public partial class ReporteNiif : System.Web.UI.Page
    {
        public GeneradorReportes _reporte = new GeneradorReportes();
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.path2 = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.LastIndexOf("/"));
            consultarDepreciacion();
        }

        #region REPORTE DEPRECIACION MES
        public void consultarDepreciacion() {
            DataTable dt =  _reporte.ConsultarControlDepreciacionMes();
            CDEPRECIACION.GetStore().DataSource = dt;
            CDEPRECIACION.DataBind();

        }

        [DirectMethod(Timeout = 800000)]
        public void Excel(string CODIGO)
        {
            DataTable dt = _reporte.ConsultarDepreciacionMes(CODIGO);

            string id_file = "depmes.txt";
            string FilePath = HttpRuntime.AppDomainAppPath + @"Planos\" + id_file;
            StreamWriter plano = new StreamWriter(FilePath);

            try
            {

                if (dt.Rows.Count > 0)
                {
                    string encabezado = "SUBCLASE;PLACA;FECHA_REVISION;NO_COMPRA;NOMBRE_COMPONENTE;NOMBRE _NORMA;TIPO_DEPRECIACION;PORCENTAJE;VIDA_UTIL;UNIDAD_DEP;VIDA_UTIL_UTILIZADO;VIDA_UTIL_REMANENTE;COSTO_INICIAL;AJUSTE_RESIDUAL;AJUSTE_DETERIORO;AJUSTE_RAZONABLE;DEP_MES;DEP_ACUMULADA;BASE_DEPRECIABLE;IMPORTE_LIBROS";
                    plano.WriteLine(encabezado);

                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        string linea = string.Empty;

                        for (int k = 0; k < dt.Columns.Count; k++)
                        {
                            linea += dt.Rows[j][k].ToString() + ";";
                        }
                        linea = linea.TrimEnd(';');
                        plano.WriteLine(linea);
                    }

                    plano.Close();

                    X.AddScript("descargaCSV('../Planos/depmes.txt');");
                }
                else
                {
                    throw new Exception("No existe datos para generar.");
                }

            }
            catch (Exception)
            {

                Response.End();
            }

        }

        #endregion

        #region Reportes Normas Internacionales 

        /// <summary>
        /// REPORTE DE TRAZABILIDAD DETERIORO POR NIIF
        /// </summary>

        [DirectMethod(ShowMask = true, Msg = "Creando Informe HitoricoDeterioro en PDF ...", Target = MaskTarget.Page)]
        public void Informe_HistoricoDeterioro_RptDetalle(string codActivo, string placa, string nombreActivo)
        {
            try
            {

                Conexion_Mysql connect = new Conexion_Mysql();
                ReportePDF pdf = new ReportePDF("rptDetalleHistoricoDeterioro");
                List<string> htm = pdf.Template;

                string cad = "SELECT "
                                + " a_c.id_activo as idactivo, "
                                + " s_c.Componente AS Componente, "
                                + " a_c.costo_inicial, "
                                + " a_c.Porcentaje_ci, "
                                + " d_v.valor AS Vdeterioro, "
                                + " vr.fecha, "
                                + " CONCAT_WS(' ',emp.Nombres, emp.Apellido1,emp.Apellido2) as nombre, "
                                + " t_d.descripcion AS tipo,t_m.Descripcion AS motivo"
                                + " 				FROM detalle_deterioro as d_v "
                                + " 				INNER JOIN deterioro AS vr ON d_v.id_deterioro=vr.id_deterioro  "
                                + " 				INNER JOIN activo_componente AS a_c ON d_v.id_componente=a_c.id_Componente "
                                + " 				INNER JOIN subclase_componente AS s_c ON a_c.sub_compID=s_c.id_componente"
                                + " 				INNER JOIN tb_tipo_deterioro AS t_d ON vr.id_tipo = t_d.id_tipo"
                                + " 				INNER JOIN tb_motivo_deterioro AS t_m ON vr.id_motivo = t_m.id"
                                + " 				INNER JOIN empleado AS emp ON vr.idResponsable = emp.id_empleado"
                                + " 				WHERE a_c.id_activo={0} ORDER BY Componente,vr.fecha,vr.FyH_Real";


                DataSet dt = connect.EjecutarSelectMysql(string.Format(cad, codActivo));

                //** Encabezado
                htm[1] = htm[1].Replace("DETALLE", "DETALLE DETERIORO DEL ACTIVO: " + placa + "-" + nombreActivo);
                htm[1] = string.Format(htm[1], Global.Empresa, Global.Nit, DateTime.Now.ToString("yyyy-MM-dd"));
                cad = pdf.Inicio;
                //**
                int i = 0;
                while (i < dt.Tables[0].Rows.Count)
                {
                    double vr = 0;
                    DataRow[] rows = dt.Tables[0].Select("idactivo='" + dt.Tables[0].Rows[i]["idactivo"] + "'");
                    cad += htm[2];

                    foreach (DataRow item in rows)
                    {
                        cad += string.Format(htm[3], Convert.ToDateTime(item["fecha"]).ToString("yyyy-MM-dd"), 
                                                    item["Componente"], 
                                                    string.Format("{0:n}",
                                                    item["costo_inicial"]), 
                                                    string.Format("{0:n}",
                                                    item["Porcentaje_ci"]) + "%", 
                                                    string.Format("{0:n}", 
                                                    item["Vdeterioro"]),
                                                    item["tipo"], 
                                                    item["motivo"], 
                                                    item["nombre"]
                                            );

                    }

                    cad += string.Format(htm[4], "");
                    i += rows.Length;
                }
                //  cad += string.Format(htm[5]);

                cad += pdf.Fin;

                pdf.createPDF(cad, htm[1], "Detalles_HistoricoDeterioro_", new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 25f, 25f, 20f, 20f));
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
        /// <summary>
        /// REPORTE DE TRAZABILIDAD RESIDUAL
        /// </summary>

        [DirectMethod(ShowMask = true, Msg = "Creando Informe HitoricoResidual en PDF ...", Target = MaskTarget.Page)]
        public void Informe_HistoricoResidual_RptDetalle(string codActivo, string placa, string nombreActivo)
        {
            try
            {

                Conexion_Mysql connect = new Conexion_Mysql();
                ReportePDF pdf = new ReportePDF("rptDetalleHistoricoResidual");
                List<string> htm = pdf.Template;

                string cad = "SELECT s_c.Componente, "
                                + "a_c.costo_inicial, "
                                + "a_c.Porcentaje_ci, "
                                + "d_v.valorResidualAnterior, "
                                + "CONCAT_WS(' ',emp.Nombres,emp.Apellido1,emp.Apellido2) as nombre, "
                                + " d_v.valorResidualActual, "
                                + " vr.fecha, "
                                + " vr.concepto,"
                                + " vr.idactivo "
                                    + " FROM detalle_valorresidual as d_v " 
                                    + " INNER JOIN valorresidual as vr ON d_v.idvalorResidual=vr.id " 
                                    + " INNER JOIN activo_componente as a_c ON d_v.idcomponente=a_c.id_Componente " 
                                    + " INNER JOIN subclase_componente as s_c ON a_c.sub_compID=s_c.id_componente " 
                                    + " INNER JOIN empleado as emp ON emp.id_empleado = vr.idResponsable "
                                    + " WHERE a_c.id_activo={0} ORDER BY Componente,vr.fecha,vr.FyH_Real";

                DataSet dt = connect.EjecutarSelectMysql(string.Format(cad, codActivo));

                //** Encabezado
                htm[1] = htm[1].Replace("DETALLE", "HISTORIAL DEL VALOR RESIDUAL DEL ACTIVO: " + placa + "-" + nombreActivo);
                htm[1] = string.Format(htm[1], Global.Empresa, Global.Nit, DateTime.Now.ToString("yyyy-MM-dd"));
                cad = pdf.Inicio;
                //**
                cad += htm[2];
                int i = 0;
                while (i < dt.Tables[0].Rows.Count)
                {
                    double vr = 0;
                    DataRow[] rows = dt.Tables[0].Select("idactivo='" + dt.Tables[0].Rows[i]["idactivo"] + "'");
                  

                    foreach (DataRow item in rows)
                    {
                        cad += string.Format(htm[3], Convert.ToDateTime(item["fecha"]).ToString("yyyy-MM-dd"), 
                                                     item["Componente"],
                                                     string.Format("{0:n}",
                                                     item["costo_inicial"]),
                                                     string.Format("{0:n}",
                                                     item["Porcentaje_ci"]) + "%", 
                                                     string.Format("{0:n}", 
                                                     item["valorResidualAnterior"]),
                                                     string.Format("{0:n}", 
                                                     item["valorResidualActual"]),
                                                     item["nombre"],
                                                     string.Format("{0:n}",
                                                     item["concepto"])
                                            );

                    }

                  
                    i += rows.Length;
                }
                cad += string.Format(htm[4]);
                //  cad += string.Format(htm[5]);

                cad += pdf.Fin;

                pdf.createPDF(cad, htm[1], "Detalles_HistoricoResidual_", new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 25f, 25f, 20f, 20f));
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

        /// <summary>
        /// REPORTE DE TRAZABILIDAD DEPRECIACION
        /// </summary>

        [DirectMethod(ShowMask = true, Msg = "Creando Informe Depreciación en PDF ...", Target = MaskTarget.Page)]
        public void Informe_Depreciacion_RptDetalle(string codActivo,string placa,string nombreActivo)
        {
            try
            {

                Conexion_Mysql connect = new Conexion_Mysql();
                ReportePDF pdf = new ReportePDF("rptDetalle");
                List<string> htm = pdf.Template;

             

                string cad = "SELECT d.idactivo as idactivo, "
                                    +"ca.costo_inicial as costoinicial,"
                                    +"s_c.Componente as descripcion, "
                                    +"d.fecha, "
                                    +"dd.base, "
                                    +"dd.depreciacion_mes as dep_mes, "
                                    +"dd.unidad_dep as cantdep,"
                                    +"dd.saldo_depreciacion as saldo_dep,"
                                    +"dd.dep_acum as depacumulada, "
                                    +"dd.id_componente, "
                                    + "dd.vr_residual as vresidual,"
                                    + "dd.ajust_dep_acum as ajdepacumulada, "
                                    + "dd.ajust_vr_razonable as ajrazonable, "
                                    + "dd.vr_deterioro as vrdeterioro "
                                    + "   FROM depreciacion d "
                                    + "   INNER JOIN detalle_depreciacion dd ON d.id_depreciacion = dd.id_depreciacion "
                                    + "   INNER JOIN activo_componente ca ON dd.id_componente=ca.id_Componente "
                                    + "   INNER JOIN subclase_componente as s_c ON ca.sub_compID=s_c.id_componente "
                                    + " WHERE d.idactivo = '{0}' "
                                    + " ORDER BY descripcion,d.fecha,d.FyH_Real";


                DataSet dt = connect.EjecutarSelectMysql(string.Format(cad, codActivo));

                //** Encabezado
                htm[1] = htm[1].Replace("DETALLE", "DETALLE DEPRECIACION DEL ACTIVO: " + placa + "-" + nombreActivo);
                htm[1] = string.Format(htm[1], Global.Empresa, Global.Nit, DateTime.Now.ToString("yyyy-MM-dd"));
                cad = pdf.Inicio;
                cad += htm[2];
                //**
                int i = 0;
                while (i < dt.Tables[0].Rows.Count)
                {
                    double vr = 0;
                    DataRow[] rows = dt.Tables[0].Select("idactivo='" + dt.Tables[0].Rows[i]["idactivo"] + "'");
                   

                    foreach (DataRow item in rows)
                    {
                        cad += string.Format(htm[3], Convert.ToDateTime(item["fecha"]).ToString("yyyy-MM-dd"), 
                                                     item["descripcion"], string.Format("{0:n}",
                                                     item["costoinicial"]), string.Format("{0:n}", 
                                                     item["cantdep"]), string.Format("{0:n}",
                                                     item["ajrazonable"]),
                                                     string.Format("{0:n}", item["ajdepacumulada"]),
                                                     string.Format("{0:n}", item["vresidual"]),
                                                     string.Format("{0:n}", item["vrdeterioro"]), 
                                                     string.Format("{0:n}", item["base"]), 
                                                     string.Format("{0:n}", item["dep_mes"]),
                                                     string.Format("{0:n}", 
                                                     item["depacumulada"]), 
                                                     string.Format("{0:n}", 
                                                     item["saldo_dep"])
                                            );
                    }

                   
                    i += rows.Length;
                }
                //  cad += string.Format(htm[5]);
                cad += string.Format(htm[4]);
                cad += pdf.Fin;

                pdf.createPDF(cad, htm[1], "Detalles_Depreciacion_", new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 25f, 25f, 20f, 20f));
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
        /// <summary>
        /// TRAZABILIDAD GENERAL (ALTA- BAJA) 
        /// </summary>
        [DirectMethod(ShowMask = true, Msg = "Creando Informe Trazabilidad en PDF ...", Target = MaskTarget.Page)]
        public void Informe_Trazabilidad_RptDetalle(string codActivo, string placa, string nombreActivo)
        {
            try
            {

                Conexion_Mysql connect = new Conexion_Mysql();
                ReportePDF pdf = new ReportePDF("rptDetalleTrazabilidad");
                List<string> htm = pdf.Template;

                string cad = "SELECT d.idactivo as idactivo,ca.costo_inicial as costoinicial,dd.tipo_doc ,s_c.Componente as descripcion, d.fecha,  dd.base, dd.depreciacion_mes as dep_mes,dd.unidad_dep as cantdep,dd.saldo_depreciacion as saldo_dep,dd.dep_acum as depacumulada, dd.id_componente,  "
                                                                        + "dd.vr_residual as vresidual,dd.ajust_dep_acum as ajdepacumulada,dd.ajust_vr_razonable as ajrazonable,dd.vr_deterioro as vrdeterioro, dd.tipo_doc FROM depreciacion d "
                                                                        + "INNER JOIN detalle_depreciacion dd ON d.id_depreciacion = dd.id_depreciacion "
                                                                        + "INNER JOIN activo_componente ca ON dd.id_componente=ca.id_Componente "
                                                                        + "INNER JOIN subclase_componente as s_c ON ca.sub_compID=s_c.id_componente "
                                                                        + "WHERE d.idactivo = '{0}'  ORDER BY descripcion,d.fecha,d.FyH_Real";


                DataSet dt = connect.EjecutarSelectMysql(string.Format(cad, codActivo));

                //** Encabezado
                htm[1] = htm[1].Replace("DETALLE", "DETALLE TRAZABILIDAD DEL ACTIVO: " + placa + "-" + nombreActivo);
                htm[1] = string.Format(htm[1], Global.Empresa, Global.Nit, DateTime.Now.ToString("yyyy-MM-dd"));
                cad = pdf.Inicio;
                //**
                int i = 0;
                while (i < dt.Tables[0].Rows.Count)
                {
                    double vr = 0;
                    DataRow[] rows = dt.Tables[0].Select("idactivo='" + dt.Tables[0].Rows[i]["idactivo"] + "'");
                    cad += htm[2];

                    foreach (DataRow item in rows)
                    {
                        cad += string.Format(htm[3], Convert.ToDateTime(item["fecha"]).ToString("yyyy-MM-dd"),
                                                    item["tipo_doc"],
                                                    item["descripcion"],
                                                    string.Format("{0:n}",
                                                    item["costoinicial"]), 
                                                    string.Format("{0:n}",
                                                    item["cantdep"]),
                                                    string.Format("{0:n}",
                                                    item["ajrazonable"]),
                                                    string.Format("{0:n}",
                                                    item["ajdepacumulada"]), 
                                                    string.Format("{0:n}", 
                                                    item["vresidual"]),
                                                    string.Format("{0:n}", 
                                                    item["vrdeterioro"]),
                                                    string.Format("{0:n}",
                                                    item["base"]),
                                                    string.Format("{0:n}", 
                                                    item["dep_mes"]),
                                                    string.Format("{0:n}", 
                                                    item["depacumulada"]), 
                                                    string.Format("{0:n}", 
                                                    item["saldo_dep"])
                                                );

                    }

                    cad += string.Format(htm[4], "");
                    i += rows.Length;
                }
                //  cad += string.Format(htm[5]);

                cad += pdf.Fin;

                pdf.createPDF(cad, htm[1], "Detalles_Trazabilidad_", new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 25f, 25f, 20f, 20f));
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

        #endregion

        #region Reportes Norma local (SOLO VA EL REPORTE DE DEPRECIACION)

        public void TrazabilidadNormaLocal(string idactivo, string Placa, string NombreActivo)
        {
            _reporte.informeNormaTributariaDepreciacion(idactivo, Placa, NombreActivo);
        }
        #endregion
        #region Reportes Norma Internacional (SOLO VA EL REPORTE DE DEPRECIACION)
        [DirectMethod(ShowMask = true, Msg = "Creando Informe Depreciación en PDF ...", Target = MaskTarget.Page)]
        public void TrazabilidadNormaInternacional(string idactivo,string Placa,string NombreActivo) {
            _reporte.informeNormaInternacionalDepreciacion(idactivo, Placa, NombreActivo);
        }
        #endregion

        
    }
}