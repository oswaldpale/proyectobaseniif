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
    public partial class Empleado : SigcWeb.API.Shared.Page
    {
        public Boolean EstadoControl = false;
        protected override void Page_Load(object sender, EventArgs e)
        {
            // base.Page_Load(sender, e);

            if (!X.IsAjaxRequest)
            {
                idempleado.Text = "";
                Conexion_Mysql conexion = new Conexion_Mysql();
                DataSet dt = conexion.EjecutarSelectMysql("SELECT codigo,id_centro as idcosto,Upper(Centro_costo) as costo FROM  tb_centro_costo WHERE estado='A' ");
                cbx_costo.GetStore().DataSource = dt;
                cbx_costo.DataBind();

                dt = conexion.EjecutarSelectMysql("SELECT codigo,id_centro as ideconomico,Upper(Centro_Economico) as economico FROM tb_centro_economico");
                cbx_economico.GetStore().DataSource = dt;
                cbx_economico.DataBind();
                cargargrilla();

                dt = conexion.EjecutarSelectMysql("SELECT cod_tipo as idcargo,codigo,Upper(Tipo) as tipo FROM tipoempleado");
                cbx_tipo.GetStore().DataSource = dt;
                cbx_tipo.DataBind();
            }
        }

        [DirectMethod(ShowMask = true, Msg = "Guardando ...", Target = MaskTarget.Page)]
        public void Guardar(String[] values)
        {
            try{
                    Conexion_Mysql conexion = new Conexion_Mysql();
                    Boolean f = CheckMenuItem_Activo.Checked;
                    if (f==true)
                    {
                        conexion.EjecutarOperacion(string.Format("INSERT INTO empleado(id_empleado,No_documento,Nombres,Apellido1,Apellido2,id_tipo,Telefono,Celular," +
                                                            " Direccion,idcentrocosto,idcentroeconomico,eliminado,FyH_Real,login) " +
                                                            " VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','A',now(),'{11}')"
                                                            , values[0], values[0], values[1].ToUpper(), values[2].ToUpper(), values[3].ToUpper(), values[4], values[5], values[6]
                                                            , values[7].ToUpper(), values[8], values[9], HttpContext.Current.User.Identity.Name.ToUpper()));
                    }
                    else
                    {
                        conexion.EjecutarOperacion(string.Format("INSERT INTO empleado(id_empleado,No_documento,Nombres,Apellido1,Apellido2,id_tipo,Telefono,Celular," +
                                                            " Direccion,idcentrocosto,idcentroeconomico,eliminado,FyH_Real) " +
                                                            " VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','I',now(),'{11}')"
                                                            , values[0], values[0], values[1].ToUpper(), values[2].ToUpper(), values[3].ToUpper(), values[4], values[5], values[6]
                                                            , values[7].ToUpper(), values[8], values[9], HttpContext.Current.User.Identity.Name.ToUpper()));
                    }
                   
                    X.Msg.Notify("CREADO  EMPLEADO ", "SE REGISTRO EL EMPLEADO  : " + values[1]+" "+values[2]+" " + values[3]).Show();
                    FormPanel1.Reset();
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
        [DirectMethod(ShowMask = true, Msg = "Validando ...", Target = MaskTarget.Page)]
        public Boolean VerificarActivoEmpleado() {
            try
            {
                 Conexion_Mysql conexion = new Conexion_Mysql();
                 Boolean f = CheckMenuItem_Inactivo.Checked;
                 DataSet dt = conexion.EjecutarSelectMysql(string.Format("SELECT count(*) AS cantidad FROM activo " 
                                                           + " WHERE id_empleado= '{0}' AND Estado='A'", idempleado.Text));
                 int cantidad =Convert.ToInt32(dt.Tables[0].Rows[0]["cantidad"].ToString());
                        if(cantidad > 0 && f==true){
                            
                            return true;
                        }
                           return false;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        

        [DirectMethod(ShowMask = true, Msg = "Actualizando ...", Target = MaskTarget.Page)]

        public void Actualizar(string[] values)
        {
            try
            {
                Conexion_Mysql conexion = new Conexion_Mysql();
                Boolean f = CheckMenuItem_Activo.Checked;
                if (f == true)
                {
                    conexion.EjecutarOperacion(string.Format("UPDATE empleado SET Nombres='{0}', Apellido1='{1}', Apellido2='{2}', "
                                                            + " Direccion='{3}',Telefono='{4}',Celular='{5}',id_tipo='{6}', "
                                                            + " idcentrocosto='{7}',idcentroeconomico='{8}',eliminado='A' " 
                                                            + " WHERE id_empleado='{9}'", values[0], values[1], values[2], 
                                                            values[3], values[4], values[5], values[6], values[7], values[8], 
                                                            values[9]));
                    FormPanel1.Reset();
                    cargargrilla();
                    Button1.Disabled = true;
                    Button2.Disabled = false;
                }
                else
                {
                    if (VerificarActivoEmpleado()) {
                        X.Msg.Alert("ACTIVOS ASIGNADOS", "EL FUNCIONARIO TIENE ACTUALMENTE ASIGNADO ACTIVOS").Show();
                        CheckMenuItem_Activo.SetChecked(true);
                        
                       
                    }
                    else
                    {
                        conexion.EjecutarOperacion(string.Format("UPDATE empleado SET Nombres='{0}', Apellido1='{1}', Apellido2='{2}', "
                                                               + " Direccion='{3}',Telefono='{4}',Celular='{5}',id_tipo='{6}',idcentrocosto='{7}', "
                                                               + " idcentroeconomico='{8}',eliminado='I'  where id_empleado='{9}'", values[0],
                                                               values[1], values[2], values[3], values[4], values[5], values[6], values[7], 
                                                               values[8], values[9]));
                        FormPanel1.Reset();
                        cargargrilla();
                        Button1.Disabled = true;
                        Button2.Disabled = false;
                    }
                } 
                

               
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
        public void QuitarSeleccion(Boolean f) {
            if (f== true)
            {
                CheckMenuItem_Activo.SetChecked(true);
                
            }
            else
            {
                CheckMenuItem_Inactivo.SetChecked(true);
            }
            EstadoControl = true;
            RowSelectionModel sm = this.GridPanel1.GetSelectionModel() as RowSelectionModel;
            sm.ClearSelection();
        }
        [DirectMethod]
        public void LimpiarComponentes()
        {
            txt_identificacion.Reset();
            txt_nombre.Reset();
            txt_apellido1.Reset();
            txt_apellido2.Reset();
            cbx_tipo.Reset();
            txt_telefono.Reset();
            txt_celular.Reset();
            txt_direccion.Reset();
            cbx_costo.Reset();
            cbx_economico.Reset();
        }

        [DirectMethod(ShowMask = true, Msg = "...", Target = MaskTarget.Page)]
        public void cargargrilla()
        {
            Conexion_Mysql conexion = new Conexion_Mysql();
            DataSet dt = conexion.EjecutarSelectMysql(string.Format("SELECT id_empleado as idempleado,e.No_documento as cedula, "
                                                                 + " CONCAT_WS(' ',e.Nombres, e.Apellido1,e.Apellido2) as nombre, "
                                                                 + " e.Nombres as nombre1, e.Apellido1 as apellido1 ,e.Apellido2 as apellido2, "
                                                                 + " e.id_tipo as idtipo,e.Direccion AS direccion,e.Telefono as telefono, "
                                                                 + " e.Celular as celular,c.Centro_costo AS ccosto,ec.Centro_Economico as ceconomico, "
                                                                 + " e.idcentrocosto as idcosto,e.idcentroeconomico as ideconomico, e.eliminado FROM empleado AS e "	
                                                                 + " INNER JOIN tb_centro_costo as c ON  e.idcentrocosto=c.id_centro "
                                                                 + " INNER JOIN tb_centro_economico as ec ON e.idcentroeconomico=ec.id_centro"));
            store_empleados.DataSource = dt;
            store_empleados.DataBind();

        }
    }
}