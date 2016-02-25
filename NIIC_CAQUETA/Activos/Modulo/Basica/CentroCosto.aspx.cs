using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Activos.Clases;
using Ext.Net;

namespace Activos.Modulo
{
    public partial class CentroCosto : SigcWeb.API.Shared.Page
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
           base.Page_Load(sender, e);
            if (!X.IsAjaxRequest)
            {
                Conexion_Mysql conexion = new Conexion_Mysql();
                DataSet dt =  conexion.EjecutarSelectMysql("SELECT id_grupo,descripcion FROM tb_grupo_centro_costo WHERE estado='A'");
                store_grupocc.DataSource = dt;
                store_grupocc.DataBind();
                this.cargargrilla();
            }
        }

        [DirectMethod(ShowMask = true, Msg = "Guardando ...", Target = MaskTarget.Page)]
        public void Guardar(Newtonsoft.Json.Linq.JArray record)
        {
            
            try
            {
                Boolean ing = true;
                for (int i = 0; i < record.Count; i++)
                {
                 
                    if (record[i]["codigo"].ToString().Equals(txt_codigo.Text))
                    {
                        ing = false;
                        throw new Exception("Ya se encuentra un registro con este codigo");
                       
                     
                    }
                    if (record[i]["costo"].ToString().Equals(txt_costo.Text.ToString().Trim()))
                    {
                        ing = false;
                        throw new Exception("Ya se encuentra un registro con este nombre de Centro de Costo");
                        
                    }
                }
                if (ing == true)
	            {
                    string idgrupo = cbx_grupocc.SelectedItem.Value.ToString(); 
                    Boolean f = CheckMenuItem_Activo.Checked;
                    Conexion_Mysql conexion = new Conexion_Mysql();
                    if (f == true)
                    {
                        conexion.EjecutarOperacion(string.Format("INSERT INTO tb_centro_costo(Centro_costo,estado,codigo,id_grupo_CC,login,FyH_Real)"
                                                                 + " VALUES('{0}','A','{1}','{2}','{3}',now())", txt_costo.Text, txt_codigo.Text,
                                                                 idgrupo, HttpContext.Current.User.Identity.Name.ToUpper()));
                        X.Msg.Notify("CREADO CENTRO COSTO", "SE CREO EL CENTRO COSTO : " + txt_costo.Text).Show();
                    }
                    else
                    {
                        conexion.EjecutarOperacion(string.Format("INSERT INTO tb_centro_costo(Centro_costo,estado,codigo,id_grupo_CC,login,FyH_Real) "
                                                                + " VALUES('{0}','I','{1}','{2}','{3}',now())", txt_costo.Text, txt_codigo.Text,
                                                                idgrupo, HttpContext.Current.User.Identity.Name.ToUpper()));
                        X.Msg.Notify("CREADO CENTRO COSTO", "SE CREO EL CENTRO COSTO : " + txt_costo.Text).Show();
                    }
                    cargargrilla();
                    txt_costo.Reset();
                    txt_codigo.Reset();
                    cbx_grupocc.Reset();
	            }
                
            }
            catch (Exception s)
            {

                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "ActiFijo.",
                    Message = "Error: " + s.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR,
                    Closable = false
                });
            }
        }
        [DirectMethod(ShowMask = true, Msg = "Eliminando ...", Target = MaskTarget.Page)]
        public void Eliminarcosto(string id)
        {
            try
            {
                FormPanel1.Reset();
                Conexion_Mysql conexion = new Conexion_Mysql();
                conexion.EjecutarOperacion(string.Format("DELETE FROM tb_centro_costo WHERE id_centro = '{0}'", id));

            }
            catch (Exception s)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "ActiFijo.",
                    Message = "Error: Otra tabla estas haciendo uso de este registro" + s.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR,
                    Closable = false
                });
            }
            
        }
             
        [DirectMethod(ShowMask = true, Msg = "Cargando ...", Target = MaskTarget.Page)]
        public void Actualizar()
        {
            try
            {
                string idgrupo = cbx_grupocc.SelectedItem.Value.ToString(); 
                Boolean f = CheckMenuItem_Activo.Checked;
                Conexion_Mysql conexion = new Conexion_Mysql();
                if (f == true)
                {
                 conexion.EjecutarOperacion(string.Format("UPDATE tb_centro_costo SET "                                                                                                     + " Centro_costo='{0}',id_grupo_CC='{1}',estado='A',codigo='{2}',login='{3}' "
                                                            + " WHERE id_centro='{4}'", txt_costo.Text, idgrupo,txt_codigo.Text, 
                                                            HttpContext.Current.User.Identity.Name.ToUpper(), idcosto.Text));
                }
                else
                {
                 conexion.EjecutarOperacion(string.Format("UPDATE tb_centro_costo SET " 
                                                            + " Centro_costo='{0}',id_grupo_CC='{1}',estado='I',codigo='{2}',login='{3}' "
                                                            + " WHERE id_centro='{4}'", txt_costo.Text, idgrupo, txt_codigo.Text,
                                                            HttpContext.Current.User.Identity.Name.ToUpper(), idcosto.Text));
                }
                RowSelectionModel sm = this.GridPanel1.GetSelectionModel() as RowSelectionModel;
                sm.ClearSelection();
                btn_actualizar.Hidden = true;
                btn_guardar.Hidden = false;
                FormPanel1.Reset();
                cargargrilla();
            }
            catch (Exception e)
            {

                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "ActiFijo.",
                    Message = "Error:" + e.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR,
                    Closable = false
                });
            }

        }
        [DirectMethod(ShowMask = true, Msg = "Cargando ...", Target = MaskTarget.Page)]
        public void CargarDatos(string id)
        {
            try
            {

                Conexion_Mysql conexion = new Conexion_Mysql();

                DataSet dt = conexion.EjecutarSelectMysql(string.Format("SELECT Centro_costo as costo,estado,codigo,id_grupo_CC" 
                                                                        + " FROM tb_centro_costo "
                                                                        + " WHERE id_centro='{0}' ", id));
                txt_costo.Text = dt.Tables[0].Rows[0]["costo"].ToString();
                txt_codigo.Text = dt.Tables[0].Rows[0]["codigo"].ToString();
                int idgrupo = Convert.ToInt32(dt.Tables[0].Rows[0]["id_grupo_CC"].ToString());
                cbx_grupocc.SetValue(idgrupo);
                idcosto.Text = id;
                btn_guardar.Hidden = true;
                btn_actualizar.Hidden = false;
                if (dt.Tables[0].Rows[0]["estado"].ToString().Equals("A")) { CheckMenuItem_Activo.SetChecked(true); } else { CheckMenuItem_Inactivo.SetChecked(true); }
            }
            catch (Exception s)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "ActiFijo.",
                    Message = "Error: " + s.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR,
                    Closable = false
                });

            }
        }
        [DirectMethod]
        public void cargargrilla()
        {
            Conexion_Mysql conexion = new Conexion_Mysql();
            DataSet dt = conexion.EjecutarSelectMysql(string.Format("SELECT c.Centro_costo as costo,c.codigo,c.estado,c.id_centro as "                                                           +" idcosto,c.id_grupo_CC,gcc.descripcion as grupo_Ccosto FROM tb_centro_costo c "
                                                                + " INNER JOIN tb_grupo_centro_costo gcc ON c.id_grupo_CC=gcc.id_grupo ORDER BY                             c.codigo"));

            store_costo.DataSource = dt;
            store_costo.DataBind();

        }
    }
}