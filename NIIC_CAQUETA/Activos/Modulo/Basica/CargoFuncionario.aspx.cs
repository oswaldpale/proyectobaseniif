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
    public partial class CargoFuncionario : SigcWeb.API.Shared.Page
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
     //       base.Page_Load(sender, e);
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
                    conexion.EjecutarOperacion(string.Format("INSERT INTO tipoempleado(Tipo,estado,codigo,login,FyH_Real)"
                                                             +  " VALUES('{0}','A','{1}','{2}',now())", txt_cargo.Text.ToUpper(), txt_codigo.Text, 
                                                             HttpContext.Current.User.Identity.Name.ToUpper()));
                    X.Msg.Notify("CREADO CARGO FUNCIONARIO", "SE CREO EL CARGO : " + txt_cargo.Text.ToUpper()).Show();
                }
                else
                {
                    conexion.EjecutarOperacion(string.Format("INSERT INTO tb_centro_costo(Tipo,estado,codigo,login,FyH_Real)" 
                                                             + " VALUES('{0}','I','{1}','{2}',now())", txt_cargo.Text.ToUpper(), 
                                                             txt_codigo.Text, HttpContext.Current.User.Identity.Name.ToUpper()));
                    X.Msg.Notify("CREADO CARGO FUNCIONARIO", "SE CREO EL CARGO : " + txt_cargo.Text.ToUpper()).Show();
                }
                cargargrilla();
                txt_cargo.Reset();
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
                    conexion.EjecutarOperacion(string.Format("UPDATE tipoempleado SET Tipo='{0}',estado='A',codigo='{1}',login='{2}'"
                                                            + " WHERE cod_tipo='{3}'", txt_cargo.Text.ToUpper(), txt_codigo.Text, 
                                                            HttpContext.Current.User.Identity.Name.ToUpper(), idcargo.Text));
                }
                else
                {
                    conexion.EjecutarOperacion(string.Format("UPDATE tipoempleado SET Tipo='{0}',estado='I',codigo='{1}',login='{2}' "  
                                                            + " WHERE cod_tipo='{3}'", txt_cargo.Text.ToUpper(), txt_codigo.Text, 
                                                            HttpContext.Current.User.Identity.Name.ToUpper(), idcargo.Text));
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
        [DirectMethod(ShowMask = true, Msg = "Cargando ...", Target = MaskTarget.Page)]
        public void CargarDatos(string id)
        {
            try
            {

                Conexion_Mysql conexion = new Conexion_Mysql();

                DataSet dt = conexion.EjecutarSelectMysql(string.Format("SELECT Tipo as cargo,estado,codigo FROM tipoempleado "
                                                                        + " WHERE cod_tipo='{0}'", id));
                txt_cargo.Text = dt.Tables[0].Rows[0]["cargo"].ToString();
                txt_codigo.Text = dt.Tables[0].Rows[0]["codigo"].ToString();
                idcargo.Text = id;
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
            DataSet dt = conexion.EjecutarSelectMysql(string.Format("SELECT cod_tipo as idcargo,estado,Tipo as cargo,codigo FROM tipoempleado"));
            store_cargo.DataSource = dt;
            store_cargo.DataBind();

        }
    }
}