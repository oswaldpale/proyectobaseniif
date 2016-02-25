using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Activos.Clases;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace Activos
{
    public partial class InicioActivoFijos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           Global.path = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.LastIndexOf("/"));
            this.isValidUser();

            if (!X.IsAjaxRequest)
            {
                HttpCookie cookie = new HttpCookie("SigcWeb_SessionID");
                cookie.Expires = DateTime.Now.AddDays(1d);
                Response.Cookies.Add(cookie);

                cookie.Values.Add("Login", Session["Login"].ToString().ToUpper());
                cookie.Values.Add("Nombre", Session["Nombre"].ToString());
                cookie.Values.Add("Identificacion", Session["Identificacion"].ToString());
                cookie.Values.Add("Path", Global.path);

                this.MenuLoad();
                DisplayField1.Text = Session["Nombre"].ToString();
                Hidden1.Text = Session["Login"].ToString();
            }
        }

        protected void btnLogout_Click(object sender, DirectEventArgs e)
        {
            HttpCookie myCookie = new HttpCookie("SigcWeb_SessionID");
            myCookie.Values.Add("Login", null);
            Response.Cookies.Remove("SigcWeb_SessionID");
            Response.Cookies.Add(myCookie);

            Response.Redirect("Login.aspx?Session=Terminado");
        }
        private void MenuLoad()
        {
            try
            {
                Conexion_Mysql connect = new Conexion_Mysql();
                string sql = "select nombre,codigo,id_padre from tb_menu where id_padre is null";
                DataSet dt = connect.EjecutarSelectMysql(sql);

                Ext.Net.MenuItem menuItem = new Ext.Net.MenuItem();

                foreach (DataRow row in dt.Tables[0].Rows)
                {
                    menuItem = new Ext.Net.MenuItem
                    {
                        ID = row["nombre"].ToString().Replace(' ', '_'),
                        Text = row["nombre"].ToString(),
                        Icon = Icon.Folder,
                        HideOnClick = false,
                        Listeners =
                        {
                            Activate =
                            {
                                Handler = "App.direct.isValidUser();",
                            },
                            Click =
                            {
                                Handler = "App.direct.isValidUser();",
                            }
                        },
                    };

                    if (SubMenus(menuItem, row))
                    {
                        Desktop.GetInstance().StartMenu.MenuItems.Add(menuItem);
                    }
                }
            }
            catch (Exception exc)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "App.",
                    Message = exc.Message + exc.StackTrace,
                });
            }
        }

        private bool SubMenus(Ext.Net.MenuItem menuItem, DataRow row)
        {
            try
            {
                Conexion_Mysql connect = new Conexion_Mysql();
                string sql = "select * from (select codigo,nombre,url,ancho,alto from tb_pagina pa inner join tb_usuariopagina usupa on usupa.pagina=pa.codigo "
                    + "where cod_submenu=" + row["codigo"] + " and usupa.login='" + Session["Login"] + "' union all select codigo,nombre,'',0,0 from tb_menu where id_padre=" + row["codigo"] + ") v group by nombre order by url";
                DataSet dt = connect.EjecutarSelectMysql(sql);

                if (dt.Tables[0].Rows.Count < 1)
                    return false;

                Ext.Net.MenuItem item = new Ext.Net.MenuItem();
                Ext.Net.Menu subMenu = new Ext.Net.Menu();

                foreach (DataRow _row in dt.Tables[0].Rows)
                {
                    if (string.IsNullOrEmpty(_row["url"].ToString()))
                    {
                        item = new Ext.Net.MenuItem
                        {
                            ID = "M" + _row["nombre"].ToString().Replace(' ', '_'),
                            Text = _row["nombre"].ToString(),
                            Icon = Ext.Net.Icon.Folder,
                            HideOnClick = false,
                            Listeners =
                            {
                                Activate =
                                {
                                    Handler = "App.direct.isValidUser();",
                                },
                                Click =
                                {
                                    Handler = "App.direct.isValidUser();",
                                }
                            },
                        };

                        if (SubMenus(item, _row))
                            subMenu.Items.Add(item);
                    }
                    else
                    {
                        string win = "DynamicWindow({0},'{1}','{2}','{3}',{4},{5}, false);";
                        string url = Global.path + _row["url"] + "?user=" + Server.UrlEncode(Session.ToString());
                        win = string.Format(win, "#{Desktop1}", "win_" + _row["codigo"], _row["nombre"], Global.path + _row["url"] + "?key=" + HashMD5(Session.SessionID), _row["ancho"], _row["alto"]);

                        item = new Ext.Net.MenuItem
                        {
                            ID = "P" + _row["nombre"].ToString().Replace(' ', '_'),
                            Text = _row["nombre"].ToString(),
                            Icon = Ext.Net.Icon.ApplicationForm,
                            Listeners =
                            {                               
                                Click =
                                {
                                    Handler = win,
                                },
                            },
                        };

                        subMenu.Items.Add(item);
                    }
                }

                if (subMenu.Items.Count > 0)
                {
                    menuItem.Menu.Add(subMenu);
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string HashMD5(string obj)
        {
            return string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(obj)).Select(x => x.ToString("x2")));
        }
        [DirectMethod]
        public void isValidUser()
        {
            Conexion_Mysql connect = new Conexion_Mysql();
            string sql = "Select Login,SessionId from usuarios where Login='" + Session["Login"] + "' ";
            DataTable tb = connect.EjecutarSelectMysql(sql).Tables[0];

            if (Session["Login"] == null || !tb.Rows[0]["SessionId"].ToString().Equals(Session.SessionID))
            {
                Response.Redirect("Login.aspx?Session=false");
            }

        }
        
    }
    
}