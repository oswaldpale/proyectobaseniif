using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Ext.Net;
using System.Web.UI.WebControls;
using System.IO;
using Newtonsoft.Json;

namespace Activos.Clases
{
    public class GeneradorReportes
    {
        #region Informes
        Conexion_Mysql conexion = new Conexion_Mysql();
        public string ConsultarTrazabilidadReporte(string IdActivo,string TipoNorma) {
            string CadenaNorma = (TipoNorma == "") ? "" : "    AND a.TipoNorma = '" + TipoNorma + "' ";
            string cad = "SELECT "
                           + "    a.*, "
                           + "    sc.Componente, "
                           + "    td.Depreciacion,"
                           + "    ac.costo_inicial, "
                           + "    activo.Nombre "
                           + "FROM "
                           + "    ( "
                           + "        SELECT "
                           + "            d.fecha                         AS FechaRegistro, "
                           + "            'INTERNACIONAL'                 AS TipoNorma, "
                           + "            'DETERIORO'                     AS TipoDocumento, "
                           + "            d.idactivo                      AS IdActivo, "
                           + "            det_d.id_componente             AS IdComponente, "
                           + "            ''                              AS idtipodeprecion, "
                           + "            ''                              AS VidaUtilMes, "
                           + "            ''                              AS VidaUtil, "
                           + "            ''                              AS VidaUtilizada, "
                           + "            ''                              AS VidaRemanente, "
                           + "            ''                              AS VrRevaluacion, "
                           + "            det_d.Deterioro                 AS VrDeterioro, "
                           + "            ''                              AS VrResidual, "
                           + "            ''                              AS DepreciacionMes, "
                           + "            ''                              AS DepreciacionAcumulada, "
                           + "            det_d.BaseDepreciable           AS BaseDepreciable, "
                           + "            det_d.ImporteLibros             AS ImporteLibros "
                           + "        FROM "
                           + "            activos_fijos.detalle_deterioro det_d "
                           + "        INNER JOIN activos_fijos.deterioro d "
                           + "        ON "
                           + "            det_d.id_deterioro= d.id_deterioro "
                           + "        UNION ALL "
                           + "        SELECT "
                           + "            v.fecha                         AS FechaRegistro, "
                           + "            'INTERNACIONAL'                 AS TipoNorma, "
                           + "            'RESIDUAL'                      AS TipoDocumento, "
                           + "            v.idactivo                      AS IdActivo, "
                           + "            det_v.idcomponente              AS IdComponente, "
                           + "            ''                              AS idtipodeprecion, "
                           + "            ''                              AS VidaUtilMes, "
                           + "            ''                              AS VidaUtil, "
                           + "            ''                              AS VidaUtilizada, "
                           + "            ''                              AS VidaRemanente, "
                           + "            ''                              AS VrRevaluacion, "
                           + "            ''                              AS VrDeterioro, "
                           + "            det_v.ajusteValorResidual       AS VrResidual, "
                           + "            ''                              AS DepreciacionMes, "
                           + "            ''                              AS DepreciacionAcumulada, "
                           + "            ''                              AS BaseDepreciable, "
                           + "            ''                              AS ImporteLibros "
                           + "        FROM "
                           + "            activos_fijos.detalle_valorresidual det_v "
                           + "        INNER JOIN activos_fijos.valorresidual v "
                           + "        ON "
                           + "            det_v.idResidual = v.idResidual "
                           + "        UNION ALL "
                           + "        SELECT "
                           + "            r.fecha                          AS FechaRegistro, "
                           + "            'INTERNACIONAL'                  AS TipoNorma, "
                           + "            'REVALUACION'                    AS TipoDocumento, "
                           + "            r.idactivo                       AS IdActivo, "
                           + "            det_r.idcomponente               AS IdComponente, "
                           + "            ''                               AS idtipodeprecion, "
                           + "            ''                               AS VidaUtilMes, "
                           + "            ''                               AS VidaUtil, "
                           + "            ''                               AS VidaUtilizada, "
                           + "            ''                               AS VidaRemanente, "
                           + "            det_r.vr_revaluacion             AS VrRevaluacion, "
                           + "            ''                               AS VrDeterioro, "
                           + "            ''                               AS VrResidual, "
                           + "            ''                               AS DepreciacionMes, "
                           + "            ''                               AS DepreciacionAcumulada, "
                           + "            ''                               AS BaseDepreciable, "
                           + "            ''                               AS ImporteLibros "
                           + "        FROM "
                           + "            activos_fijos.detalle_revaluacion det_r "
                           + "        INNER JOIN activos_fijos.revaluacion r "
                           + "        ON "
                           + "            det_r.idrevaluacion = r.idrevaluacion "
                           + "        UNION ALL "
                           + "        SELECT "
                           + "            det_d.fecha                     AS FechaRegistro, "
                           + "            n.Norma                         AS TipoNorma, "
                           + "            'DEPRECIACIÓN'                  AS TipoDocumento, "
                           + "            det_d.idactivo                  AS IdActivo, "
                           + "            det_d.id_componente             AS IdComponente, "
                           + "            det_d.idtipodepreciacion        AS idtipodeprecion, "
                           + "            det_d.unidad_dep                AS VidaUtilMes, "
                           + "            det_d.vidaUtil                  AS VidaUtil, "
                           + "            det_d.vida_utilizada            AS VidaUtilizada, "
                           + "            det_d.vida_remanente            AS VidaRemanente, "
                           + "            det_d.ajust_vr_razonable        AS VrRevaluacion, "
                           + "            det_d.vr_deterioro              AS VrDeterioro, "
                           + "            det_d.vr_residual               AS VrResidual, "
                           + "            det_d.depreciacion_mes          AS DepreciacionMes, "
                           + "            det_d.dep_acum                  AS DepreciacionAcumulada, "
                           + "            det_d.baseDepreciable           AS BaseDepreciable, "
                           + "            det_d.ImporteLibros             AS ImporteLibros "
                           + "        FROM "
                           + "            activos_fijos.detalle_depreciacion det_d "
                           + "        INNER JOIN activos_fijos.tb_tipo_norma n "
                           + "        ON "
                           + "            det_d.idnorma = n.id_tipo_Norma "
                           + "        UNION ALL "
                           + "        SELECT "
                           + "            cam.Fecha                          AS FechaRegistro, "
                           + "            'INTERNACIONAL'                    AS TipoNorma, "
                           + "            'ESTIMACIÓN VIDA UTIL'             AS TipoDocumento, "
                           + "            cam.Idactivo                       AS IdActivo, "
                           + "            cam.IdComponente                   AS IdComponente, "
                           + "            ''                                 AS idtipodeprecion, "
                           + "            ''                                 AS VidaUtilMes, "
                           + "            cam.VidaUtil                       AS VidaUtil, "
                           + "            cam.Vidautilizada                  AS VidaUtilizada, "
                           + "            cam.VidaRemanente                  AS VidaRemanente, "
                           + "            ''                                 AS VrRevaluacion, "
                           + "            ''                                 AS VrDeterioro, "
                           + "            ''                                 AS VrResidual, "
                           + "            ''                                 AS DepreciacionMes, "
                           + "            ''                                 AS DepreciacionAcumulada, "
                           + "            ''                                 AS BaseDepreciable, "
                           + "            ''                                 AS ImporteLibros "
                           + "        FROM "
                           + "            activos_fijos.AjusteDepreciaciacion cam "
                           + "        WHERE "
                           + "            cam.Tipo='Estimacion' "
                           + "        UNION ALL "
                           + "        SELECT "
                           + "            cam.Fecha                          AS FechaRegistro, "
                           + "            'INTERNACIONAL'                    AS TipoNorma, "
                           + "            'CAMBIO DE METODO'                 AS TipoDocumento, "
                           + "            cam.Idactivo                       AS IdActivo, "
                           + "            cam.IdComponente                   AS IdComponente, "
                           + "            ''                                 AS idtipodeprecion, "
                           + "            cam.VidaUtil                       AS VidaUtilMes, "
                           + "            cam.VidaUtil                       AS VidaUtil, "
                           + "            cam.Vidautilizada                  AS VidaUtilizada, "
                           + "            cam.VidaRemanente                  AS VidaRemanente, "
                           + "            ''                                 AS VrRevaluacion, "
                           + "            ''                                 AS VrDeterioro, "
                           + "            ''                                 AS VrResidual, "
                           + "            ''                                 AS DepreciacionMes, "
                           + "            ''                                 AS DepreciacionAcumulada, "
                           + "            ''                                 AS BaseDepreciable, "
                           + "            ''                                 AS ImporteLibros "
                           + "        FROM "
                           + "            activos_fijos.AjusteDepreciaciacion cam "
                           + "        WHERE "
                           + "            cam.Tipo='CambioMetodo' "
                           + "    ) "
                           + "    a "
                           + "INNER JOIN activo_componente ac "
                           + "ON "
                           + "    a.IdComponente = ac.id_Componente "
                           + "INNER JOIN activos_fijos.activo "
                           + "ON "
                           + "    ac.id_activo = activo.idActivo "
                           + "INNER JOIN subclase_componente sc "
                           + "ON "
                           + "    ac.sub_compID = sc.id_componente "
                           + "INNER JOIN activos_fijos.tb_tipo_depreciacion td "
                           + "ON "
                           + "    a.idtipodeprecion = td.id_tipo "
                           + "WHERE "
                           + "    a.IdActivo = '" + IdActivo + "' "
                           + CadenaNorma
                           + " ORDER BY "
                           + "     a.FechaRegistro ";
            return cad;
        }
        public string TrazabilidadNormas(string codigo) {
            string sql = "SELECT "
                              + "    a.*, "
                              + "    sc.Componente, "
                              + "    activo.Nombre "
                              + "FROM "
                              + "    ( "
                              + "        SELECT "
                              + "            d.fecha                         AS FechaRegistro, "
                              + "            'INTERNACIONAL'                 AS TipoNorma, "
                              + "            'DETERIORO'                     AS TipoDocumento, "
                              + "            d.idactivo                      AS IdActivo, "
                              + "            det_d.id_componente             AS IdComponente, "
                              + "            ''                              AS idtipodeprecion, "
                              + "            ''                              AS VidaUtilMes, "
                              + "            ''                              AS VidaUtil, "
                              + "            ''                              AS VidaUtilizada, "
                              + "            ''                              AS VidaRemanente, "
                              + "            ''                              AS VrRevaluacion, "
                              + "            det_d.Deterioro                 AS VrDeterioro, "
                              + "            ''                              AS VrResidual, "
                              + "            ''                              AS DepreciacionMes, "
                              + "            ''                              AS DepreciacionAcumulada, "
                              + "            det_d.BaseDepreciable           AS BaseDepreciable, "
                              + "            det_d.ImporteLibros             AS ImporteLibros "
                              + "        FROM "
                              + "            activos_fijos.detalle_deterioro det_d "
                              + "        INNER JOIN activos_fijos.deterioro d "
                              + "        ON "
                              + "            det_d.id_deterioro= d.id_deterioro "
                              + "        UNION ALL "
                              + "        SELECT "
                              + "            v.fecha                         AS FechaRegistro, "
                              + "            'INTERNACIONAL'                 AS TipoNorma, "
                              + "            'RESIDUAL'                      AS TipoDocumento, "
                              + "            v.idactivo                      AS IdActivo, "
                              + "            det_v.idcomponente              AS IdComponente, "
                              + "            ''                              AS idtipodeprecion, "
                              + "            ''                              AS VidaUtilMes, "
                              + "            ''                              AS VidaUtil, "
                              + "            ''                              AS VidaUtilizada, "
                              + "            ''                              AS VidaRemanente, "
                              + "            ''                              AS VrRevaluacion, "
                              + "            ''                              AS VrDeterioro, "
                              + "            det_v.ajusteValorResidual       AS VrResidual, "
                              + "            ''                              AS DepreciacionMes, "
                              + "            ''                              AS DepreciacionAcumulada, "
                              + "            ''                              AS BaseDepreciable, "
                              + "            ''                              AS ImporteLibros "
                              + "        FROM "
                              + "            activos_fijos.detalle_valorresidual det_v "
                              + "        INNER JOIN activos_fijos.valorresidual v "
                              + "        ON "
                              + "            det_v.idResidual = v.idResidual "
                              + "        UNION ALL "
                              + "        SELECT "
                              + "            r.fecha                          AS FechaRegistro, "
                              + "            'INTERNACIONAL'                  AS TipoNorma, "
                              + "            'REVALUACION'                    AS TipoDocumento, "
                              + "            r.idactivo                       AS IdActivo, "
                              + "            det_r.idcomponente               AS IdComponente, "
                              + "            ''                               AS idtipodeprecion, "
                              + "            ''                               AS VidaUtilMes, "
                              + "            ''                               AS VidaUtil, "
                              + "            ''                               AS VidaUtilizada, "
                              + "            ''                               AS VidaRemanente, "
                              + "            det_r.vr_revaluacion             AS VrRevaluacion, "
                              + "            ''                               AS VrDeterioro, "
                              + "            ''                               AS VrResidual, "
                              + "            ''                               AS DepreciacionMes, "
                              + "            ''                               AS DepreciacionAcumulada, "
                              + "            ''                               AS BaseDepreciable, "
                              + "            ''                               AS ImporteLibros "
                              + "        FROM "
                              + "            activos_fijos.detalle_revaluacion det_r "
                              + "        INNER JOIN activos_fijos.revaluacion r "
                              + "        ON "
                              + "            det_r.idrevaluacion = r.idrevaluacion "
                              + "        UNION ALL "
                              + "        SELECT "
                              + "            det_d.fecha                     AS FechaRegistro, "
                              + "            n.Norma                         AS TipoNorma, "
                              + "            'DEPRECIACIÓN'                  AS TipoDocumento, "
                              + "            det_d.idactivo                  AS IdActivo, "
                              + "            det_d.id_componente             AS IdComponente, "
                              + "            det_d.idtipodepreciacion        AS idtipodeprecion, "
                              + "            det_d.unidad_dep                AS VidaUtilMes, "
                              + "            det_d.vidaUtil                  AS VidaUtil, "
                              + "            det_d.vida_utilizada            AS VidaUtilizada, "
                              + "            det_d.vida_remanente            AS VidaRemanente, "
                              + "            det_d.ajust_vr_razonable        AS VrRevaluacion, "
                              + "            det_d.vr_deterioro              AS VrDeterioro, "
                              + "            det_d.vr_residual               AS VrResidual, "
                              + "            det_d.depreciacion_mes          AS DepreciacionMes, "
                              + "            det_d.dep_acum                  AS DepreciacionAcumulada, "
                              + "            det_d.BaseDepreciable           AS BaseDepreciable, "
                              + "            det_d.ImporteLibros             AS ImporteLibros "
                              + "        FROM "
                              + "            activos_fijos.detalle_depreciacion det_d "
                              + "        INNER JOIN activos_fijos.tb_tipo_norma n "
                              + "        ON "
                              + "            det_d.idnorma = n.id_tipo_Norma "
                              + "        UNION ALL "
                              + "        SELECT "
                              + "            cam.Fecha                          AS FechaRegistro, "
                              + "            'INTERNACIONAL'                    AS TipoNorma, "
                              + "            'ESTIMACIÓN VIDA UTIL'             AS TipoDocumento, "
                              + "            cam.Idactivo                       AS IdActivo, "
                              + "            cam.IdComponente                   AS IdComponente, "
                              + "            ''                                 AS idtipodeprecion, "
                              + "            ''                                 AS VidaUtilMes, "
                              + "            cam.VidaUtil                       AS VidaUtil, "
                              + "            cam.Vidautilizada                  AS VidaUtilizada, "
                              + "            cam.VidaRemanente                  AS VidaRemanente, "
                              + "            ''                                 AS VrRevaluacion, "
                              + "            ''                                 AS VrDeterioro, "
                              + "            ''                                 AS VrResidual, "
                              + "            ''                                 AS DepreciacionMes, "
                              + "            ''                                 AS DepreciacionAcumulada, "
                              + "            ''                                 AS BaseDepreciable, "
                              + "            ''                                 AS ImporteLibros "
                              + "        FROM "
                              + "            activos_fijos.AjusteDepreciaciacion cam "
                              + "        WHERE "
                              + "            cam.Tipo='Estimacion' "
                              + "        UNION ALL "
                              + "        SELECT "
                              + "            cam.Fecha                          AS FechaRegistro, "
                              + "            'INTERNACIONAL'                    AS TipoNorma, "
                              + "            'CAMBIO DE METODO'                 AS TipoDocumento, "
                              + "            cam.Idactivo                       AS IdActivo, "
                              + "            cam.IdComponente                   AS IdComponente, "
                              + "            ''                                 AS idtipodeprecion, "
                              + "            cam.VidaUtil                       AS VidaUtilMes, "
                              + "            cam.VidaUtil                       AS VidaUtil, "
                              + "            cam.Vidautilizada                  AS VidaUtilizada, "
                              + "            cam.VidaRemanente                  AS VidaRemanente, "
                              + "            ''                                 AS VrRevaluacion, "
                              + "            ''                                 AS VrDeterioro, "
                              + "            ''                                 AS VrResidual, "
                              + "            ''                                 AS DepreciacionMes, "
                              + "            ''                                 AS DepreciacionAcumulada, "
                              + "            ''                                 AS BaseDepreciable, "
                              + "            ''                                 AS ImporteLibros "
                              + "        FROM "
                              + "            activos_fijos.AjusteDepreciaciacion cam "
                              + "        WHERE "
                              + "            cam.Tipo='CambioMetodo' "
                              + "    ) "
                              + "    a "
                              + "INNER JOIN activo_componente ac "
                              + "ON "
                              + "    a.IdComponente = ac.id_Componente "
                              + "INNER JOIN activos_fijos.activo "
                              + "ON "
                              + "    ac.id_activo = activo.idActivo "
                              + "INNER JOIN subclase_componente sc "
                              + "ON "
                              + "    ac.sub_compID = sc.id_componente "
                              + "WHERE "
                              + "    a.IdActivo = '" + codigo + "' "
                              + "ORDER BY "
                              + "     a.FechaRegistro ";
            return sql;
        }
        public void informeNormaInternacionalDepreciacion(string IdActivo, string Placa, string NombreActivo)
        {
            try
            {

                Conexion_Mysql connect = new Conexion_Mysql();
                ReportePDF pdf = new ReportePDF("rprDetalleDepreciacionNiic");
                List<string> htm = pdf.Template;

                string cad = ConsultarTrazabilidadReporte(IdActivo, "INTERNACIONAL");

                DataSet dt = connect.EjecutarSelectMysql(string.Format(cad, IdActivo));

                //** Encabezado
                htm[1] = htm[1].Replace("DETALLE", "DETALLE DEPRECIACION DEL ACTIVO: " + Placa + "-" + NombreActivo);
                htm[1] = string.Format(htm[1], Global.Empresa, Global.Nit, "-" + Placa + " " + NombreActivo, DateTime.Now.ToString("yyyy-MM-dd"));
                cad = pdf.Inicio;
                //**
                int i = 0;
                while (i < dt.Tables[0].Rows.Count)
                {
                    double vr = 0;
                    DataRow[] rows = dt.Tables[0].Select("IdActivo='" + dt.Tables[0].Rows[i]["IdActivo"] + "'");
                    cad += htm[2];

                    foreach (DataRow item in rows)
                    {
                        cad += string.Format(htm[3], Convert.ToDateTime(item["FechaRegistro"]).ToString("yyyy-MM-dd"), item["Componente"], item["TipoDocumento"], item["Depreciacion"], item["VidaUtil"],
                                            string.Format("{0:n}", item["VidaUtilizada"]), string.Format("{0:n}", item["VidaRemanente"]), string.Format("{0:n}", item["costo_inicial"]),
                                            string.Format("{0:n}", item["VrRevaluacion"]), string.Format("{0:n}", item["VrDeterioro"]), string.Format("{0:n}", item["VrResidual"]),
                                            string.Format("{0:n}", item["DepreciacionMes"]), string.Format("{0:n}", item["DepreciacionAcumulada"]),
                                            string.Format("{0:n}", item["BaseDepreciable"]), string.Format("{0:n}", item["ImporteLibros"])
                                            );

                    }

                    cad += string.Format(htm[4], "");
                    i += rows.Length;
                }
                //  cad += string.Format(htm[5]);

                cad += pdf.Fin;
                pdf.createPDF(cad, htm[1], "Detalles_Depreciacion_", pdf.getPageConfig("LETTER", true));
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
        public void informeNormaTributariaDepreciacion(string IdActivo, string Placa, string NombreActivo)
        {
            try
            {

                Conexion_Mysql connect = new Conexion_Mysql();
                ReportePDF pdf = new ReportePDF("rprDetalleDepreciacionTributaria");
                List<string> htm = pdf.Template;

                string cad = "SELECT d.idactivo as idactivo,ca.costo_inicial as costoinicial, s_c.Componente as descripcion, d.fecha,  dd.base, dd.depreciacion_mes as dep_mes,dd.unidad_dep as cantdep,dd.ImporteLibros as saldo_dep,dd.dep_acum as depacumulada, dd.id_componente,  "
                                                                        + "dd.vr_residual as vresidual,dd.ajust_dep_acum as ajdepacumulada,dd.ajust_vr_razonable as ajrazonable,dd.vr_deterioro as vrdeterioro, dd.tipo_doc FROM depreciacion d "
                                                                        + "INNER JOIN detalle_depreciacion dd ON d.id_depreciacion = dd.id_depreciacion "
                                                                        + "INNER JOIN activo_componente ca ON dd.id_componente=ca.id_Componente "
                                                                        + "INNER JOIN subclase_componente as s_c ON ca.sub_compID=s_c.id_componente "
                                                                        + "WHERE d.idactivo = '{0}' AND dd.tipo_doc='DEPRECIACIÓN' ORDER BY descripcion,d.fecha,d.FyH_Real";


                DataSet dt = connect.EjecutarSelectMysql(string.Format(cad, IdActivo));

                //** Encabezado
                htm[1] = htm[1].Replace("DETALLE", "DETALLE TRAZABILIDAD DEL ACTIVO: " + Placa + "-" + NombreActivo);
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
                        cad += string.Format(htm[3], Convert.ToDateTime(item["fecha"]).ToString("yyyy-MM-dd"), item["descripcion"], string.Format("{0:n}",
                                            item["costoinicial"]), string.Format("{0:n}", item["cantdep"]), string.Format("{0:n}", item["ajrazonable"]),
                                            string.Format("{0:n}", item["ajdepacumulada"]), string.Format("{0:n}", item["vresidual"]),
                                            string.Format("{0:n}", item["vrdeterioro"]), string.Format("{0:n}", item["base"]), string.Format("{0:n}", item["dep_mes"]),
                                            string.Format("{0:n}", item["depacumulada"]), string.Format("{0:n}", item["saldo_dep"])
                                            );

                    }

                    cad += string.Format(htm[4], "");
                    i += rows.Length;
                }
                //  cad += string.Format(htm[5]);

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
        #endregion
        /// <summary>
        ///  Crea la lista que se va imprimir para visualizar la depreciacion antes de grabar.
        /// </summary>
        /// <param name="ListaActivos"></param>
        /// <param name="parteExcel"></param>
        /// <returns></returns>
        public bool ListaActivosDeprExcel(List<Activo> ListaActivos,string FilePath)
        {

            try
            {
                StreamWriter plano = new StreamWriter(FilePath);
                string encabezado = "SUBCLASE;PLACA;FECHA_U_DEPRECIACION;FECHA_REVISION;NO_COMPRA;NOMBRE_COMPONENTE;NOMBRE _NORMA;TIPO_DEPRECIACION;PORCENTAJE;VIDA_UTIL;UNIDAD_DEP;VIDA_UTIL_UTILIZADO;VIDA_UTIL_REMANENTE;COSTO_INICIAL;AJUSTE_RESIDUAL;AJUSTE_DETERIORO;AJUSTE_RAZONABLE;DEP_MES;DEP_ACUMULADA_ANTERIOR;DEP_ACUMULADA;BASE_DEPRECIABLE_ANTERIOR;BASE_DEPRECIABLE;IMPORTE_LIBROS";

                plano.WriteLine(encabezado);
                foreach (Activo itemActivo in ListaActivos)
                {
                 
                    foreach (ActivoComponente itemcomp in itemActivo.Componente)
                    {
                        
                        string linea = string.Empty;
                        linea += itemActivo.nombresubclase.ToString() + ";";
                        linea += itemActivo.Placa.ToString() + ";";
                        linea += itemActivo.Fecha_Udepreciacion.ToString() + ";";
                        linea += itemActivo.Fecha_Revision.ToString() + ";";
                        linea += itemActivo.NoCompra.ToString() + ";";
                        linea += itemcomp.nombre_componente.ToString() + ";";
                        linea += itemcomp.NombreNorma.ToString() + ";";
                        linea += itemcomp.nombre_depreciacion.ToString() + ";";
                        linea += itemcomp.Porcentaje_ci.ToString() + ";";
                        linea += itemcomp.vida_util.ToString() + ";";
                        linea += itemcomp.unidad_dep.ToString() + ";";
                        linea += itemcomp.vida_util_utilizado.ToString() + ";";
                        linea += itemcomp.vida_util_remanente.ToString() + ";";
                        linea += itemcomp.costo_inicial.ToString() + ";";
                        linea += itemcomp.ajust_vr_residual.ToString() + ";";
                        linea += itemcomp.ajust_vr_deterioro.ToString() + ";";
                        linea += itemcomp.ajust_vr_razonable.ToString() + ";";
                        linea += itemcomp.vr_dep_mes.ToString() + ";";
                        linea += itemcomp.vr_dep_acumulada_old.ToString() + ";";
                        linea += itemcomp.vr_dep_acumulada.ToString() + ";";
                        linea += itemcomp.base_deprec_old.ToString() + ";";
                        linea += itemcomp.base_deprec.ToString() + ";";
                        linea += itemcomp.vr_importe_libros.ToString() + ";";

                        linea = linea.TrimEnd(';');
                        plano.WriteLine(linea);

                    }
                  
                }
                plano.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
           
        }

        #region REPORTE DEPRECIACION MES
        public DataTable ConsultarControlDepreciacionMes()
        {
            String sql = @"SELECT
                                rcd.Ncontrol AS CODIGO,
                                CONCAT(IF(rcd.Descripcion ='Entrada','ENTRADA ANTES DEL','DEPRECIACIÓN DEL'),' ', Date_format(
                                rcd.Fecha, '%d-%m-%Y')) AS DESCRIPCION
                            FROM
                                activos_fijos.registro_control_depreciacion rcd
                            WHERE rcd.Estado = 'Activo'
                            ORDER BY
                                rcd.Fecha,
                                rcd.Ncontrol";

           return conexion.EjecutarSelectMysql(sql).Tables[0];
        }

        public DataTable ConsultarDepreciacionMes(string codigo) {

            string sql = @"SELECT
	                         sc.Componente 				     AS SUBCLASE,
                            a.placa                          AS PLACA,
                            Date_format(dd.Fecha,'%d-%m-%Y') AS FECHA_REVISION,
                            a.NoCompra 					     AS NO_COMPRA,
                            sc.Componente                    AS NOMBRE_COMPONENTE,
                            tn.Norma                         AS NOMBRE_NORMA,
                            ttd.Depreciacion                 AS TIPO_DEPRECIACION,
                            ac.Porcentaje_ci                 AS PORCENTAJE,
                            dd.vidaUtil                      AS VIDA_UTIL,
                            dd.unidad_dep                    AS UNIDAD_DEP,
                            dd.vida_utilizada                AS VIDA_UTIL_UTILIZADO,
                            dd.vida_remanente                AS VIDA_UTIL_REMANENTE,
                            dd.Costo_Inicial                 AS COSTO_INICIAL,
                            dd.vr_residual                   AS AJUSTE_RESIDUAL,
                            dd.vr_deterioro                  AS AJUSTE_DETERIORO,
                            dd.ajust_vr_razonable            AS AJUSTE_RAZONABLE,
                            dd.depreciacion_mes 			 AS DEP_MES,
                            dd.dep_acum 					 AS DEP_ACUMULADA,
                            dd.baseDepreciable 				 AS BASE_DEPRECIABLE,
                            dd.ImporteLibros 				 AS IMPORTE_LIBROS
                        FROM
                            detalle_depreciacion dd
                        INNER JOIN depreciacion d
                        ON
                            dd.id_depreciacion = d.id_depreciacion
                        INNER JOIN activo a 
                        ON  d.idactivo = a.idActivo 
                        INNER JOIN activo_componente ac
                        ON
                        dd.id_componente = ac.id_Componente
                        INNER JOIN tb_tipo_norma tn
                        ON
                        dd.idnorma =tn.id_tipo_Norma
                        INNER JOIN tb_tipo_depreciacion ttd
                        ON
                            dd.idtipodepreciacion=ttd.id_tipo
                        INNER JOIN subclase_componente sc
                        ON
                            ac.sub_compID = sc.id_componente
                        WHERE
                            d.IDcontrol = '" + codigo + "'";
            return conexion.EjecutarSelectMysql(sql).Tables[0];
        }
        #endregion

    }
    public class ListaComponenteExcel
    {
        public string FechaDepreciacionActual { get; set; }
        public string FechaRevision { get; set; }
        public string NoCompra { get; set; }
        public string NombreSubclase { get; set; }
        public string Placa { get; set; }
        public string NombreComponente { get; set; }
        public string Norma { get; set; }
        public string Porcentaje { get; set; }
        public string TipoDepreciacion { get; set; }
        public string VidaUtil { get; set; }
        public string UnidadMes { get; set; }
        public string VidaEjecutada { get; set; }
        public string VidaRemanente { get; set; }
        public string CostoInicial { get; set; }
        public string Residual { get; set; }
        public string Razonable { get; set; }
        public string Deterioro { get; set; }
        public string DepreciacionMes { get; set; }
        public string DepreciacionAcumuladaAnterior { get; set; }
        public string DepreciacionAcumulada { get; set; }
        public string BaseDepreciableAnterior { get; set; }
        public string BaseDepreciable { get; set; }
        public string ImporteLibros { get; set; }
    }

   
}
