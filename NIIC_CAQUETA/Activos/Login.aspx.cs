using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Activos.Clases;
using Ext.Net;

namespace Activos
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (Session.IsNewSession && Request["reLogin"] != null)
                {
                    txtUsuario.Text = Request["reLogin"].ToString();
                    txtPassword.Text = Global._user[Request["reLogin"].ToString()];

                    Global._user.Remove(Request["reLogin"].ToString());
                    this.btnIngresar_Click();
                }

                if (Request["Session"] != null)
                {
                    Session["OldSession"] = Session["Login"].ToString();
                    this.UpdateSession("Relogueo");
                }
            }
            catch (Exception exc)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Sigc_net. #_Error.",
                    Message = "Error: " + exc.Message + exc.StackTrace,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING,
                    Closable = false,
                });
            }
        }

        [DirectMethod(ShowMask = true, Msg = "Cargando Configuración ...", Target = MaskTarget.Page)]
        public void btnIngresar_Click()
        {
            try
            {
                Conexion_Mysql connect = new Conexion_Mysql();
                string sql = "select Login,Identificacion,Nombre,SessionId from usuarios where Login='{0}' and AES_DECRYPT(Password,'SIGC')='{1}' ";
                DataTable tb = connect.EjecutarSelectMysql(string.Format(sql, txtUsuario.Text.ToUpper(), txtPassword.Text)).Tables[0];

                if (tb.Rows.Count > 0)
                {
                    sql = "select Login from usuarios where SessionId='{0}' ";
                    DataTable tbSession = connect.EjecutarSelectMysql(string.Format(sql, Session.SessionID)).Tables[0];

                    if (tbSession.Rows.Count < 1 && string.IsNullOrEmpty(tb.Rows[0]["SessionId"].ToString()))
                    {
                        Session["Login"] = txtUsuario.Text.ToUpper();
                        Session["Nombre"] = tb.Rows[0]["Nombre"];
                        Session["Identificacion"] = tb.Rows[0]["Identificacion"];

                        sql = "update usuarios set SessionId='{0}' where Login='{1}' ";
                        connect.EjecutarOperacion(string.Format(sql, Session.SessionID, txtUsuario.Text.Trim()));
                        Response.Redirect("Default.aspx");
                    }
                    else
                    {
                        Session["OldSession"] = !string.IsNullOrEmpty(tb.Rows[0]["SessionId"].ToString()) ? tb.Rows[0]["Login"] : tbSession.Rows[0]["Login"];
                        this.UpdateSession("Logueo");
                    }
                }

                throw new Exception("Usuario y/o Contraseña no son correctos.");
            }
            catch (Exception exc)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Sigc_net. #_Error.",
                    Message = "Error: " + exc.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING,
                    Closable = false,
                    Modal = true,
                });
            }
        }

        private void UpdateSession(string estado)
        {
            Conexion_Mysql connect = new Conexion_Mysql();
            string sql = "update usuarios set SessionId=null where Login='{0}' ";
            connect.EjecutarOperacion(string.Format(sql, Session["OldSession"]));

            Session.Clear();
            Session.Abandon();

            HttpCookie myCookie = new HttpCookie("ASP.NET_SessionId");
            myCookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(myCookie);

            if (estado.Equals("Logueo"))
            {
                Global._user.Add(txtUsuario.Text.ToUpper(), txtPassword.Value.ToString());
                Response.Redirect("Login.aspx?reLogin=" + txtUsuario.Text.ToUpper() + "");
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

    }
}