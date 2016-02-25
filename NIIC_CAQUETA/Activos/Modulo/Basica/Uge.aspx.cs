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
    public partial class Uge : SigcWeb.API.Shared.Page
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            //base.Page_Load(sender, e);
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
                Conexion_Mysql conexion = new Conexion_Mysql();
                Boolean f = CheckMenuItem_Activo.Checked;
                if (f == true)
                {
                    conexion.EjecutarOperacion(string.Format("INSERT INTO tb_uge(Unidad_g_Efectivo,codigo,estado,login,FyH_Real) "
                                                            + " VALUES('{0}','{1}','A','{2}',now())", txt_uge.Text.ToUpper(), 
                                                            txt_codigo.Text, HttpContext.Current.User.Identity.Name.ToUpper()));
                }
                else
                {
                    conexion.EjecutarOperacion(string.Format("INSERT INTO tb_uge(Unidad_g_Efectivo,codigo,estado,login,FyH_Real) "
                                                            + " VALUES('{0}','{1}','I','{2}',now())", txt_uge.Text.ToUpper(), 
                                                            txt_codigo.Text, HttpContext.Current.User.Identity.Name.ToUpper()));
                }
                X.Msg.Notify("CREADO UNIDAD GENERADORA EFECTIVO", "SE CREO EL UGE : " + txt_uge.Text.ToUpper()).Show();
                cargargrilla();
                txt_uge.Reset();
                txt_codigo.Reset();

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
        public void Eliminaruge(string id)
        {
            try
            {
                Conexion_Mysql conexion = new Conexion_Mysql();
                conexion.EjecutarOperacion(string.Format("DELETE FROM tb_uge WHERE id_uge = '{0}'", id));

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
        public void Actualizar()
        {
            try
            {
                Boolean f = CheckMenuItem_Activo.Checked;
                Conexion_Mysql conexion = new Conexion_Mysql();
                if (f == true)
                {
                    conexion.EjecutarOperacion(string.Format("UPDATE tb_uge SET Unidad_g_Efectivo='{0}',codigo='{1}',estado='A',login='{2}' "
                                                            + "WHERE id_uge='{3}'", txt_uge.Text.ToUpper(), txt_codigo.Text.ToUpper(),
                                                            HttpContext.Current.User.Identity.Name.ToUpper(), iduge.Text));
                }
                else
                {
                    conexion.EjecutarOperacion(string.Format("update tb_uge set Unidad_g_Efectivo='{0}',codigo='{1}',estado='I',login='{2}' "
                                                            + " WHERE id_uge='{3}'", txt_uge.Text.ToUpper(), txt_codigo.Text.ToUpper(), 
                                                            HttpContext.Current.User.Identity.Name.ToUpper() , iduge.Text));
                }
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
        [DirectMethod]
        public void CargarDatos(string id)
        {
            try
            {

                Conexion_Mysql conexion = new Conexion_Mysql();

                DataSet dt = conexion.EjecutarSelectMysql(string.Format("SELECT Unidad_g_Efectivo as uge,estado,codigo FROM tb_uge where id_uge='{0}'", id));
                txt_uge.Text = dt.Tables[0].Rows[0]["uge"].ToString();
                txt_codigo.Text = dt.Tables[0].Rows[0]["codigo"].ToString();
                iduge.Text = id;
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
            DataSet dt = conexion.EjecutarSelectMysql(string.Format("SELECT Unidad_g_Efectivo as uge,id_uge as iduge,codigo,estado FROM tb_uge"));
            store_uge.DataSource = dt;
            store_uge.DataBind();

        }
    }
}