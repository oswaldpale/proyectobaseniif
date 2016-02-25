using Ext.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Activos.Clases;

namespace Activos.Modulo
{
    public partial class CargarValorResidualPlano : System.Web.UI.Page
    {
        public List<AtributosDepreciacion> depreciacion = new List<AtributosDepreciacion>();
        public List<string> sqlActivo = new List<string>(); 
        protected void Page_Load(object sender, EventArgs e)
        {
            this.StoreAtributos.DataSource = this.DataDepreciacion;
            this.StoreAtributos.DataBind();
        }
       
        
        private object[] DataDepreciacion
        {
            get
            {
                return new object[]
            {
                new object[] { "Placa Activo","SI" },
                new object[] { "Codigo componente", "SI" },
                new object[] { "Tipo Depreciación", "SI" },
                new object[] { "Cantidad Depreciada", "SI" },
              
            };
            }
        }
        [DirectMethod(Msg = "Cargando...", ShowMask = true, Target = MaskTarget.CustomTarget)]
        public void CargarCaracteristicas(string id) {
            GrillaAtributos.GetStore().DataSource = this.DataDepreciacion;
            GrillaAtributos.DataBind();
        }
        [DirectMethod(Msg = "Cargando...", ShowMask = true, Target = MaskTarget.CustomTarget)]
        public void CargarArchivo(Newtonsoft.Json.Linq.JArray datos)
        {
            
            try
            {
                string tpl = "Archivo cargado: {0}";

                if (this.FileUploadField1.HasFile)
                {


                    List<string[]> parsedData = new List<string[]>();
                   

                    using (StreamReader readFile = new StreamReader(FileUploadField1.FileContent))
                    {
                        string archivo = readFile.ReadToEnd();
                        string s = txt_separador_f.Value.ToString();
                        if (s.Equals("\\n"))
                        {
                            s = "\n";
                        }
                        string[] filas = archivo.Split(new string[] { s }, StringSplitOptions.RemoveEmptyEntries);
                        if (filas.Length > 0)
                        {

                             AtributosDepreciacion dep = new AtributosDepreciacion(); 
                                for (int i = 0; i < filas.Length; i++)
                                {
                                    string[] columnas = filas[i].Split(new string[] { txt_separador_c.Value.ToString() }, StringSplitOptions.RemoveEmptyEntries);

                                    if (columnas.Length < datos.Count)
                                    {
                                        throw new Exception("Las columnas del archivo no corresponden a las seleccionadas, por favor revise el archivo e intente de nuevo.");
                                    }
                                        //dep.IdActivo = columnas[0].ToString();
                                        //dep.CodComponente = Convert.ToInt32(columnas[1].ToString());
                                        //dep.TipoDepreciacion =Convert.ToInt32(columnas[2].ToString());
                                        //dep.Cantidad =Convert.ToDouble(columnas[3].ToString());

                                        depreciacion.Add(dep);
                                }
                                
                               
                        }
                        else
                        {
                            throw new Exception("No ha sido posible leer el archivo, revise los separadores.");
                        }

                    }

                    X.Msg.Show(new MessageBoxConfig
                    {
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.INFO,
                        Title = "Archivo cargado con éxito",
                        Message = string.Format(tpl, this.FileUploadField1.PostedFile.FileName)
                    });
                }
                else
                {
                    X.Msg.Show(new MessageBoxConfig
                    {
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.ERROR,
                        Title = "Error",
                        Message = "No ha sido posible leer el archivo, revise los separadores."
                    });
                }
            }
            catch (Exception exc)
            {
                X.Msg.Show(new MessageBoxConfig
                {
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR,
                    Title = "Error",
                    Message = exc.Message
                });
            }
        }
        public void CalcularDepreciacion(List<AtributosDepreciacion> dep)
        {

            Conexion_Mysql conexion = new Conexion_Mysql();
            string detActivo = "SELECT CostoInicial,vr_Depreciacion,vr_deterioro,vr_residual,"
                                + " vr_razonable,ajus_dep_acum,Fecha_Udepreciacion "
                                + " FROM activo WHERE placa={0}";

            string detComponente = "SELECT id_Componente,id_activo,Porcentaje_ci,ajust_dep_acum,vida_util"
                                    + " ajus_vr_residual,ajus_vr_razonable,ajus_vr_deterioro,vida_util,"
                                    + " IF(vida_util_temp IS NULL,vida_util,vida_util_temp) AS vida_util_temp"
                                    + " FROM activo_componente"
                                    + "  WHERE id_activo={0}";
            foreach (AtributosDepreciacion activo in dep)
            {
                
                DataSet dtActivo = conexion.EjecutarSelectMysql(string.Format(detActivo, activo.IdActivo));
                
                DataSet dtComponente = conexion.EjecutarSelectMysql(string.Format(detComponente,activo.CodComponente));
                foreach (DataRow Comp in dtComponente.Tables[0].Rows)
                {
                    Comp[""].ToString();       
                }            
            
           }
        }
        public double valorasignado(double valor, double porcentaje)
        {
            return ((valor * porcentaje) / 100);
        }

    }
}