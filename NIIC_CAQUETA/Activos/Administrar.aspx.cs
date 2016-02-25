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
    public partial class Administrar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                Conexion_Mysql connect = new Conexion_Mysql();
                //** Usuarios
                string sql = "select login,nombre from usuarios where eliminado=0";
                data_User.GetStore().DataSource = connect.EjecutarSelectMysql(sql);
                data_User.GetStore().DataBind();

                //** Modulos
                sql = "select codigo,nombre from tb_menu where id_padre is null";
                data_Modulo.GetStore().DataSource = connect.EjecutarSelectMysql(sql);
                data_Modulo.GetStore().DataBind();
            }
        }

        [DirectMethod(ShowMask = true, Target = MaskTarget.CustomTarget, CustomTarget = "Panel3", Msg = "Cargando Formularios ...")]
        public void data_Formularios_Load(Newtonsoft.Json.Linq.JObject recordModulo)
        {
            try
            {
                Ext.Net.TreePanel tree = new Ext.Net.TreePanel();

                tree.ID = "TreePanel1";
                tree.Width = Unit.Pixel(300);
                tree.Height = Unit.Pixel(450);
                //tree.Icon = Icon.BookOpen;
                //tree.Title = "Catalog";
                tree.AutoScroll = true;
                tree.RootVisible = false;
                tree.Border = false;
                tree.FolderSort = true;
                tree.UseArrows = true;
                tree.Border = false;

                Ext.Net.Button btnExpand = new Ext.Net.Button();
                btnExpand.Text = "Expadir Todo";
                btnExpand.Scale = ButtonScale.Medium;
                btnExpand.Icon = Icon.ControlAddBlue;
                btnExpand.Listeners.Click.Handler = tree.ClientID + ".expandAll();";

                Ext.Net.Button btnCollapse = new Ext.Net.Button();
                btnCollapse.Text = "Contraer Todo";
                btnCollapse.Scale = ButtonScale.Medium;
                btnCollapse.Icon = Icon.ControlRemoveBlue;
                btnCollapse.Listeners.Click.Handler = tree.ClientID + ".collapseAll();";

                Ext.Net.Button btnSave = new Ext.Net.Button();
                btnSave.Text = "Guardar";
                btnSave.Scale = ButtonScale.Medium;
                btnSave.Icon = Icon.ControlAdd;
                btnSave.Listeners.Click.Handler = "detectedCkeckeds();";

                Toolbar toolBar = new Toolbar();
                toolBar.ID = "Toolbar1";
                toolBar.Items.Add(btnExpand);
                toolBar.Items.Add(btnCollapse);
                toolBar.Items.Add(new ToolbarFill());
                toolBar.Items.Add(btnSave);
                tree.TopBar.Add(toolBar);

                tree.Listeners.ItemClick.Handler = "getFormularios(record);";

                //***
                //tree.Fields.Add(new ModelField("formulario"));
                //tree.Fields.Add(new ModelField("panel", ModelFieldType.Boolean));

                //tree.ColumnModel.Columns.Add(new TreeColumn[]{
                //    new TreeColumn() { Text = "Formularios", DataIndex = "formulario", Sortable = true, Flex = 2 },
                //});
                //tree.ColumnModel.Columns.AddRange(new CheckColumn[]{
                //    new CheckColumn() { Text = "Panel", DataIndex = "panel", Editable = true, Sortable = false, Align = Alignment.Center, Flex = 1 },
                //});
                //***

                Ext.Net.Node root = new Ext.Net.Node()
                {
                    Text = "Forms",
                    Expanded = true,
                };

                tree.Root.Add(root);

                Conexion_Mysql connect = new Conexion_Mysql();
                string sql = "select codigo,nombre,id_padre from tb_menu where codigo={0}";
                DataSet dt = connect.EjecutarSelectMysql(string.Format(sql, recordModulo["codigo"]));

                foreach (DataRow row in dt.Tables[0].Rows)
                {
                    Ext.Net.Node menu = new Ext.Net.Node()
                    {
                        NodeID = "mod_" + row["codigo"].ToString(),
                        Text = row["nombre"].ToString(),
                        Icon = Icon.Folder,
                        Checked = false,
                    };

                    //menu.CustomAttributes.AddRange(new ConfigItem[]{
                    //    new ConfigItem("formulario", row["nombre"].ToString(), ParameterMode.Value),
                    //    new ConfigItem("panel", "false", ParameterMode.Value),
                    //});

                    root.Children.Add(menu);

                    if (!this.PagesLoad(menu, row["codigo"].ToString()))
                    {
                        root.Children.Remove(menu);
                    }
                }

                tree.Render(this.Panel3);
                X.Js.AddScript("var select = App.data_User.getSelectionModel().getSelection(); if(select.length > 0) { App.direct.data_User_Load(App.data_User.getSelectionModel().getSelection()[0].data); }");
            }
            catch (Exception exc)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Sigc .",
                    Message = "Error: " + exc.Message + exc.StackTrace,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR,
                    Closable = false
                });
            }
        }

        private bool PagesLoad(Ext.Net.Node leaf, string id)
        {
            Conexion_Mysql connect = new Conexion_Mysql();
            string sql = "select * from (select codigo,nombre,url,ancho,alto,panel,cod_submenu from tb_pagina pa "
                + "where cod_submenu={0} union all select codigo,nombre,'',0,0,0,0 from tb_menu where id_padre={0}) v order by url";

            DataTable tb = connect.EjecutarSelectMysql(string.Format(sql, id)).Tables[0];

            if (tb.Rows.Count > 0)
            {
                DataRow[] foundRows = tb.Select("ancho > 0");

                if (foundRows.Length > 0)
                {
                    foreach (DataRow row in tb.Rows)
                    {
                        Ext.Net.Node subleaf;

                        if (Convert.ToInt32(row["ancho"].ToString()) == 0)
                        {
                            subleaf = new Node()
                            {
                                NodeID = "sub_" + row["codigo"],
                                Text = row["nombre"].ToString(),
                                Icon = Icon.Folder,
                                Checked = false,
                            };

                            leaf.Children.Add(subleaf);

                            if (!this.PagesLoad(subleaf, row["codigo"].ToString()))
                            {
                                leaf.Children.Remove(subleaf);
                            }
                        }
                        else
                        {
                            subleaf = new Node()
                            {
                                NodeID = "win_" + row["codigo"],
                                Text = row["nombre"].ToString(),
                                Icon = Icon.ApplicationForm,
                                Checked = false,
                                Leaf = true,
                            };

                            leaf.Children.Add(subleaf);
                        }
                    }
                    return true;
                }
                return true;
            }
            return false;
        }
        [DirectMethod(ShowMask = true, Target = MaskTarget.CustomTarget, CustomTarget = "Panel3", Msg = "Cargando Formularios ...")]
        public void data_User_Load(Newtonsoft.Json.Linq.JObject recordUser)
        {
            if (recordUser.Count < 1) return;

            try
            {
                varUser.Text = recordUser["login"].ToString();
                Conexion_Mysql connect = new Conexion_Mysql();
                //string sql = "select concat('win_',u.pagina) as codpage,p.nombre as page,if(sub.id_padre is null,concat('mod_',sub.codigo),concat('sub_',sub.codigo)) as codsub,sub.nombre as sub "
                //    + "from tb_usuariopagina u inner join tb_pagina p on u.pagina=p.codigo left join tb_menu sub on p.cod_submenu=sub.codigo "
                //    + "where u.login='ADMIN'";
                string sql = "select u.pagina as codpage,p.nombre as page,sub.codigo as codsub,sub.nombre as sub,sub.id_padre "
                    + "from tb_usuariopagina u inner join tb_pagina p on u.pagina=p.codigo left join tb_menu sub on p.cod_submenu=sub.codigo "
                    + "where login='{0}'";
                DataTable tb = connect.EjecutarSelectMysql(string.Format(sql, varUser.Text)).Tables[0];

                Newtonsoft.Json.Linq.JArray user = new Newtonsoft.Json.Linq.JArray();
                Newtonsoft.Json.Linq.JObject obj;
                string x = string.Empty;

                foreach (DataRow item in tb.Rows)
                {
                    obj = new Newtonsoft.Json.Linq.JObject();
                    obj.Add("codpage", "win_" + item["codpage"]);
                    obj.Add("page", item["page"].ToString());
                    obj.Add("codsub", !string.IsNullOrEmpty(item["id_padre"].ToString()) ? "sub_" + item["codsub"] : "mod_" + item["codsub"]);
                    obj.Add("sub", item["sub"].ToString());

                    user.Add(obj);
                }

                X.Js.Call("UserLoad", user);
            }
            catch (Exception exc)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Sigc .",
                    Message = "Error: " + exc.Message + exc.StackTrace,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR,
                    Closable = false
                });
            }
        }

        [DirectMethod(ShowMask = true, Target = MaskTarget.CustomTarget, CustomTarget = "Panel3", Msg = "Cargando Formularios ...")]
        public void btnGuardar_Click(Newtonsoft.Json.Linq.JArray record, Newtonsoft.Json.Linq.JObject user)
        {
            try
            {
                Conexion_Mysql connect = new Conexion_Mysql();
                string sql = "select u.pagina as codpage,p.nombre as page,sub.codigo as codsub,sub.nombre as sub,if(sub.id_padre is null,sub.codigo,sub.id_padre) as ID "
                    + "from tb_usuariopagina u inner join tb_pagina p on u.pagina=p.codigo left join tb_menu sub on p.cod_submenu=sub.codigo "
                    + "where login='{0}' and if(sub.id_padre is null,sub.codigo,sub.id_padre)={1}";
                DataTable tb = connect.EjecutarSelectMysql(string.Format(sql, varUser.Text, user["codigo"])).Tables[0];

                List<string> p = new List<string>();
                string page = "insert into tb_usuariopagina values('{0}',{1})";

                foreach (Newtonsoft.Json.Linq.JObject item in record)
                {
                    if ((bool)item["leaf"])
                    {
                        DataRow[] r = tb.Select("codpage=" + item["id"].ToString().Substring(4));

                        if (r.Length < 1)
                            p.Add(string.Format(page, varUser.Text, item["id"].ToString().Substring(4)));
                        else
                            tb.Rows.Remove(r[0]);
                    }
                }

                page = "delete from tb_usuariopagina where login='{0}' and pagina={1}";

                foreach (DataRow item in tb.Rows)
                    p.Add(string.Format(page, varUser.Text, item["codpage"]));

                connect.EjecutarTransaccion(p);
            }
            catch (Exception exc)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Sigc .",
                    Message = "Error: " + exc.Message + exc.StackTrace,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR,
                    Closable = false
                });
            }
        }
    }
}