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
    public partial class ResponsableActivo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                try
                {
                 
                    Conexion_Mysql conexion = new Conexion_Mysql();
                    DataSet dt = conexion.EjecutarSelectMysql("select id_empleado,UPPER(Nombres) as NombreEmpleado from empleado");
                    Store_empleado.DataSource = dt;
                    Store_empleado.DataBind();
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
            Conexion_Mysql conexion = new Conexion_Mysql();
            DataSet dt = conexion.EjecutarSelectMysql("select Nombre as nombre, idActivo as codigo, placa from activo");
            GridActivo.GetStore().DataSource = dt;
            GridActivo.GetStore().DataBind();
        }
        protected void TriggerField2_Click(object sender, DirectEventArgs e)
        {
            Conexion_Mysql conexion = new Conexion_Mysql();
            DataSet dt = conexion.EjecutarSelectMysql("select id_empleado,UPPER(Nombres) as NombreEmpleado from empleado");
            Store_empleado.DataSource = dt;
            Store_empleado.DataBind();
        }

        [DirectMethod]
        public void Guardar(Newtonsoft.Json.Linq.JArray record, string[] values)
        {
            try
            {
                /* el    empleado que se le asigne el activo tiene un estado activo   */
                //   int estado = 1;
                //     int codigo= Convert.ToInt16(Session["codigo"]);
                string fecha = Convert.ToDateTime(values[2]).ToString("yyyy-MM-dd");
                int estado = 1;
                Conexion_Mysql conexion = new Conexion_Mysql();
                //   conexion.EjecutarOperacion();
                List<string> sql = new List<string>();

                string cadena = string.Format("update asignacionactivo set estado=0 where idactivo= '{0}' and estado='{1}';", codigo.Text, estado);
                sql.Add(cadena);
                if (values[1].ToString() == "")
                {
                    sql.Add(string.Format("insert into asignacionactivo (idactivo,idempleado,fecha,estado,motivo_alta,FyH_Real) values('{0}','{1}','{2}','{3}','{4}',now())",
                           codigo.Text, values[0], fecha, estado,values[3]));
                }
                else
                {
                    sql.Add(string.Format("insert into asignacionactivo (idactivo,idempleado,observacion,fecha,estado,motivo_alta,FyH_Real) values('{0}','{1}','{2}','{3}','{4}','{5}',now())",
                        codigo.Text, values[0], values[1], fecha, estado,values[3]));
                }
                if (FechaDepreciacion.Text != "" && FechaAlta.Text!="") {

                    sql.Add(string.Format("UPDATE activo SET id_empleado = '{0}'  WHERE idActivo = '{1}'", values[0], codigo.Text));
                }
                else
                {
                    if (record.Count() == 0)
                    {
                        sql.Add(string.Format("UPDATE activo SET Fecha_Alta = '{0}',id_empleado = '{1}', Fecha_Udepreciacion = '{2}'  WHERE idActivo = '{3}'", fecha, values[0], fecha, codigo.Text));
                    }
                    else
                    {
                        sql.Add(string.Format("UPDATE activo SET id_empleado = '{0}'  WHERE idActivo = '{1}'", values[0], codigo.Text));
                    }
                }
                    
                
                conexion.EjecutarTransaccion(sql);
               // Drop2.Reset();
                dfd_fecha.Reset();
                txt_codigo.Reset();
                txt_ce.Reset();
                txt_cc.Reset();
                txt_conceptos.Reset();
                txt_cc.Reset();
                txt_ce.Reset();
                cbx_motivo.Reset();
                TriggerField1.Reset();
                TriggerField2.Reset();
                // limpiarFormulario();
              //  cargarGrillaAsignaciones();
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Sigc.",
                    Message = "La asignacion se ha realizado con éxito",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.INFO,
                    Closable = true
                });

            }
            catch (Exception s)
            {

                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "ActiFijo.",
                    Message = "Error... " + s.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR,
                    Closable = false
                });
            }


        }
        [DirectMethod]
        public void cargarCentros(string id)
        {
            try
            {
                Conexion_Mysql conexion = new Conexion_Mysql();
                DataSet dt = conexion.EjecutarSelectMysql("select idcentroeconomico as ce,idcentrocosto as cc from empleado where id_empleado=" + id);
                string cc = dt.Tables[0].Rows[0]["cc"].ToString();
                string ce = dt.Tables[0].Rows[0]["ce"].ToString();
                dt = conexion.EjecutarSelectMysql("select Centro_costo as cc from tb_centro_costo where id_centro =" + cc);
                txt_cc.Text = dt.Tables[0].Rows[0]["cc"].ToString();
                dt = conexion.EjecutarSelectMysql("select Centro_Economico as ce from tb_centro_economico where id_centro =" + ce);
                txt_ce.Text = dt.Tables[0].Rows[0]["ce"].ToString();
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
        protected void GridComodato_SelectRow(object sender, DirectEventArgs e)
        {
            TriggerField1.Text = e.ExtraParams["Nombre"].ToString();
            codigo.Text = e.ExtraParams["NComodato"].ToString();
            cargarGrillaAsignaciones();
            Conexion_Mysql conexion = new Conexion_Mysql();
            DataSet dt = conexion.EjecutarSelectMysql("select MAltaId as codigo,Motivo as motivo FROM tb_motivo_alta");
            cbx_motivo.GetStore().DataSource = dt;
            cbx_motivo.DataBind();
            dt = conexion.EjecutarSelectMysql(string.Format("select c.Fecha,a.Fecha_Alta,Fecha_Udepreciacion from activo as a INNER JOIN detallecompra as d ON a.NoCompra = d.NCompra INNER JOIN compra as c ON d.NCompra=c.NCompra where a.idActivo='{0}'", codigo.Text));
            if (dt.Tables[0].Rows[0]["Fecha_Alta"].ToString() != "") {
                FechaAlta.SetValue(DateTime.Parse(dt.Tables[0].Rows[0]["Fecha_Alta"].ToString()).ToString("yyyy-MM-dd"));
                dfd_fecha.MinDate =Convert.ToDateTime(FechaAlta.Text);
            }
            else
            {
                FechaCompra.SetValue(DateTime.Parse(dt.Tables[0].Rows[0]["Fecha"].ToString()).ToString("yyyy-MM-dd"));
                dfd_fecha.MinDate = Convert.ToDateTime(FechaCompra.Text);
            }
            if (dt.Tables[0].Rows[0]["Fecha_Udepreciacion"].ToString() != "")
            {
                FechaDepreciacion.SetValue(DateTime.Parse(dt.Tables[0].Rows[0]["Fecha_Udepreciacion"].ToString()).ToString("yyyy-MM-dd"));
            }
            
            
        }

        [DirectMethod]
        public void cargarGrillaAsignaciones()
        {

            //   int codigo = Convert.ToInt16(Session["codigo"]);
            Conexion_Mysql conexion = new Conexion_Mysql();
            DataSet dt = conexion.EjecutarSelectMysql("select  e.Nombres as nombre, a.fecha as fecha, c_costo.Centro_costo  as centro from asignacionactivo as a " +
                                                    "inner join empleado e on a.idempleado=e.id_empleado inner join tb_centro_costo as c_costo on e.idcentrocosto=c_costo.id_centro where a.idactivo=" + codigo.Text);

            GridPanel1.GetStore().DataSource = dt;
            GridPanel1.DataBind();

        }

       
        protected void GridDatoEmp_SelectRow(object sender, DirectEventArgs e)
        {
            TriggerField2.Text = e.ExtraParams["Nombre"].ToString();
            this.SelectDatos(e.ExtraParams["Codigo"].ToString());
            txt_codigo.Text = e.ExtraParams["Codigo"].ToString();
           
        }
       
        [DirectMethod]
        public void SelectDatos(string id)
        {
            
            Conexion_Mysql conexion = new Conexion_Mysql();
            DataSet dt = conexion.EjecutarSelectMysql("select idcentroeconomico as ce,idcentrocosto as cc from empleado where id_empleado=" + id);
            string cc = dt.Tables[0].Rows[0]["cc"].ToString();
            string ce = dt.Tables[0].Rows[0]["ce"].ToString();
            dt = conexion.EjecutarSelectMysql("select Centro_costo as cc from tb_centro_costo where id_centro =" + cc);
            txt_cc.Text = dt.Tables[0].Rows[0]["cc"].ToString();
            dt = conexion.EjecutarSelectMysql("select Centro_Economico as ce from tb_centro_economico where id_centro =" + ce);
            txt_ce.Text = dt.Tables[0].Rows[0]["ce"].ToString();
        }
    }
}