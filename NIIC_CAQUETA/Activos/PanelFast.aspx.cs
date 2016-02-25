using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Data;
using Activos.Clases;

namespace Activos
{
    public partial class PanelFast : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                Conexion_Mysql connect = new Conexion_Mysql();

                //** Modulos
                string sql = "select _m.codigo,_m.nombre "
                    + "from tb_usuariopagina u inner join tb_pagina p on u.pagina=p.codigo inner join tb_menu m on p.cod_submenu=m.codigo inner join tb_menu _m on m.id_padre=_m.codigo "
                    + "where u.login='{0}' and _m.id_padre is null group by _m.codigo";
                DataTable tb = connect.EjecutarSelectMysql(string.Format(sql, Session["Login"])).Tables[0];

                data_Modulo.GetStore().DataSource = tb;
                data_Modulo.GetStore().DataBind();

                Newtonsoft.Json.Linq.JObject ob = new Newtonsoft.Json.Linq.JObject();
                ob.Add("codigo", tb.Rows[0]["codigo"].ToString());
                ob.Add("nombre", tb.Rows[0]["nombre"].ToString());

                this.getFormModules(ob);
            }
        }

        [DirectMethod(ShowMask = true, Target = MaskTarget.Page, Msg = "Cargando Formularios ...")]
        public void getFormModules(Newtonsoft.Json.Linq.JObject recordModule)
        {
            Conexion_Mysql connect = new Conexion_Mysql();
            string sql = "select p.codigo,p.nombre,p.url,p.ancho,p.alto,p.icon "
                + "from tb_usuariopagina u inner join tb_pagina p on u.pagina=p.codigo inner join tb_menu m on p.cod_submenu=m.codigo "
                + "inner join tb_menu _m on m.id_padre=_m.codigo where u.login='{0}' and _m.codigo={1}  and p.icon is not null";

            DataView1.GetStore().DataSource = connect.EjecutarSelectMysql(string.Format(sql, Session["Login"], recordModule["codigo"]));
            DataView1.GetStore().DataBind();
        }
    }
}