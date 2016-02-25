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
    public partial class CentroEconomico : SigcWeb.API.Shared.Page
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!X.IsAjaxRequest)
            {
                this.cargargrilla();
            }
        }

        [DirectMethod(ShowMask = true, Msg = "Guardando ...", Target = MaskTarget.Page)]
        public void Guardar()
        {
            try
            {
                        Boolean f = CheckMenuItem_Activo.Checked;
                        Conexion_Mysql conexion = new Conexion_Mysql();
                        if (f==true)
                        {
                            conexion.EjecutarOperacion(string.Format("INSERT INTO tb_centro_economico(Centro_Economico,codigo,estado,login,FyH_Real) "
                                                                     + " VALUES('{0}','{1}','A','{2}',now())", txt_economico.Text, txt_codigo.Text, 
                                                                     HttpContext.Current.User.Identity.Name.ToUpper()));
                            X.Msg.Notify("CREADO CENTRO ECONOMICO", "SE CREO EL CENTRO ECONOMICO : " + txt_economico.Text).Show();
                        }
                        else
                        {
                            conexion.EjecutarOperacion(string.Format("INSERT INTO tb_centro_economico(Centro_Economico,codigo,estado,login,FyH_Real) " 
                                                                    + " VALUES('{0}','{1}','I','{2}',now())", txt_economico.Text, txt_codigo.Text, 
                                                                    HttpContext.Current.User.Identity.Name.ToUpper()));
                            X.Msg.Notify("CREADO CENTRO ECONOMICO", "SE CREO EL CENTRO ECONOMICO : " + txt_economico.Text).Show();
                        }

                        txt_economico.Reset();
                        txt_codigo.Reset();
                        cargargrilla();
                 
      
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
        [DirectMethod(ShowMask = true, Msg = "Cargando ...", Target = MaskTarget.Page)]
        public void Actualizar() {
            try
            {
                 Boolean f = CheckMenuItem_Activo.Checked;
                 Conexion_Mysql conexion = new Conexion_Mysql();
                if (f==true)
	            {

                    conexion.EjecutarOperacion(string.Format("UPDATE tb_centro_economico SET Centro_Economico='{0}', estado='A',login='{1}',codigo='{2}' "
                                                            + " WHERE id_centro='{3}'", txt_economico.Text, HttpContext.Current.User.Identity.Name.ToUpper(),
                                                            txt_codigo.Text, idcentroEconomico.Text));
                }
                else
                {
                    conexion.EjecutarOperacion(string.Format("UPDATE tb_centro_economico SET Centro_Economico='{0}', estado='I',login='{1}',codigo='{2}' " 
                                                            + " WHERE id_centro='{3}'", txt_economico.Text, HttpContext.Current.User.Identity.Name.ToUpper(),
                                                            txt_codigo.Text,idcentroEconomico.Text));
                }
                btn_actualizar.Hidden = true;
                btn_guardar.Hidden = false;
                FormPanel1.Reset();
                cargargrilla();
            }
            catch (Exception e)
            {
                
               
            }
        }
        [DirectMethod(ShowMask = true, Msg = "Cargando ...", Target = MaskTarget.Page)]
        public void CargarDatos(string id) {
            try
            {
                
                Conexion_Mysql conexion = new Conexion_Mysql();
                DataSet dt = conexion.EjecutarSelectMysql(string.Format("SELECT Centro_Economico as economico,estado,codigo FROM tb_centro_economico "
                                                                        + " WHERE id_centro='{0}'", id));
                txt_economico.Text = dt.Tables[0].Rows[0]["economico"].ToString();
                txt_codigo.Text = dt.Tables[0].Rows[0]["codigo"].ToString();
                idcentroEconomico.Text = id;
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
        [DirectMethod(ShowMask = true, Msg = "Eliminando ...", Target = MaskTarget.Page)]
        public void Eliminareconomico(string id)
        {
            try
            {
                Conexion_Mysql conexion = new Conexion_Mysql();
                conexion.EjecutarOperacion(string.Format("DELETE FROM tb_centro_economico WHERE id_centro = '{0}'", id));

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
            cargargrilla();
        }
       

        [DirectMethod]
        public void cargargrilla()
        {
            Conexion_Mysql conexion = new Conexion_Mysql();
            DataSet dt = conexion.EjecutarSelectMysql(string.Format("SELECT Centro_Economico as economico,id_centro as idcentro,codigo,estado FROM tb_centro_economico"));
            store_economico.DataSource = dt;
            store_economico.DataBind();

        }
    }
}