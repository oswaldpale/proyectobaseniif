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
    public partial class MotivoAlta : SigcWeb.API.Shared.Page
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
        public void Guardar(Newtonsoft.Json.Linq.JArray record, string valor)
        {
            Boolean ing = true;
            for (int i = 0; i < record.Count; i++)
            {
                if (record[i]["motivo"].ToString().Equals(valor))
                {
                    ing = false;
                }
            }
            try
            {
                if (ing == false)
                {
                    throw new Exception("Este Motivo ya se ha creado.");
                }
                else
                {
                    if (valor != "null")
                    {
                        Conexion_Mysql conexion = new Conexion_Mysql();
                        conexion.EjecutarOperacion(string.Format("INSERT INTO tb_motivo_alta(Motivo,login,FyH_Real) "
                                                                + " VALUES('{0}','{1}',now())", valor.ToUpper(), 
                                                                HttpContext.Current.User.Identity.Name.ToUpper()));
                        X.Msg.Notify("CREADO MOTIVO", "SE CREO EL MOTIVO ALTA : " + valor.ToUpper()).Show();
                        cargargrilla();
                        txt_motivo.Reset();

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
        public void Eliminarmotivo(string id)
        {
            try
            {
                Conexion_Mysql conexion = new Conexion_Mysql();
                conexion.EjecutarOperacion(string.Format("DELETE FROM tb_motivo_alta WHERE MAltaId = '{0}'", id));
               
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
        [DirectMethod(Namespace = "Detalle", Msg = "Modificando...", ShowMask = true, Target = MaskTarget.Page)]
        public void Editar(string id, string values)
        {
            try
            {
                Conexion_Mysql conexion = new Conexion_Mysql();
                conexion.EjecutarOperacion(string.Format("UPDATE tb_motivo_alta SET Motivo='{0}',login='{1}' "
                                                        + " WHERE MAltaId='{2}'", values.ToUpper(), 
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

        [DirectMethod]
        public void cargargrilla()
        {
            Conexion_Mysql conexion = new Conexion_Mysql();
            DataSet dt = conexion.EjecutarSelectMysql(string.Format("SELECT Motivo as motivo,MAltaId as codigo FROM tb_motivo_alta"));
            store_motivo.DataSource = dt;
            store_motivo.DataBind();

        }
    }
}