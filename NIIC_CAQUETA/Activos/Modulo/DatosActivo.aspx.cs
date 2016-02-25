using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Activos.Clases;
using System.Data;
using System.Globalization;

namespace Activos.Modulo
{
    public partial class DatosActivo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TriggerField1_Click(object sender, DirectEventArgs e)
        {
            Conexion_Mysql conexion = new Conexion_Mysql();
            DataSet  dt =  conexion.EjecutarSelectMysql("SELECT Nombre as nombre, idActivo as codigo, placa FROM activo");
            GridActivo.GetStore().DataSource = dt;
            GridActivo.GetStore().DataBind();
        }
        protected void GridComodato_SelectRow(object sender, DirectEventArgs e)
        {
            TriggerField1.Text = e.ExtraParams["NComodato"].ToString();
            this.SelectDatos(e.ExtraParams["NComodato"].ToString());
            this.CargarGrillaCaracteristica(e.ExtraParams["NComodato"].ToString());
            this.CargarGrillaComponentesNIIF(e.ExtraParams["NComodato"].ToString());
            this.CargarGrillaComponentesTributaria(e.ExtraParams["NComodato"].ToString());
        }

        private void CargarGrillaComponentesTributaria(string placa)
        {
            Conexion_Mysql conexion = new Conexion_Mysql();

            string sql = "SELECT "
                            + "    s_c.Componente AS descripcion, "
                            + "    t_d.Depreciacion AS TipoDepreciacion, "
                            + "    a_c.vida_util      AS vutil,  "
                            + "   a_c.Porcentaje_ci  AS porcentaje, "
                            + "    a_c.costo_inicial  AS costo, "
                            + "    a_c.ajus_vr_residual AS Residual, "
                            + "    a_c.ajus_vr_deterioro AS Deterioro, "
                            + "    a_c.ajus_vr_razonable AS Revaluacion, "
                            + "    a_c.ajust_dep_acum  AS AjusteDepreciacionAcumulada, "
                            + "    a_c.reexpresionVidaUtil AS ReexpresionVidaUtil, "
                            + "    a_c.base_deprec    AS Basedepreciable, "
                            + "    a_c.vr_dep_mes AS DepreciacionMes, "
                            + "    a_c.vr_dep_acumulada  AS DepreciacionAcumulada, "
                            + "    vr_importe_libros AS ImporteLibros "
                            + "FROM "
                            + "    activo_componente          AS a_c "
                            + "INNER JOIN subclase_componente AS s_c "
                            + "ON "
                            + "    a_c.sub_compID=s_c.id_componente "
                            + "INNER JOIN tb_tipo_depreciacion t_d "
                            + "ON  "
                            + "    a_c.id_tipo_depreciacion = t_d.id_tipo "
                            + "WHERE "
                            + "    id_activo = "
                            + "    ( "
                            + "        SELECT "
                            + "            a.idActivo "
                            + "        FROM "
                            + "            activo a "
                            + "        WHERE "
                            + "            a.placa='" + placa + "' "
                            + "    ) "
                            + "AND a_c.idtiponorma='1'";

            DataSet dt = conexion.EjecutarSelectMysql(sql);
            GridPanel5.GetStore().DataSource = dt;
            GridPanel5.DataBind();
        }
       
        
        [DirectMethod]
        public void SelectDatos(string placa)
        {
            Conexion_Mysql conexion = new Conexion_Mysql();
            string sql = "SELECT"
                        + " a.idActivo AS idactivo,"
                        + " a.Nombre,"
                        + " CONCAT('(',p.codProveedor,') ', p.Proveedor,' ')                                AS Proveedor,"
                        + " IF(a.FechaEntrada IS NULL,'N/D', Date_format( a.FechaEntrada,'%d-%m-%Y'))       AS FechaEntrada,"
                        + " IF(a.Fecha_Alta IS NULL,'N/D',Date_format( a.Fecha_Alta, '%d-%m-%Y'))           AS Fecha_Alta,"
                        + " IF(a.Fecha_Udepreciacion IS NULL,'N/D',Date_format( a.Fecha_Udepreciacion, '%d-%m-%Y')) AS"
                        + " Fecha_Udepreciacion,"
                        + " IF(a.Fecha_Baja IS NULL,'N/D',Date_format( a.Fecha_Baja, '%d-%m-%Y'))           AS Fecha_Baja,"
                        + " UPPER(pr.Propiedad)   AS Propiedad,"
                        + " UPPER(c.Clase)        AS Clase,"
                        + " UPPER(sc.Subclase)    AS Subclase,"
                        + " UPPER(ta.Tipo_Activo) AS TipoActivo,"
                        + " IF(a.Estado='A','ACTIVO','INACTIVO') AS Estado,"
                        + " a.CostoInicial,"
                        + " a.vr_deterioro,"
                        + " a.vr_residual,"
                        + " a.vr_razonable,"
                        + " a.ajus_dep_acum,"
                        + " a.vr_dep_mes,"
                        + " a.vr_Depreciacion,"
                        + " a.Importe_libros "
                        + " FROM"
                        + "     activo a"
                        + " INNER JOIN tb_clase c"
                        + " ON"
                        + "     a.id_clase = c.id_clase"
                        + " INNER JOIN tb_subclase sc"
                        + " ON"
                        + "     a.id_subclase = sc.id_subclase"
                        + " INNER JOIN proveedor p"
                        + " ON"
                        + "     a.Proveedor = p.codProveedor"
                        + " INNER JOIN tb_propiedad pr"
                        + " ON"
                        + "     a.id_propiedad=pr.id_propiedad"
                        + " INNER JOIN tb_tipo_activo ta"
                        + " ON"
                        + "     a.id_tipo_activo = ta.id_tipo_activo"
                        + " WHERE"
                        + "     a.placa='"+placa+"'"; 
            DataSet dt = conexion.EjecutarSelectMysql(sql);

            txtNombre.Text = dt.Tables[0].Rows[0]["Nombre"].ToString();
            TextProveedor.Text = dt.Tables[0].Rows[0]["Proveedor"].ToString();
            TextFechaIngreso.Text = dt.Tables[0].Rows[0]["FechaEntrada"].ToString();
            TextFechaAlta.Text = dt.Tables[0].Rows[0]["Fecha_Alta"].ToString();
            TextFechaBaja.Text = dt.Tables[0].Rows[0]["Fecha_Baja"].ToString();
            TextFechaDepreciacion.Text = dt.Tables[0].Rows[0]["Fecha_Udepreciacion"].ToString();

            txtPropiedad.Text = dt.Tables[0].Rows[0]["Propiedad"].ToString();
            TextClase.Text = dt.Tables[0].Rows[0]["Clase"].ToString();
            TextSubclase.Text = dt.Tables[0].Rows[0]["Subclase"].ToString();
            txtTipoActivo.Text = dt.Tables[0].Rows[0]["TipoActivo"].ToString();
            txtEstado.Text = dt.Tables[0].Rows[0]["Estado"].ToString();

            txtCostoInicial.Text = Convert.ToDouble(dt.Tables[0].Rows[0]["CostoInicial"].ToString()).ToString("N1", CultureInfo.InvariantCulture);
            txtresidual.Text = Convert.ToDouble(dt.Tables[0].Rows[0]["vr_residual"].ToString()).ToString("N1", CultureInfo.InvariantCulture);
            txtdeterioro.Text = Convert.ToDouble(dt.Tables[0].Rows[0]["vr_deterioro"].ToString()).ToString("N1", CultureInfo.InvariantCulture);
            txtrazonable.Text = Convert.ToDouble(dt.Tables[0].Rows[0]["vr_razonable"].ToString()).ToString("N1", CultureInfo.InvariantCulture);
            txtajusdepAcum.Text = Convert.ToDouble(dt.Tables[0].Rows[0]["ajus_dep_acum"].ToString()).ToString("N1", CultureInfo.InvariantCulture);
            txtdepreciado.Text = Convert.ToDouble(dt.Tables[0].Rows[0]["vr_Depreciacion"].ToString()).ToString("N1", CultureInfo.InvariantCulture);
            txtImporteDepreciable.Text = Convert.ToDouble(dt.Tables[0].Rows[0]["ImporteDepreciable"].ToString()).ToString("N1", CultureInfo.InvariantCulture);
            txtimporte.Text = Convert.ToDouble(dt.Tables[0].Rows[0]["Importe_libros"].ToString()).ToString("N1", CultureInfo.InvariantCulture);
           

            // CONSULTA ASIGANR POR ALTA
            sql =        "SELECT "
                            +"    CONCAT('(',ec.codigo,') ',ec.Centro_Economico)                    AS Centro_Economico, "
                            +"    CONCAT('(',tc.codigo,') ',tc.Centro_costo,' ',dca.porcentaje,'%') AS Centro_costo, "
                            +"    CONCAT('(', u.codigo,') ',u.Unidad_g_Efectivo)                    AS Uge, "
                            +"    CONCAT('(',d.IdEmpresa,') ',d.Nombre) AS Dependencia, "
                            +"  CONCAT('(',e.id_empleado,') ',e.Nombres,' ',e.Apellido1,' ',e.Apellido2) AS Empleado, "
                            +"    f.Funcion, "
                            +"   c.Condicion "
                            +" FROM "
                            +"    activo a "
                            +" INNER JOIN asignacionactivo asi "
                            +" ON "
                            +"    a.idActivo = asi.idactivo "
                            +" AND asi.estado='A' "
                            +" INNER JOIN detalle_centrocosto_asignacionactivo dca "
                            +" ON "
                            +"    asi.idasignacion = dca.idasignacion "
                            +" INNER JOIN tb_centro_costo tc "
                            +" ON "
                            +"    dca.id_centro = tc.id_centro "
                            +" INNER JOIN tb_centro_economico ec "
                            +"ON "
                            +"    a.id_centro_economico = ec.id_centro "
                            +" INNER JOIN tb_uge u "
                            +" ON "
                            +"    a.id_uge = u.id_uge "
                            +" INNER JOIN tb_funcion f "
                            +" ON "
                            +"    a.id_funcion = f.id_funcion "
                            +" INNER JOIN tb_condicion c "
                            +" ON "
                            +"    a.id_condicion = c.id_condicion "
                            +" INNER JOIN dependencia d "
                            +" ON "
                            +"    a.id_dependencia = d.Codigo "
                            + " INNER JOIN empleado e"
                            + " ON "
                            + "    a.id_empleado = e.id_empleado "
                            +" WHERE "
                            +"    a.placa = '"+placa +"'";

            DataSet datasetAlta = conexion.EjecutarSelectMysql(sql);
                    
            // Cargas Propiedades del Activo
            if(datasetAlta.Tables[0].Rows.Count!=0){
                txtfuncion.Text = datasetAlta.Tables[0].Rows[0]["Funcion"].ToString();
                txtubicacion.Text = datasetAlta.Tables[0].Rows[0]["Dependencia"].ToString();
                TextUge.Text = datasetAlta.Tables[0].Rows[0]["Uge"].ToString();
                TextCentroCosto.Text = datasetAlta.Tables[0].Rows[0]["Centro_costo"].ToString();
                TextCentroEconomico.Text = datasetAlta.Tables[0].Rows[0]["Centro_Economico"].ToString();
                txtCondicion.Text = datasetAlta.Tables[0].Rows[0]["Condicion"].ToString();
                TextFuncionario.Text = datasetAlta.Tables[0].Rows[0]["Empleado"].ToString();
            }
          
        
        }
        [DirectMethod]
        public void CargarGrillaCaracteristica(string placa) {
            Conexion_Mysql conexion = new Conexion_Mysql();
            string sql = "SELECT "
                            + "t_b.Caracteristica AS caracteristica, "
                            + "a_c.valor          AS valor "
                            +"FROM "
                            +"    activo_caracteristica     AS a_c "
                            +"INNER JOIN tb_caracteristicas AS t_b "
                            +"ON "
                            +"    a_c.id_caracteristica = t_b.id_caracteristica "
                            +"WHERE "
                            +"    a_c.id_activo= (SELECT a.idActivo FROM activo a WHERE a.placa='"+placa+ "')";
            DataSet dt = conexion.EjecutarSelectMysql(sql);
            GridPanel4.GetStore().DataSource = dt;
            GridPanel4.DataBind();
        }
        [DirectMethod]
        public void CargarGrillaComponentesNIIF(string placa) {
            Conexion_Mysql conexion = new Conexion_Mysql();

            string sql = "SELECT "
                            +"    s_c.Componente AS descripcion, "
                            +"    t_d.Depreciacion AS TipoDepreciacion, "
                            +"    a_c.vida_util      AS vutil,  "
                            +"   a_c.Porcentaje_ci  AS porcentaje, "
                            +"    a_c.costo_inicial  AS costo, "
                            +"    a_c.ajus_vr_residual AS Residual, "
                            +"    a_c.ajus_vr_deterioro AS Deterioro, "
                            +"    a_c.ajus_vr_razonable AS Revaluacion, "
                            +"    a_c.ajust_dep_acum  AS AjusteDepreciacionAcumulada, "
                            +"    a_c.reexpresionVidaUtil AS ReexpresionVidaUtil, "
                            +"    a_c.base_deprec    AS Basedepreciable, "
                            +"    a_c.vr_dep_mes AS DepreciacionMes, "
                            +"    a_c.vr_dep_acumulada  AS DepreciacionAcumulada, "
                            +"    a_c.ImporteDepreciable AS SaldoDepreciable, "
                            +"    vr_importe_libros AS ImporteLibros "
                            +"FROM "
                            +"    activo_componente          AS a_c "
                            +"INNER JOIN subclase_componente AS s_c "
                            +"ON "
                            +"    a_c.sub_compID=s_c.id_componente "
                            +"INNER JOIN tb_tipo_depreciacion t_d "
                            +"ON  "
                            +"    a_c.id_tipo_depreciacion = t_d.id_tipo "
                            +"WHERE "
                            +"    id_activo = "
                            +"    ( "
                            +"        SELECT "
                            +"            a.idActivo "
                            +"        FROM "
                            +"            activo a "
                            +"        WHERE "
                            +"            a.placa='"+ placa +"' " 
                            +"    ) "
                            +"AND a_c.idtiponorma='1'";                         
                                                                    
            DataSet dt = conexion.EjecutarSelectMysql(sql);
            GridPanel3.GetStore().DataSource = dt;
            GridPanel3.DataBind();
        }
    }
}