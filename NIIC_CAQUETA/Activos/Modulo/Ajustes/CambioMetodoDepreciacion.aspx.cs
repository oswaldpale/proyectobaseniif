using Activos.Clases;
using Ext.Net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Activos.Modulo.Ajustes
{
    public partial class CambioMetodoDepreciacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Conexion_Mysql conexion = new Conexion_Mysql();
            DataSet dt = conexion.EjecutarSelectMysql("SELECT id_medida as codigo,UnidadMedida FROM tb_umedida");
            cbx_medida.GetStore().DataSource = dt;
            cbx_medida.GetStore().DataBind();
        
        }  
        [DirectMethod(ShowMask = true, Msg = "Cargando SubClases...")]
        public void CargarSubClases()
        {
            try
            {
                Conexion_Mysql conexion = new Conexion_Mysql();
                StoreSubclase.DataSource = conexion.EjecutarSelectMysql("SELECT s.id_subclase AS Codigo, s.id_clase as IdClase, "
                                                                 + " c.Clase, s.Subclase AS Nombre "
                                                                 + " FROM tb_subclase s "
                                                                 + " INNER JOIN tb_clase c ON s.id_clase=c.id_clase"
                );
                StoreSubclase.DataBind();
            }
            catch (Exception exc)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Error",
                    Message = exc.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.INFO,
                    Closable = false
                });
            }
        }

        private void CargarCbxComponente(string IdSubclase)
        {
            Conexion_Mysql conexion = new Conexion_Mysql();
            String sql = "SELECT "
                            + "    ac.sub_compID    AS IdComponente, "
                            + "    c.Componente AS nombre_componente "
                            + "FROM "
                            + "    activo a "
                            + "INNER JOIN activo_componente ac "
                            + "ON "
                            + "    a.idActivo = ac.id_activo "
                            + "AND ac.idtiponorma ='1' "
                            + "INNER JOIN subclase_componente c "
                            + "ON "
                            + "    ac.sub_compID = c.id_componente "
                            + "WHERE "
                            + "    a.id_subclase = '" + IdSubclase + "' "
                            + "GROUP BY "
                            + "    ac.sub_compID";


            DataSet dt = conexion.EjecutarSelectMysql(sql);
            store_Componente.DataSource = dt;
            store_Componente.DataBind();

        }

        protected void GridComodato_SelectSubclase(object sender, DirectEventArgs e)
        {
            TriggerListaSubclase.Text = "(" + e.ExtraParams["Clase"].ToString() + ") " + e.ExtraParams["Nombre"].ToString();
            idsubclase.SetValue(e.ExtraParams["Codigo"].ToString());
            idclase.SetValue(e.ExtraParams["IdClase"].ToString());
            CargarGrillaReexpresion(e.ExtraParams["Codigo"].ToString());
            CargarCbxComponente(e.ExtraParams["Codigo"].ToString());
            CargarCbxTipoMetodo();

        }

        private void CargarGrillaReexpresion(string IdSubclase)
        {
            Conexion_Mysql conexion = new Conexion_Mysql();
                    String sql = " SELECT "
                     + "    a.idActivo                        AS IdActivo, "
                     + "    CONCAT('(',a.placa,') ',a.Nombre) AS Activo, "
                     + "    ac.id_Componente                  AS IdComponente, "
                     + "    ac.sub_compID                     AS IdsubComponente, "
                     + "    UPPER(c.Componente)               AS nombre_componente, "
                     + "    CONCAT(ac.Porcentaje_ci,'%')      AS Porcentaje_ci, "
                     + "    d.id_tipo                         AS IDdepreciacion, "
                     + "    ac.vida_util , "
                     + "    ac.vida_util_utilizado, "
                     + "    IF(ac.vida_util_remanente IS NULL, ac.vida_util,ac.vida_util_remanente)  AS vida_util_remanente,  "
                     + "    d.Depreciacion, "
                     + "    ac.ajust_dep_acum AS AjusteDepreciacionAcumulada, "
                     + "    ac.ajus_vr_residual AS AjusteResidual, "
                     + "    ac.ajus_vr_deterioro AS AjusteDeterioro, "
                     + "    ac.ajus_vr_razonable AS AjusteRazonable, "
                     + "    ac.base_deprec AS BaseDepreciable, "
                     + "    ac.vr_dep_mes AS DepreciacionMes, "
                     + "    ac.vr_dep_acumulada AS DepreciacionAcumulada, "
                     + "    ac.vr_importe_libros AS ImporteLibros "
                     + "FROM "
                     + "    activo a "
                     + "INNER JOIN activo_componente ac "
                     + "ON "
                     + "    a.idActivo = ac.id_activo "
                     + "AND ac.idtiponorma ='1' "
                     + "INNER JOIN subclase_componente c "
                     + "ON "
                     + "    ac.sub_compID = c.id_componente "
                     + "INNER JOIN tb_tipo_depreciacion d "
                     + "ON "
                     + "    ac.id_tipo_depreciacion =d.id_tipo "
                     + "WHERE "
                     + "    a.id_subclase = '" + IdSubclase + "' "
                     + "AND a.Estado='A' "
                     + "AND a.id_empleado IS NOT NULL "
                     + "AND a.Fecha_Udepreciacion IS NOT NULL "
                     + "AND a.Fecha_Baja IS NULL "
                     + "ORDER BY "
                     + "    a.idActivo, "
                     + "    ac.id_Componente, "
                     + "    ac.id_tipo_depreciacion";
                    
                    DataSet dt = conexion.EjecutarSelectMysql(sql);
                    List<CambiarMetodo> ListaCambioMetodo = new List<CambiarMetodo>();

                    foreach (DataRow item in dt.Tables[0].Rows)
                    {
                        CambiarMetodo Lista = new CambiarMetodo();
                        Lista.IdActivo = item["IdActivo"].ToString();
                        Lista.Activo = item["Activo"].ToString();
                        Lista.IdComponente = item["IdComponente"].ToString();
                        Lista.IdsubComponente = item["IdsubComponente"].ToString();
                        Lista.nombre_componente = item["nombre_componente"].ToString();
                        Lista.Porcentaje_ci = item["Porcentaje_ci"].ToString();
                        Lista.IDdepreciacion = item["IDdepreciacion"].ToString();
                        Lista.Depreciacion = item["Depreciacion"].ToString();
                        Lista.vida_util = float.Parse(item["vida_util"].ToString());
                        Lista.vida_util_utilizado = float.Parse( item["vida_util_utilizado"].ToString());
                        Lista.vida_util_remanente = float.Parse(item["vida_util_remanente"].ToString());
                        Lista.AjusteDepreciacionAcumulada = float.Parse(item["AjusteDepreciacionAcumulada"].ToString());
                        Lista.AjusteResidual = float.Parse(item["AjusteResidual"].ToString());
                        Lista.AjusteDeterioro = float.Parse(item["AjusteDeterioro"].ToString());
                        Lista.AjusteRazonable = float.Parse(item["AjusteRazonable"].ToString());
                        Lista.BaseDepreciable = float.Parse(item["BaseDepreciable"].ToString());
                        Lista.DepreciacionMes = float.Parse(item["DepreciacionMes"].ToString());
                        Lista.DepreciacionAcumulada = float.Parse(item["DepreciacionAcumulada"].ToString());
                        Lista.ImporteLibros = float.Parse(item["ImporteLibros"].ToString());
                        ListaCambioMetodo.Add(Lista);
                    }
                    if (ListaCambioMetodo.Count > 0)
                    {
                        Session["ListaCambioMetodo"] = ListaCambioMetodo;
                        store_CambioMetodo.DataSource = ListaCambioMetodo;
                        store_CambioMetodo.DataBind();
                    }
                    else
                    {
                        X.MessageBox.Show(new MessageBoxConfig
                        {
                            Title = "Activos No Existentes!..",
                            Message = "No Existe Activos dado de Alta en esta Subclase",
                            Buttons = MessageBox.Button.OK,
                            Icon = MessageBox.Icon.INFO,
                            Closable = true
                        });
                        gridCambioMetodo.GetStore().RemoveAll();
                    }
        }

        private void CargarCbxTipoMetodo()
        {
            Conexion_Mysql conexion = new Conexion_Mysql();
            String sql = "SELECT "
            + "    id_tipo AS IdMetodoDepreciacion, Depreciacion AS MetodoDepreciacion "
            + "    FROM "
            + "    tb_tipo_depreciacion";

            store_metodo.DataSource = conexion.EjecutarSelectMysql(sql);
            store_metodo.DataBind();

        }
        [DirectMethod(ShowMask = true, Msg = "Aplicando Cambios.. ")]

        public void AplicarAjuste(string IdSubComponete, string IdTipoMetodo,string EtiquetaTIpoMetodo,string cantidad)
        {
            List<CambiarMetodo> ListaCambioMetodo = (List<CambiarMetodo>) Session["ListaCambioMetodo"];
            int VerificarCambioMetodo =  ListaCambioMetodo.Where(w => w.IdsubComponente == IdSubComponete).Where(X => X.IDdepreciacion == IdTipoMetodo).Count();
            if (VerificarCambioMetodo  > 0)
            {
                  X.MessageBox.Show(new MessageBoxConfig
                        {
                            Title = " Seleccione otro método ",
                            Message = "El método selecionado ya esta en ejecución",
                            Buttons = MessageBox.Button.OK,
                            Icon = MessageBox.Icon.INFO,
                            Closable = true
                        });
                   cbx_Metodo.Reset();
            }
            else
            {
                foreach (CambiarMetodo item in ListaCambioMetodo.Where(w => w.IdsubComponente == IdSubComponete))
                {
                    item.IDdepreciacion = IdTipoMetodo;
                    item.vida_util =float.Parse(cantidad);
                    item.Depreciacion = cbx_Metodo.SelectedItem.Text; 
                    item.vida_util_remanente =float.Parse(cantidad);
                    item.vida_util_utilizado = 0;
                }
                Session["ListaCambioMetodo"] = ListaCambioMetodo;
                store_CambioMetodo.DataSource = ListaCambioMetodo;
                store_CambioMetodo.DataBind();
            }

        }

       [DirectMethod(ShowMask = true, Msg = "Grabando Cambios...  ")]
        public void GrabarCambiosAplicados(string IdSubComponete, string IdTipoMetodo, string TipoMedida, string cantidad)
        {
            try
            {
                string Fecha = Convert.ToDateTime(FechaCambio.Text).ToString("yyyy-MM-dd"); 
                Conexion_Mysql conexion = new Conexion_Mysql();
                List<string> sql = new List<string>();    //Actualizo la plantilla subComponente
                    string sentencia = "UPDATE "
                    + "    subclase_componente "
                    + "SET "
                    + "    VidaUtil = '" + cantidad + "', "
                    + "    id_metodoDepreciacion = '" + IdTipoMetodo + "', "
                    + "    id_medida = '" + TipoMedida + "' "
                    + "WHERE "
                    + "    id_componente ='" + IdSubComponete + "'";
                sql.Add(sentencia);

               sentencia = "UPDATE    "                    // Actualizo todos los subcomponentes  con el Nuevo Metodo Depreciacion
                + "    activo_componente ac, "
                + "    ( "
                + "        SELECT "
                + "            a.idActivo "
                + "        FROM "
                + "            activo a "
                + "        WHERE "
                + "            a.Estado='A' "
                + "    ) "
                + "    a "
                + "SET "
                + "    ac.vida_util = '" + cantidad + "', "
                + "    ac.vida_util_utilizado = 0 , "
                + "    ac.vida_util_remanente = '" + cantidad + "', "
                + "    ac.id_tipo_depreciacion = '" + IdTipoMetodo + "' "
                + "WHERE "
                + "    ac.id_activo = a.idActivo "
                + " AND   ac.sub_compID = '" + IdSubComponete + "'"
                + " AND   ac.idtiponorma= '1'";
                
               sql.Add(sentencia);
               
                
                // Registro el cambio de metodo de los activos que estan ya dado de alta (Trazabilidad) 

               List<CambiarMetodo> ListaCambioMetodo = (List<CambiarMetodo>)Session["ListaCambioMetodo"];
           
               foreach (CambiarMetodo item in ListaCambioMetodo.Where(w => w.IdsubComponente == IdSubComponete))
               {
          
                           sentencia = "INSERT "
                           + "INTO "
                           + "    cambiometododepreciacion "
                           + "    ( "
                           + "        Idactivo, "
                           + "        IdComponente, "
                           + "        IdMetodoDepreciacion, "
                           + "        VidaUtil, "
                           + "        VidaRemanente, "
                           + "        Vidautilizada, "
                           + "        Fecha "
                           + "    ) "
                           + "    VALUES "
                           + "    ( "
                           + "        " + item.IdActivo + ", "
                           + "        " + item.IdComponente + ", "
                           + "        " + item.IDdepreciacion + ", "
                           + "        " + item.vida_util+", "
                           + "        " + item.vida_util_remanente + ", "
                           + "        " + item.vida_util_utilizado + ", "
                           + "        '" + Fecha + "' "
                           + "    )";
                            sql.Add(sentencia);

               }

               conexion.EjecutarTransaccion(sql);

               X.MessageBox.Show(new MessageBoxConfig
               {
                   Title = " Grabación Exitosa ",
                   Message = "Cambio de Metodo realizado Correctamente!",
                   Buttons = MessageBox.Button.OK,
                   Icon = MessageBox.Icon.INFO,
                   Closable = true
               });
            }
            catch (Exception)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = " Error!.. ",
                    Message = "Ha ocurrido un error durante la Transacción!",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR,
                    Closable = true
                });
               
            }

            
            
        
        }

    }
}