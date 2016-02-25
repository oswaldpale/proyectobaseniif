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
    public partial class SubComponente : SigcWeb.API.Shared.Page
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            

            if (!X.IsAjaxRequest)
            {
                try
                {
                    Conexion_Mysql conexion = new Conexion_Mysql();

                    CargarClase();

                    DataSet dt = conexion.EjecutarSelectMysql("select id_tipo, UPPER(Depreciacion) as Depreciacion from tb_tipo_depreciacion");
                    cbx_tipo_depreciacion.GetStore().DataSource = dt;
                    cbx_tipo_depreciacion.GetStore().DataBind();

                    dt = conexion.EjecutarSelectMysql("select id_medida as codigo,UnidadMedida from tb_umedida");
                    cbx_medida.GetStore().DataSource = dt;
                    cbx_medida.GetStore().DataBind();

                    dt = conexion.EjecutarSelectMysql("select id_tipo as codigo, TipoDato as tipo from tb_tipodato");
                    store_tipo.DataSource = dt;
                    store_tipo.DataBind();

                    dt = conexion.EjecutarSelectMysql("select id_tipo_Norma as codigo, Norma as norma from tb_tipo_norma ");
                    store_norma.DataSource = dt;
                    store_norma.DataBind();
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
        protected void TriggerField1_Click(object sender, DirectEventArgs e)
        {
            cargarCaracteristica();
        }
        protected void TriggerField1_Click1(object sender, DirectEventArgs e) {
            CargarClase();
        }
        protected void TriggerField1_Click2(object sender, DirectEventArgs e)
        {
            select_subclase();
        }
      
        protected void GridComodato_SelectRow(object sender, DirectEventArgs e)
        {
            TriggerField2.Text = e.ExtraParams["Nombre"].ToString();
            idclase.SetValue(e.ExtraParams["Codigo"].ToString());
            clase.SetValue(e.ExtraParams["Nombre"].ToString());
            TriggerField3.Reset();
            
        }
        protected void GridComodato_SelectRow1(object sender, DirectEventArgs e)
        {
            TriggerField3.Text = e.ExtraParams["Nombre"].ToString();
            idsubclase.SetValue(e.ExtraParams["Codigo"].ToString());
            subclase.SetValue(e.ExtraParams["Nombre"].ToString());
            consultarSubclase();

        }
        protected void GridComodato_SelectRow2(object sender, DirectEventArgs e)
        {
            TriggerField1.Text = e.ExtraParams["Caracteristica"].ToString();
            idcaracteristica.SetValue(e.ExtraParams["Codigo"].ToString());
            caracteristica.SetValue(e.ExtraParams["Caracteristica"].ToString());
            tipo.SetValue(e.ExtraParams["Tipo"].ToString());
            descaracteristica.SetValue(e.ExtraParams["Descripcion"].ToString());
            consultarSubclase();
            HabilitarEditCaract();
        }
        [DirectMethod(ShowMask = true, Msg = "Cargando...", Target = MaskTarget.Page)]
        public void CargarClase()
        {
            Conexion_Mysql conexion = new Conexion_Mysql();
            DataSet dt = conexion.EjecutarSelectMysql("select id_clase as codigoclase,UPPER(Clase) as descripcionclase from tb_clase");
            Store_clase.DataSource = dt;
            Store_clase.DataBind();
        }
        [DirectMethod(ShowMask = true, Msg = "Cargando...", Target = MaskTarget.Page)]
        public void select_subclase()
        {
            Button5.Disabled = false;
            //Dropsubclase.Disabled = false;
          //  Dropsubclase.Reset();
            Conexion_Mysql conexion = new Conexion_Mysql();
            string val = "select id_subclase as codigosubclase,UPPER(Subclase) as descripcionsubclase from tb_subclase as sub inner join tb_clase as c on  sub.id_clase=c.id_clase  where c.id_clase=" + idclase.Text + "";
            DataSet dt = conexion.EjecutarSelectMysql(val);
            Store_subclase.DataSource = dt;
            Store_subclase.DataBind();

        }
        [DirectMethod]
        public void cargarCaracteristica()
        {
            Conexion_Mysql conexion = new Conexion_Mysql();
            DataSet dt = conexion.EjecutarSelectMysql(string.Format("SELECT t_c.id_tipo as tipo, t_c.Caracteristica as caracteristica,t_c.id_caracteristica as codigo,t_c.Descripcion as descripcion " +
                                                                  "FROM  tb_caracteristicas t_c left JOIN subclase_caract s_c ON t_c.id_caracteristica=s_c.id_caracteristica " +
                                                                  "and s_c.id_clase='{0}' and s_c.id_subclase='{1}' Where s_c.id_caracteristica is null", idclase.Text, idsubclase.Text));

            Store_tbcaracteristica.DataSource = dt;
            Store_tbcaracteristica.DataBind();
        }

        [DirectMethod(ShowMask = true, Msg = "Guardando ...", Target = MaskTarget.Page)]
        public void cargarGrillaCaracteristica(string clase, string subclase)
        {
            Conexion_Mysql conexion = new Conexion_Mysql();


            DataSet dt = conexion.EjecutarSelectMysql(string.Format("SELECT tc.id_consecutivo as codigo, c.Clase as clase, sc.Subclase as subclase, t_c.Caracteristica as caracteristica, tc.Obligatorio as obligatorio  " +
                                                    " FROM tb_clase c INNER JOIN tb_subclase sc ON c.id_clase = sc.id_clase " +
                                                    " left join subclase_caract tc on tc.id_clase = sc.id_clase and tc.id_subclase " +
                                                    " = sc.id_subclase left join tb_caracteristicas t_c on tc.id_caracteristica = " +
                                                    " t_c.id_caracteristica WHERE tc.id_clase='{0}' and tc.id_subclase='{1}'", clase, subclase));

            store_caracteristica.DataSource = dt;
            store_caracteristica.DataBind();

        }
        [DirectMethod(ShowMask = true, Msg = "Guardando ...", Target = MaskTarget.Page)]
        public void GuardarCaracteristica(string[] values)
        {
            try
            {
                Conexion_Mysql conexion = new Conexion_Mysql();
                conexion.EjecutarOperacion(string.Format("insert into subclase_caract(id_clase,id_subclase,id_caracteristica,Obligatorio,login,FyH_Real)" +
                                                        " values('{0}','{1}','{2}','{3}','{4}',now())", values[0], values[1], values[2], values[3], HttpContext.Current.User.Identity.Name));
                cargarCaracteristica();
                cargarGrillaCaracteristica(values[0], values[1]);
               // DroptbCaracteristica.Reset();
                //Button10.Disabled = true;
                //    X.Msg.Notify("ASIGNACION DE CARACTERISTICAS", "LA CARACTERISTICA: " + DroptbCaracteristica. + " SE ASIGNO HA LA SUBCLASE  " + values[1]).Show();
                cbx_obligatorio.Reset();
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
        public void EliminarCaracteristica(string id)
        {
            Conexion_Mysql conexion = new Conexion_Mysql();
            string sql = string.Format("DELETE FROM subclase_caract WHERE  id_consecutivo='{0}';", id);
            conexion.EjecutarOperacion(string.Format("DELETE FROM subclase_caract WHERE  id_consecutivo='{0}';", id));
            cargarCaracteristica();

        }
        [DirectMethod(ShowMask = true, Msg = "Guardando ...", Target = MaskTarget.Page)]
        public void cargarGrillaComponente(string clase, string subclase)
        {
            Conexion_Mysql conexion = new Conexion_Mysql();


            DataSet dt = conexion.EjecutarSelectMysql(string.Format("SELECT com.id_componente as codigo, c.Clase AS clase,UPPER(s.Subclase) AS subclase, UPPER(com.Componente) AS " +
                                                    " componente, com.porcentaje AS porc,com.VidaUtil AS vidautil,med.UnidadMedida " +
                                                    "AS unidad,UPPER(tipo.Depreciacion) AS  tipo from tb_clase as c INNER JOIN tb_subclase as " +
                                                    " s on c.id_clase=s.id_clase INNER JOIN subclase_componente AS com ON s.id_subclase=com.id_subclase " +
                                                    " INNER JOIN tb_umedida AS med ON com.id_medida=med.id_medida INNER JOIN tb_tipo_depreciacion " +
                                                    " AS tipo ON com.id_metodoDepreciacion=tipo.id_tipo WHERE com.id_clase='{0}' and com.id_subclase='{1}'", clase, subclase));

            store_componente.DataSource = dt;
            store_componente.DataBind();
        }
        [DirectMethod(ShowMask = true, Msg = "Guardando...", Target = MaskTarget.Page)]
        public void GuardarComponente(string[] values)
        {
            try
            {
                Conexion_Mysql conexion = new Conexion_Mysql();
                conexion.EjecutarOperacion(string.Format("insert into subclase_componente(id_clase,id_subclase,Componente,porcentaje,VidaUtil,id_metodoDepreciacion,id_medida,idnorma,FyH_Real) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',now())",
                                          values[0], values[1], values[2], values[3].ToString().Replace(',', '.'), values[4].ToString().Replace(',', '.'), values[5], values[6],values[7]));
                X.Msg.Notify("CREADO COMPONENTE", "SE CREO EL COMPONENTE: " + values[2]).Show();
                cargarGrillaComponente(values[0], values[1]);
                cbx_tipo_depreciacion.Reset();
                txt_nombre.Reset();
                txt_porcentajeComponente.Reset();
                txt_vidautil.Reset();
                cbx_medida.Reset();

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
        public void EliminarComponente(string id)
        {
            Conexion_Mysql conexion = new Conexion_Mysql();
            string sql = string.Format("DELETE FROM subclase_componente WHERE  id_componente='{0}';", id);
            conexion.EjecutarOperacion(string.Format("DELETE FROM subclase_componente WHERE  id_componente='{0}';", id));


        }
        [DirectMethod(ShowMask = true, Msg = "Guardando ...", Target = MaskTarget.Page)]
        public void GuardarClase(string valor)
        {
            try
            {
                Conexion_Mysql conexion = new Conexion_Mysql();
                string sql = "insert into tb_clase(Clase) values('" + txt_clase.Text + "')";
                conexion.EjecutarOperacion("insert into tb_clase(Clase,FyH_Real) values('" + valor + "',now())");
                X.Msg.Notify("CREACION ACTIVO", "SE HA CREADO LA CLASE: " + valor).Show();
                txt_clase.Reset();
                CargarClase();
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
        public void GuardarSubclase(string valor)
        {
            try
            {
                Conexion_Mysql conexion = new Conexion_Mysql();
                conexion.EjecutarOperacion(string.Format("insert into tb_subclase(id_clase,Subclase,FyH_Real) values('{0}','{1}',now())", idclase.Text, valor));
                X.Msg.Notify("CREADO SUBCLASE", "SE CREO LA SUBCLASE: " + valor).Show();
                txt_subclase.Reset();
                select_subclase();
                //Dropsubclase.Reset();
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
        public void EliminarSubclase(string id)
        {
            try
            {
                Conexion_Mysql conexion = new Conexion_Mysql();
                conexion.EjecutarOperacion(string.Format("DELETE FROM tb_subclase WHERE id_subclase = '{0}';", id));
                txt_subclase.Reset();
             //   Dropsubclase.Reset();
                select_subclase();
               // Dropsubclase.Reset();
                CargarClase();
            }
            catch (Exception s)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "ActiFijo.",
                    Message = "Error: Esta Subclase esta siendo usada en otra tabla " + s.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR,
                    Closable = false
                });
            }
        }
        [DirectMethod(ShowMask = true, Msg = "Modificando ...", Target = MaskTarget.Page)]
        public void ModificarSubclase(string[] values)
        {
            try
            {
                Conexion_Mysql conexion = new Conexion_Mysql();
                conexion.EjecutarOperacion(string.Format("UPDATE tb_subclase SET Subclase ='{0}' WHERE id_subclase = '{1}' ", values[0], values[1]));
                txt_subclase.Reset();
                select_subclase();
               // Dropsubclase.Reset();
                //Dropsubclase.SetValue(values[0]);
                CargarClase();
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
        }
        [DirectMethod(ShowMask = true, Msg = "Eliminando ...", Target = MaskTarget.Page)]
        public void Eliminar_tbCaracteristica(string id)
        {
            try
            {
                Conexion_Mysql conexion = new Conexion_Mysql();
                
                string dato = string.Format("select Count(t_c.id_caracteristica) as cantidad from tb_caracteristicas as t_c inner join subclase_caract as s_c on t_c.id_caracteristica=s_c.id_caracteristica  where t_c.id_caracteristica='{0}'", idcaracteristica.Text);
                DataSet dt = conexion.EjecutarSelectMysql(string.Format("select t_c.id_caracteristica as cantidad from tb_caracteristicas as t_c inner join subclase_caract as s_c on t_c.id_caracteristica=s_c.id_caracteristica  where t_c.id_caracteristica='{0}'", idcaracteristica.Text));
                int cantidad = dt.Tables[0].Rows.Count;
                if (cantidad == 0)
                {
                    conexion.EjecutarOperacion(string.Format("DELETE FROM tb_caracteristicas WHERE id_caracteristica = '{0}';", id));
                    //DroptbCaracteristica.Reset();
                    cargarCaracteristica();
                   
                }
                else
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "ActiFijo.",
                        Message = "Error:  " + "Esta Caracteristica Esta haciendo Usada por otra Subclase",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.ERROR,
                        Closable = false
                    });
                }
               

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
        [DirectMethod(ShowMask = true, Msg = "Modificando ...", Target = MaskTarget.Page)]
        public void Modificar_tbCaracteristica(string[] values)
        {
            try
            {
                Conexion_Mysql conexion = new Conexion_Mysql();
                conexion.EjecutarOperacion(string.Format("UPDATE tb_caracteristicas SET Caracteristica ='{0}', Descripcion ='{1}', id_tipo='{2}' WHERE id_caracteristica = '{3}' ", values[0], values[1], values[2], values[3]));
                //DroptbCaracteristica.SetValue(values[0]);
                cargarCaracteristica();
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
        public void HabilitarEditCaract()
        {
            Button12.Disabled = false;
        }
        [DirectMethod(ShowMask = true, Msg = "Eliminando ...", Target = MaskTarget.Page)]
        public void EliminarClase(string id)
        {
            try
            {
                Conexion_Mysql conexion = new Conexion_Mysql();
                string dato = string.Format("DELETE FROM tb_clase WHERE id_clase = '{0}';", id);
                conexion.EjecutarOperacion(string.Format("DELETE FROM tb_clase WHERE id_clase = '{0}';", id));
                txt_subclase.Reset();
                //Dropclase.Reset();
                select_subclase();
                //Dropsubclase.Reset();
                txt_clase.Reset();
                clase.Reset();
                idclase.Reset();
                CargarClase();
                //Dropsubclase.Disabled = true;
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
        }
        [DirectMethod(ShowMask = true, Msg = "Modificando ...", Target = MaskTarget.Page)]
        public void ModificarClase(string[] values)
        {
            try
            {
                Conexion_Mysql conexion = new Conexion_Mysql();
                conexion.EjecutarOperacion(string.Format("UPDATE tb_clase SET Clase ='{0}' WHERE id_clase = '{1}' ", values[0], values[1]));
                txt_subclase.Reset();
                //Dropclase.SetValue(values[0]);

                select_subclase();
                //Dropsubclase.Reset();
                CargarClase();
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
        }

        [DirectMethod(ShowMask = true, Msg = "Guardando ...", Target = MaskTarget.Page)]
        public void Guardar_tbCaracteristica(string[] values)
        {
            try
            {
                Conexion_Mysql conexion = new Conexion_Mysql();
                conexion.EjecutarOperacion(string.Format("insert into tb_caracteristicas(Caracteristica,Descripcion,id_tipo,FyH_Real) values('{0}','{1}','{2}',now())", values[0], values[1], values[2]));
                X.Msg.Notify("CREACION DE CARACTERISTICA", "SE HA CREADO LA CARACTERISTICA: " + values[0]).Show();
                cbx_tipo.Reset();
                txt_caracteristica.Reset();
                txt_descripcion.Reset();
                cargarCaracteristica();

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
        public void consultarSubclase()
        {
            Button9.Disabled = false;
            CommandColumn10.Enabled = false;
            FormPanel2.Disabled = false;
            FormPanel3.Disabled = false;
            cargarCaracteristica();
            cargarGrillaCaracteristica(idclase.Text, idsubclase.Text);
            cargarGrillaComponente(idclase.Text, idsubclase.Text);


        }
        [DirectMethod(ShowMask = true, Msg = "Cargando ...", Target = MaskTarget.Page)]
        public void EditClase()
        {

            txt_clase.Text = clase.Text;
            btn_modificarclase.Hidden = false;
            btn_eliminarclase.Hidden = false;
            btn_crearClase.Hidden = true;
            window1.Show();

        }
        [DirectMethod]
        public void AddClase()
        {
           // txt_filtrocodigoclase.Reset();
          //  txt_filtrodescripcionclase.Reset();
            txt_clase.Reset();
            btn_crearClase.Hidden = false;
            btn_modificarclase.Hidden = true;
            btn_eliminarclase.Hidden = true;
            window1.Show();

        }
        [DirectMethod(ShowMask = true, Msg = "Cargando ...", Target = MaskTarget.Page)]
        public void EditSubClase()
        {

            txt_subclase.Text = subclase.Text;
            btn_modificarsubclase.Hidden = false;
            btn_eliminarsubclase.Hidden = false;
            btn_crearSubclase.Hidden = true;
            window2.Show();

        }
        [DirectMethod]
        public void AddSubclase()
        {
          
            btn_crearSubclase.Hidden = false;
            btn_modificarsubclase.Hidden = true;
            btn_eliminarsubclase.Hidden = true;
            window2.Show();

        }
        [DirectMethod(ShowMask = true, Msg = "Cargando ...", Target = MaskTarget.Page)]
        public void EditCaracteristica()
        {
            try
            {
                
                
                cargarCaracteristica();
                txt_caracteristica.Text = caracteristica.Text;
                txt_descripcion.Text = descaracteristica.Text;
                cbx_tipo.SetValue(Convert.ToInt32(tipo.Text));
                btn_ModCaracteristica.Hidden = false;
                btn_ElimCaracteristica.Hidden = false;
                btn_crearCaracteristica.Hidden = true;
                window3.Show();
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
        [DirectMethod(ShowMask = true, Msg = "Cargando ...", Target = MaskTarget.Page)]
        public void AddCaracteristica()
        {
           
            txt_caracteristica.Reset();
            txt_descripcion.Reset();
            cbx_tipo.Reset();
            btn_ModCaracteristica.Hidden = true;
            btn_ElimCaracteristica.Hidden = true;
            btn_crearCaracteristica.Hidden = false;
            window3.Show();

        }
    }
}