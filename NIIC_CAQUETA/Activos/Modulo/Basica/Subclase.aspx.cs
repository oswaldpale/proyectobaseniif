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
    public partial class CrearSubclase : SigcWeb.API.Shared.Page
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!X.IsAjaxRequest)
            {
                cargargrilla();
            }
           
        }
        [DirectMethod(ShowMask = true, Msg = "Cargando ...", Target = MaskTarget.Page)]
        public void cargargrilla()
        {
            try
            {
                Conexion_Mysql conexion = new Conexion_Mysql();
                DataSet dt = conexion.EjecutarSelectMysql(string.Format("SELECT Subclase as subclase,CONVERT(IF(depreciable = 'SI',1,0),UNSIGNED INTEGER) as depreciable,id_subclase as idsubclase FROM tb_subclase "
                                                                       + " WHERE id_clase='{0}' and Estado='A'", idclase.Text));
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
            string estadodepreciable;
            if (CDEPRECIABLE.Checked)
            {
                estadodepreciable = "SI";
            }
            else
            {
                estadodepreciable = "NO";
            }
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
                        conexion.EjecutarOperacion(string.Format("INSERT INTO tb_subclase(id_clase,Estado,Subclase,login,depreciable,FyH_Real) "
                                                                + " VALUES('{0}','A','{1}','{2}','{3}',now())", idclase.Text, valor.ToUpper(), 
                                                                HttpContext.Current.User.Identity.Name.ToUpper(),estadodepreciable));
                        X.Msg.Notify("CREADO SUBCLASE", "SE CREO LA SUBCLASE: " + valor.ToUpper()).Show();
                        cargargrilla();
                        txt_subclase.Reset();
                       
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
        
      
        protected void TriggerField1_Click1(object sender, DirectEventArgs e)
        {
            CargarClase();
        }
        [DirectMethod(ShowMask = true, Msg = "Cargando...", Target = MaskTarget.Page)]
        public void CargarClase()
        {
            Conexion_Mysql conexion = new Conexion_Mysql();
            DataSet dt = conexion.EjecutarSelectMysql("SELECT id_clase as codigoclase,UPPER(Clase) as descripcionclase FROM tb_clase");
            Store_clase.DataSource = dt;
            Store_clase.DataBind();
        }
        protected void GridComodato_SelectRow(object sender, DirectEventArgs e)
        {
            TriggerField2.Text = e.ExtraParams["Nombre"].ToString();
            idclase.SetValue(e.ExtraParams["Codigo"].ToString());
            cargargrilla();
        }
        [DirectMethod(ShowMask = true, Msg = "Eliminando ...", Target = MaskTarget.Page)]
        public void EliminarSubclase(string id)
        {
            try
            {
                Conexion_Mysql conexion = new Conexion_Mysql();
                conexion.EjecutarOperacion(string.Format("DELETE FROM tb_subclase WHERE id_subclase = '{0}';", id));
                txt_subclase.Reset();
            }
            catch (Exception s)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "ActiFijo.",
                    Message = "Error: Esta Subclase no se puede eliminar porque se esta usando ",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR,
                    Closable = false
                });
            }
        }
        
        [DirectMethod(Namespace = "Detalle", Msg = "Modificando Subclase...", ShowMask = true, Target = MaskTarget.Page)]
        public void Editar(string id,string idsubclase,string estadodepreciable)
        {
            try
            {
               
                if (estadodepreciable=="true")
                {
                    estadodepreciable = "SI";
                }
                else
                {
                    estadodepreciable = "NO";
                }
                Conexion_Mysql conexion = new Conexion_Mysql();
                string sql = string.Format("UPDATE tb_subclase set Subclase='{0}',login='{1}',depreciable='{2}' "
                                                        + " WHERE id_subclase='{3}'", idsubclase.ToUpper(),
                                                        HttpContext.Current.User.Identity.Name.ToUpper(),
                                                        estadodepreciable,
                                                        id);
                
                conexion.EjecutarOperacion(sql);
                cargargrilla();
            }
            catch (Exception e)
            {

                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "ActiFijo.",
                    Message = "Error:" + e.Message ,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR,
                    Closable = false
                });
            }
            
        }
        
      
    }
}