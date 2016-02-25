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
    public partial class Propiedad : SigcWeb.API.Shared.Page
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
                if (f == true)
                {
                    conexion.EjecutarOperacion(string.Format("INSERT INTO tb_propiedad(Propiedad,estado,login,FyH_Real) "
                                                            + " VALUES('{0}','A','{1}',now())", txt_propiedad.Text.ToUpper(), 
                                                            HttpContext.Current.User.Identity.Name.ToUpper()));
                    X.Msg.Notify("CREADO TIPO", "SE CREO EL TIPO ACTIVO : " + txt_propiedad.Text.ToUpper()).Show();
                }
                else
                {
                    conexion.EjecutarOperacion(string.Format("INSERT INTO tb_propiedad(Propiedad,estado,login,FyH_Real) "
                                                            + " VALUES('{0}','I','{1}',now())", txt_propiedad.Text.ToUpper(), 
                                                            HttpContext.Current.User.Identity.Name.ToUpper()));
                    X.Msg.Notify("CREADO TIPO", "SE CREO EL TIPO ACTIVO: " + txt_propiedad.Text.ToUpper()).Show();
                }
                cargargrilla();
                txt_propiedad.Reset();

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
        public void CargarDatos(string id)
        {
            try
            {

                Conexion_Mysql conexion = new Conexion_Mysql();

                DataSet dt = conexion.EjecutarSelectMysql(string.Format("SELECT Propiedad as propiedad,estado FROM tb_propiedad " 
                                                                        + " WHERE id_propiedad={0}", id));
                txt_propiedad.Text = dt.Tables[0].Rows[0]["propiedad"].ToString();
                idpropiedad.Text = id;
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

        [DirectMethod(ShowMask = true, Msg = "Cargando ...", Target = MaskTarget.Page)]
        public void Actualizar()
        {
            try
            {
                Boolean f = CheckMenuItem_Activo.Checked;
                Conexion_Mysql conexion = new Conexion_Mysql();
                if (f == true)
                {
                    conexion.EjecutarOperacion(string.Format("UPDATE tb_propiedad set Propiedad='{0}',estado='A',login='{1}' "
                                                            + " WHERE id_propiedad='{2}'", txt_propiedad.Text.ToUpper(), 
                                                            HttpContext.Current.User.Identity.Name.ToUpper(), idpropiedad.Text));
                }
                else
                {
                    conexion.EjecutarOperacion(string.Format("UPDATE tb_propiedad set Propiedad='{0}',estado='I',login='{1}' "  
                                                            + " WHERE id_propiedad='{2}'", txt_propiedad.Text.ToUpper(),
                                                            HttpContext.Current.User.Identity.Name.ToUpper(), idpropiedad.Text));
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
        public void cargargrilla()
        {
            Conexion_Mysql conexion = new Conexion_Mysql();
            DataSet dt = conexion.EjecutarSelectMysql(string.Format("SELECT id_propiedad AS idpropiedad, Propiedad as propiedad,estado "
                                                                    + " FROM tb_propiedad"));
            store_propiedad.DataSource = dt;
            store_propiedad.DataBind();

        }

    }
}