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
    public partial class Clase : SigcWeb.API.Shared.Page
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!X.IsAjaxRequest)
            {
                this.cargargrilla();
            }
           

        }
        [DirectMethod]
        public void cargargrilla()
        {
                Conexion_Mysql conexion = new Conexion_Mysql();
                DataSet dt = conexion.EjecutarSelectMysql(string.Format("SELECT Clase as clase,id_clase as codigo FROM tb_clase"
                                                         + " WHERE Estado='A'"));
                store_hclase.DataSource = dt;
                store_hclase.DataBind();
          
        }
        [DirectMethod(ShowMask = true, Msg = "Guardando ...", Target = MaskTarget.Page)]
        public void Guardar(Newtonsoft.Json.Linq.JArray record, string valor)
        {
            Boolean ing = true;
            for (int i = 0; i < record.Count; i++)
            {
                if (record[i]["clase"].ToString().Equals(valor))
                {
                    ing = false;
                }
            }
            try
            {
                if (ing == false)
                {
                    throw new Exception("Esta clase  ya se ha creado.");
                }
                else
                {
                    if (valor != "null")
                    {
                        Conexion_Mysql conexion = new Conexion_Mysql();
                        conexion.EjecutarOperacion(string.Format("INSERT INTO tb_clase(Estado,Clase,login,FyH_Real) "
                                                                +" VALUES('A','{0}','{1}',now())", valor.ToUpper(), 
                                                                HttpContext.Current.User.Identity.Name.ToUpper()));
                        X.Msg.Notify("CREADO CLASE", "SE CREO LA CLASE: " + valor.ToUpper()).Show();
                        cargargrilla();
                        txt_clase.Reset();

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
      
        [DirectMethod(ShowMask = true, Msg = "Eliminando ...", Target = MaskTarget.Page)]
        public void EliminarClase(string id)
        {
            try
            {
                Conexion_Mysql conexion = new Conexion_Mysql();
                conexion.EjecutarOperacion(string.Format("DELETE FROM tb_clase WHERE id_clase = '{0}'", id));
            }
            catch (Exception s)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "ActiFijo.",
                    Message = "Error: Esta Clase esta siendo usada en otra tabla " + s.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR,
                    Closable = false
                });
            }
            cargargrilla();
        }
        [DirectMethod(Namespace = "Detalle", Msg = "Modificando clase...", ShowMask = true, Target = MaskTarget.Page)]
        public void Editar(string id, string values)
        {
            try
            {
                Conexion_Mysql conexion = new Conexion_Mysql();
                conexion.EjecutarOperacion(string.Format("UPDATE tb_clase set clase='{0}',login='{1}' "
                                                        + " WHERE id_clase='{2}'", values.ToUpper(),
                                                        HttpContext.Current.User.Identity.Name.ToUpper(), id));
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
    }
}