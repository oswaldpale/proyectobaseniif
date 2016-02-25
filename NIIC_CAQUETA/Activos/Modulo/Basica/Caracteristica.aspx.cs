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
    public partial class Caracteristica : SigcWeb.API.Shared.Page
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
          //   base.Page_Load(sender, e);
            if (!X.IsAjaxRequest)
            {

                Conexion_Mysql conexion = new Conexion_Mysql();
                DataSet dt = conexion.EjecutarSelectMysql("SELECT id_tipo, TipoDato,Abreviatura FROM tb_tipodato");
                store_tipoDato.DataSource = dt;
                store_tipoDato.DataBind();

                this.cargargrilla();
            }
        }

        [DirectMethod(ShowMask = true, Msg = "Cargando ...", Target = MaskTarget.Page)]
        public void CargarDatos(string id)
        {
            try
            {
                idcaracteristica.Text = id;
                Conexion_Mysql conexion = new Conexion_Mysql();

                DataSet dt = conexion.EjecutarSelectMysql("SELECT id_tipo,Caracteristica,Descripcion FROM tb_caracteristicas WHERE id_caracteristica=" + id);
                txt_caracteristica.Text = dt.Tables[0].Rows[0]["Caracteristica"].ToString();
                txt_descripcion.Text = dt.Tables[0].Rows[0]["Descripcion"].ToString();
                cbx_tipo.SetValue(Convert.ToInt32(dt.Tables[0].Rows[0]["id_tipo"].ToString()));
                idcaracteristica.Text = id;
                btn_Guardar.Hidden = true;
                btn_Actualizar.Hidden = false;
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
        public void Guardar_tbCaracteristica(String[] values) {
            try
            {
                Conexion_Mysql conexion = new Conexion_Mysql();

                conexion.EjecutarOperacion(string.Format("INSERT INTO tb_caracteristicas(id_tipo,Caracteristica,Descripcion,FyH_Real,login) "
                                                         + "VALUES('{0}','{1}','{2}',now(),'{3}')",values[0],values[1],values[2],
                                                         HttpContext.Current.User.Identity.Name.ToUpper()));
                FormPanel1.Reset();
                btn_Actualizar.Hidden = true;
                btn_Guardar.Hidden = false;
                cargargrilla();
               
              
            }
            catch (Exception s)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "ActiFijo.",
                    Message = "Error" + s.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR,
                    Closable = false
                });
            }
        
        }

        [DirectMethod(ShowMask = true, Msg = "Eliminando ...", Target = MaskTarget.Page)]
        public void Eliminar_tbCaracteristica(string id)
        {
            try
            {
                Conexion_Mysql conexion = new Conexion_Mysql();
                conexion.EjecutarOperacion(string.Format("DELETE FROM tb_caracteristicas WHERE id_caracteristica = '{0}';", id));
               
                cargargrilla();
                FormPanel1.Reset();
                btn_Actualizar.Hidden = true;
                btn_Guardar.Hidden = false;
               

            }
            catch (Exception s)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "ActiFijo.",
                    Message = "Esta Caracteristica se encuentra Relacionada con una Subclase" + s.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR,
                    Closable = false
                });
            }
        }
        [DirectMethod(ShowMask = true, Msg = "Modificando ...", Target = MaskTarget.Page)]
        public void Modificar_tbCaracteristica(string[] values)
        {
            try
            {
                Conexion_Mysql conexion = new Conexion_Mysql();
                conexion.EjecutarOperacion(string.Format("UPDATE tb_caracteristicas SET Caracteristica ='{0}', "
                                                         + " Descripcion ='{1}', id_tipo='{2}' WHERE id_caracteristica = '{3}' ",
                                                         values[0], values[1], values[2],idcaracteristica.Text));
          
                cargargrilla();
                FormPanel1.Reset();
                btn_Actualizar.Hidden = true;
                btn_Guardar.Hidden = false;
            }
            catch (Exception s)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "ActiFijo.",
                    Message = "Error:" + s.Message,
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
            DataSet dt = conexion.EjecutarSelectMysql("SELECT c_a.id_caracteristica AS idcaracteristica,c_a.Caracteristica AS Caracteristica,c_a.Descripcion AS Descripcion, "
                                                      + " t_t.TipoDato AS Tipo FROM tb_caracteristicas AS c_a " 
                                                      + " INNER JOIN tb_tipodato AS t_t ON  c_a.id_tipo=t_t.id_tipo");
            store_caracteristica.DataSource = dt;
            store_caracteristica.DataBind();

        }

    }
}