using Activos.Clases;
using Ext.Net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Activos.Modulo
{
    public partial class ReexpresarVidaUtil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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

        protected void GridComodato_SelectSubclase(object sender, DirectEventArgs e)
        {
            TriggerListaSubclase.Text = "(" + e.ExtraParams["Clase"].ToString() + ") " + e.ExtraParams["Nombre"].ToString();
            idsubclase.SetValue(e.ExtraParams["Codigo"].ToString());
            idclase.SetValue(e.ExtraParams["IdClase"].ToString());
            CargarGrillaReexpresion(e.ExtraParams["Codigo"].ToString());
            CargarCbxComponente(e.ExtraParams["Codigo"].ToString());
        }

        private void CargarGrillaReexpresion(string IdSubclase)
        {
          Conexion_Mysql conexion = new Conexion_Mysql();
          String sql = "  SELECT "
                              + "    a.idActivo                        AS IdActivo, "
                              + "    CONCAT('(',a.placa,') ',a.Nombre) AS Activo, "
                              + "    ac.id_Componente                  AS IdComponente, "
                              + "    ac.sub_compID                  AS IdsubComponente, "
                              + "    UPPER(c.Componente) AS nombre_componente,  "
                              + "    CONCAT(ac.Porcentaje_ci,'%')      AS Porcentaje_ci, "
                              + "    d.Depreciacion, "
                              + "    ac.vida_util, "
                              + "    ac.vida_util_utilizado, "

                              + "    IF(ac.vida_util_remanente IS NULL, ac.vida_util,ac.vida_util_remanente)  AS vida_util_remanente,  "
                              + "    IF(ac.reexpresionVidaUtil IS NULL,'0',ac.reexpresionVidaUtil)  AS reexpresionVidaUtil  "
                              + "FROM "
                              + "    activo a "
                              + "INNER JOIN activo_componente ac "
                              + "ON "
                              + "    a.idActivo = ac.id_activo AND ac.idtiponorma ='1' "
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
                              + "ORDER BY a.idActivo,ac.id_Componente,ac.id_tipo_depreciacion";
          DataSet dt = conexion.EjecutarSelectMysql(sql);
          List<ReexpresarRemanente>  ListaRexpresarRemanente = new List<ReexpresarRemanente>();
         
          foreach (DataRow item in dt.Tables[0].Rows)
          {
              ReexpresarRemanente Lista = new ReexpresarRemanente();
              Lista.IdActivo = item["IdActivo"].ToString();
              Lista.Activo = item["Activo"].ToString();
              Lista.IdComponente = item["IdComponente"].ToString();
              Lista.IdsubComponente = item["IdsubComponente"].ToString();
              Lista.nombre_componente = item["nombre_componente"].ToString();
              Lista.Porcentaje_ci =  item["Porcentaje_ci"].ToString();
              Lista.Depreciacion =  item["Depreciacion"].ToString();
              Lista.vida_util = item["vida_util"].ToString();
              Lista.vida_util_utilizado = item["vida_util_utilizado"].ToString();
              Lista.vida_util_remanente = float.Parse(item["vida_util_remanente"].ToString());
              Lista.reexpresionVidaUtil = float.Parse(item["reexpresionVidaUtil"].ToString());
              ListaRexpresarRemanente.Add(Lista);
          }
          if (ListaRexpresarRemanente.Count>0)
          {
              Session["ListaRexpresarRemanente"] = ListaRexpresarRemanente;
              store_CambioVidaTemp.DataSource = ListaRexpresarRemanente;
              store_CambioVidaTemp.DataBind();
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
              gridCambioVidaTemp.GetStore().RemoveAll();
          }
          
        }
        private void CargarCbxComponente(string IdSubclase)
        {
            Conexion_Mysql conexion = new Conexion_Mysql();
            String sql =  "SELECT "
                            + "    ac.sub_compID    AS IdComponente, "
                            + "    UPPER(CONCAT('(',c.id_subclase,') ',c.Componente)) AS nombre_componente "
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

        [DirectMethod]
        public void AplicarAjuste(string idcomponente, string cantidad)
        {
         List<ReexpresarRemanente>  ListaRexpresarRemanente = (List<ReexpresarRemanente>) Session["ListaRexpresarRemanente"];
         foreach (ReexpresarRemanente item in ListaRexpresarRemanente.Where(w => w.IdsubComponente == idcomponente))
         {
             item.reexpresionVidaUtil =  item.reexpresionVidaUtil + float.Parse(cantidad);
             item.vida_util_remanente = item.vida_util_remanente + item.reexpresionVidaUtil;
         }
         Session["ListaRexpresarRemanente"] = ListaRexpresarRemanente;
         store_CambioVidaTemp.DataSource = ListaRexpresarRemanente;
         store_CambioVidaTemp.DataBind();

        }
        [DirectMethod]
        public void Guardar()
        {
            try
            {
           
                List<ReexpresarRemanente> ListaRexpresarRemanente = (List<ReexpresarRemanente>)Session["ListaRexpresarRemanente"];
                Conexion_Mysql conexion = new Conexion_Mysql();
                List<String> setencias = new List<String>();
                foreach (ReexpresarRemanente item in ListaRexpresarRemanente)
                {
                    String sql =  "UPDATE "
                    + "    activos_fijos.activo_componente c "
                    + "SET "
                    + "    c.reexpresionVidaUtil ='" + item.reexpresionVidaUtil + "' "
                    + "WHERE "
                    + "    c.id_Componente='" + item.IdComponente + "' "
                    + "AND c.id_activo = '" + item.IdActivo + "'";
                    setencias.Add(sql);
                }
                conexion.EjecutarTransaccion(setencias);
                Session.Remove("ListaRexpresarRemanente");
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Cambio Realizado!",
                    Message = "Cambio Realizado Correctamente",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.INFO,
                    Closable = true
                });


            }
            catch (Exception )
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Error! Reexpresion de la Vida Util",
                    Message = "La vida Remanente no puede ser Negativo ",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.INFO,
                    Closable = true
                });
               
            }
           
        
        }
    }
}