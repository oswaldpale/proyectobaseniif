using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Data;
using Activos.Clases;

namespace Activos.Modulo
{
    public partial class CrearSubclase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!X.IsAjaxRequest)
            {
                try
                {
                    /***    Clase  */
                    Conexion_Mysql conexion = new Conexion_Mysql();
                    DataSet dt = conexion.EjecutarSelectMysql("select id_clase as codigoclase,UPPER(Clase) as descripcionclase from tb_clase");
                    Store_clase.DataSource = dt;
                    Store_clase.DataBind();
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
        }
        [DirectMethod(ShowMask = true, Msg = "Cargando ...", Target = MaskTarget.Page)]
        public void cargargrilla()
        {
            try
            {
                Conexion_Mysql conexion = new Conexion_Mysql();
                DataSet dt = conexion.EjecutarSelectMysql("select UPPER(c.Clase) as clase,UPPER(s.Subclase) as subclase FROM  tb_clase AS c INNER JOIN tb_subclase AS s ON c.id_clase=s.id_clase");
                store_hclase.DataSource = dt;
                store_hclase.DataBind();
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
        [DirectMethod(ShowMask = true, Msg = "Guardando ...", Target = MaskTarget.Page)]
        public void Guardar(Newtonsoft.Json.Linq.JArray record,string valor )
        {
            Boolean ing = true;
            for (int i = 0; i < record.Count; i++)
            {
                if (record[i]["subclase"].ToString().Equals(valor))
                {
                    ing = false;
                }
            }
            try
            {
                if (ing == false)
                {
                    throw new Exception("Esta Subclase  ya se ha creado.");
                }
                else
                {
                    if (valor != "null")
                    {
                        Conexion_Mysql conexion = new Conexion_Mysql();
                        conexion.EjecutarOperacion(string.Format("insert into tb_subclase(id_clase,Subclase,FyH_Real) values('{0}','{1}',now())", idclase.Text, valor));
                        X.Msg.Notify("CREADO SUBCLASE", "SE CREO LA SUBCLASE: " + valor).Show();                    
                        cargargrilla();
                        txt_subclase.Reset();
                        Dropclase.Reset();
                    }
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
        public void cargarCaracteristicas() {
            Conexion_Mysql conexion = new Conexion_Mysql();
            conexion.EjecutarSelectMysql("select  ");
          Ext.Net.Label lbl_text = new Ext.Net.Label();
        }
    }
}