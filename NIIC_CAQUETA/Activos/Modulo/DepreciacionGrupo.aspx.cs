using Activos.Clases;
using Ext.Net;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Xml;
using System.Xml.Xsl;
using Newtonsoft.Json;


namespace Activos.Modulo
{
    public partial class DepreciacionGrupo : SigcWeb.API.Shared.Page
    {
        public DataSet DtComponentes;
        private Parametrizacion doc = new Parametrizacion();
        private GeneradorReportes _reporte = new GeneradorReportes();
        private Mensajes mens = new Mensajes();
        public List<AtributosDepreciacion> datosDepreciacion = new List<AtributosDepreciacion>();
        public List<ActivoComponente> datoActivo = new List<ActivoComponente>();
        public List<Activo> ListaActivos = new List<Activo>();
        public List<string> sqlActivo = new List<string>();
        public GeneradorReportes Informe = new GeneradorReportes();
        public List<Activo> ActivosDepreciados = new List<Activo>();

        protected override void Page_Load(object sender, EventArgs e)
        {
            Global.path2 = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.LastIndexOf("/"));
            FechaUDepreciacionMinima();
            ListaComponenteIngresoUnidadesDepreciadas();
            // base.Page_Load(sender, e);
            if (X.IsAjaxRequest)
            {

            }

        }

        #region Metodos por Ingreso de Unidades Depreciable
        /// <summary>
        /// consulta todos  los componente que son diferentes  al Tipo de Depreciacion por Linea Recta en una subclase
        /// </summary>
        /// <param name="IdSubclase"></param>
        private void ListaComponenteIngresoUnidadesDepreciadas()
        {
            Conexion_Mysql conexion = new Conexion_Mysql();
            String sql = "SELECT "
                         + "    a.idActivo                                        AS IdActivo, "
                         + "    CONCAT('COD: ',CAST(a.idActivo as char),' - (' ,a.Nombre, ')' ) AS Activo, "
                         + "    a_c.id_Componente                                 AS IdComponente, "
                         + "    s_c.Componente                                    AS Componente, "
                         + "    t_d.Depreciacion                                  AS TipoDepreciacion, "
                         + "    '0'                                               AS UnidadesDepreciadas "
                         + "FROM "
                         + "    activo_componente          AS a_c "
                         + "INNER JOIN subclase_componente AS s_c "
                         + "ON "
                         + "    a_c.sub_compID=s_c.id_componente "
                         + "INNER JOIN tb_tipo_depreciacion t_d "
                         + "ON "
                         + "    a_c.id_tipo_depreciacion = t_d.id_tipo "
                         + "AND a_c.id_tipo_depreciacion <> 1 "
                         + "INNER JOIN activo a "
                         + "ON "
                         + "    a_c.id_activo = a.idActivo "
                         + "AND a.Estado = 'A' "
                         + "AND a.id_empleado IS NOT NULL "
                         + "AND a.Fecha_Alta IS NOT NULL "
                         + "ORDER BY "
                         + "    a.idActivo";
            DataSet dt = conexion.EjecutarSelectMysql(sql);
            storeIngresoUnidades.DataSource = dt;
            storeIngresoUnidades.DataBind();

            EstadoUnidadUtilizada.Text = Convert.ToString(dt.Tables[0].Rows.Count);
        }
        /// <summary>
        ///  Carga el Archivo plano ----<El archivo plano debe tener el orden siguiente:   IdActivo;IdCompnente;UnidadesUtilizadas >
        /// </summary>
        /// <param name="ListaComponente"> Obtiene Todos los componentes que requieren ingresar las Unidades Utilizadas </param>
        [DirectMethod(ShowMask = true, Msg = "Actualizando", Target = MaskTarget.CustomTarget, CustomTarget = "App.GridPanelIngresoUnidades")]
        #region CARGAR PLANO VIDA EJECUTDA MES
        public void CargarArchivoIngresoUnidades(Newtonsoft.Json.Linq.JArray ListaComponente)
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
                            if (ListaComponente.Count < filas.Length)
                            {
                                throw new Exception("Error, la cantidad de registro de componente  del archivo supera la indicada en el detalle.\nCantidad de componentes:" + filas.Length + ", Cantidad del detalle=" + ListaComponente.Count + "");
                            }
                            datosDepreciacion.Clear();
                            for (int i = 0; i < filas.Length; i++)
                            {
                                AtributosDepreciacion dep = new AtributosDepreciacion();
                                string[] columnas = filas[i].Split(new string[] { txt_separador_c.Value.ToString() }, StringSplitOptions.RemoveEmptyEntries);

                                if (columnas.Length < this.datosDepreciacion.Count)
                                {
                                    throw new Exception("Las columnas del archivo no corresponden a las especificadas, por favor revise el archivo e intente de nuevo.");
                                }
                                dep.IdActivo = Convert.ToInt32(columnas[0].ToString());
                                dep.CodComponente = columnas[1].ToString();
                                dep.Cantidad = Convert.ToDouble(columnas[2].ToString().Replace('\r', ' '));
                                datosDepreciacion.Add(dep);

                                for (int j = 0; j < ListaComponente.Count; j++)
                                {
                                    if (ListaComponente[j]["IdActivo"].ToString() == columnas[0].ToString().Trim() && ListaComponente[j]["IdComponente"].ToString() == columnas[1].ToString().Trim())
                                    {
                                        ListaComponente[j]["UnidadesDepreciadas"] = columnas[2].ToString().Replace('\r', ' ');
                                    }
                                }

                            }
                            GridPanelIngresoUnidades.GetStore().DataSource = ListaComponente.ToList();
                            GridPanelIngresoUnidades.GetStore().DataBind();
                            GridPanelIngresoUnidades.GetStore().CommitChanges();
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

        #endregion
        /// <summary>
        /// Valida las unidades utilizadas ingresado por plano.
        /// </summary>
        /// <param name="ListaComponente"> Lista de Componentes ya cargados con los valores de las Unidades Utilizadas</param>
        [DirectMethod]
        public void ValidarCamposUnidadesDepreciadas(Newtonsoft.Json.Linq.JArray record)
        {
            try
            {

                for (int j = 0; j < record.Count; j++)
                {
                    if (Convert.ToDouble(record[j]["UnidadesDepreciadas"].ToString()) == 0)
                    {
                        throw new Exception("Faltan Campos por Llenar");

                    }
                }
                Conexion_Mysql conexion = new Conexion_Mysql();
                String sql = "SELECT "
                + "    a.idActivo                                        AS IdActivo, "
                + "    CONCAT('COD: ',a.idActivo,' - (' ,a.Nombre, ')' ) AS Activo, "
                + "    a_c.id_Componente                                 AS IdComponente, "
                + "    s_c.Componente                                    AS Componente, "
                + "    t_d.Depreciacion                                  AS TipoDepreciacion, "
                + "    a.NoCompra                                        AS IdNorma, "
                + "    n.Norma, "
                + "    '0' AS UnidadesDepreciadas "
                + "FROM "
                + "    activo_componente          AS a_c "
                + "INNER JOIN subclase_componente AS s_c "
                + "ON "
                + "    a_c.sub_compID=s_c.id_componente "
                + "INNER JOIN tb_tipo_depreciacion t_d "
                + "ON "
                + "    a_c.id_tipo_depreciacion = t_d.id_tipo "
                + "AND a_c.id_tipo_depreciacion = '1' "
                + "INNER JOIN tb_tipo_norma n "
                + "ON "
                + "    a_c.idtiponorma = n.id_tipo_Norma "
                + "INNER JOIN activo a "
                + "ON "
                + "    a_c.id_activo = a.idActivo "
                + "AND a.Estado = 'A' "
                + "AND a.id_empleado IS NOT NULL "
                + "AND a.Fecha_Alta IS NOT NULL "
                + "ORDER BY "
                + "    a.idActivo";

                DataSet dt = conexion.EjecutarSelectMysql(sql);

                datosDepreciacion.Clear();

                for (int j = 0; j < record.Count; j++)
                {
                    AtributosDepreciacion dep = new AtributosDepreciacion();
                    dep.IdActivo = Convert.ToInt32(record[j]["IdActivo"].ToString());
                    dep.CodComponente = record[j]["IdComponente"].ToString();
                    dep.Cantidad = Convert.ToDouble(record[j]["UnidadesDepreciadas"].ToString());
                    datosDepreciacion.Add(dep);
                }

                foreach (DataRow item in dt.Tables[0].Rows)
                {
                    AtributosDepreciacion dep = new AtributosDepreciacion();
                    dep.IdActivo = Convert.ToInt32(item["IdActivo"].ToString());
                    dep.CodComponente = item["IdComponente"].ToString();
                    dep.Cantidad = Convert.ToDouble(item["UnidadesDepreciadas"].ToString());
                    datosDepreciacion.Add(dep);
                }
                Session["datosDepreciacion"] = datosDepreciacion;
                estadoPlano.Text = "true";
                BtnGuardarUnidadesUtilizadas.Disabled = true;
                btn_procesar.Disabled = false;
                win_cargarplano.Hide();

                X.Msg.Show(new MessageBoxConfig
                {
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.INFO,
                    Title = "Componentes Listos!..",
                    Message = "Activos listos para Iniciar el proceso de Depreciación. "
                });
            }
            catch (Exception e)
            {
                X.Msg.Show(new MessageBoxConfig
                {
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR,
                    Title = "Error!..",
                    Message = e.Message
                });

            }

        }

        #endregion

        [DirectMethod]
        public void FechaUDepreciacionMinima()
        {
            Conexion_Mysql conexion = new Conexion_Mysql();

            string FechaRegistroUDocumento = doc.ConsultarFechaDepreciacionMin();

            if (FechaRegistroUDocumento != "")
            {
                DateTime FechaTemp = Convert.ToDateTime(DateTime.Parse(FechaRegistroUDocumento).ToString("yyyy-MM-dd")).AddDays(1);
                dfd_fecha.MinDate = FechaTemp;
                // A partir de la Segunda depreciación se restringue depreciacion para que  se realize el 1 de cada mes.
                if (ExisteDocumentos() >= 0)
                {
                    DateTime fecha1 = new DateTime(FechaTemp.Year, FechaTemp.Month, 1).AddMonths(1).AddDays(-1);

                    dfd_fecha.MaxDate = fecha1;
                }

            }
            else
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Información!.",
                    Message = "No Existe Activos Dado De Alta.",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.INFO,
                    Closable = true
                });
                dfd_fecha.Reset();
            }

        }

        protected void GridComodato_SelectRow1(object sender, DirectEventArgs e)
        {
            cargarGrillaActivos();

        }

        public DataSet ConsultarLimiteActivos(string limitarFilas)
        {
            Conexion_Mysql conexion = new Conexion_Mysql();
            string sql = string.Format(" SELECT idActivo,  placa,Nombre, s.Subclase,NoCompra, "
                                      + " Date_format(Fecha_Udepreciacion, '%d-%m-%Y') as FechaUdep,"
                                      + " CostoInicial,vr_razonable as revaluacion,id_empleado,vr_dep_mes, "
                                      + " vr_deterioro as deterioro,vr_residual as residual,BaseDepreciable as baseDepreciacion, "
                                      + " (SELECT COUNT(ac.id_tipo_depreciacion) FROM activo_componente ac "
                                      + " WHERE "
                                      + " ac.id_activo=idActivo AND ac.id_tipo_depreciacion<>1) as Cant_Dif_LRecta, "
                                      + " vr_Depreciacion as DepAcum,Importe_libros as importe FROM activo as a "
                                      + " INNER JOIN tb_clase as c ON a.id_clase=c.id_clase "
                                      + " INNER JOIN tb_subclase as s ON a.id_subclase=s.id_subclase and s.depreciable='SI' "
                                      + " WHERE a.Estado='A' AND a.id_empleado is not null "
                                      + " AND a.Fecha_Udepreciacion is not null AND a.Fecha_Baja is null "
                                      + " AND a.Fecha_Udepreciacion  <'{0}' " + limitarFilas,
                                      Convert.ToDateTime(dfd_fecha.Text).ToString("yyyy-MM-dd"));

            return conexion.EjecutarSelectMysql(sql);
        }

        [DirectMethod]
        public void cargarGrillaActivos()
        {

            DataSet dt = ConsultarLimiteActivos("");

            if (dfd_fecha.Text != "")
            {
                foreach (DataRow fila in dt.Tables[0].Rows)
                {
                    Activo tempActivo = new Activo();
                    tempActivo.CostoInicial = Convert.ToDouble(fila["CostoInicial"].ToString());
                    tempActivo.idactivo = Convert.ToInt32(fila["idActivo"].ToString());
                    tempActivo.idresponsable = fila["id_empleado"].ToString();
                    tempActivo.Nombre = fila["Nombre"].ToString();
                    tempActivo.nombresubclase = fila["Nombre"].ToString();
                    tempActivo.Placa = fila["placa"].ToString();
                    tempActivo.NoCompra = fila["NoCompra"].ToString();
                    tempActivo.dep_mes = Convert.ToDouble(fila["vr_dep_mes"].ToString());
                    tempActivo.Fecha_Udepreciacion = Convert.ToDateTime(fila["FechaUdep"]).ToString("yyyy-MM-dd");
                    tempActivo.VrRazonable = Convert.ToDouble(fila["revaluacion"].ToString());
                    tempActivo.VrDeterioro = Convert.ToDouble(fila["deterioro"].ToString());
                    tempActivo.VrResidual = Convert.ToDouble(fila["residual"].ToString());
                    tempActivo.baseDepreciable = Convert.ToDouble(fila["baseDepreciacion"].ToString());
                    tempActivo.VrDepreciacion = Convert.ToDouble(fila["DepAcum"].ToString());
                    tempActivo.Importe_libros = Convert.ToDouble(fila["importe"].ToString());
                    tempActivo.Fecha_Revision = Convert.ToDateTime(dfd_fecha.Text).ToString("yyyy-MM-dd");
                    tempActivo.Cant_Dif_LRecta = Convert.ToInt32(fila["Cant_Dif_LRecta"].ToString());
                    ListaActivos.Add(tempActivo);
                }
                cantPlano.Text = ListaActivos.Count.ToString();
                int cantDifLineaRecta = ListaActivos.Sum(y => y.Cant_Dif_LRecta);
                if (cantDifLineaRecta >= 1)
                {
                    btnCargarPlano.Disabled = false;
                    btn_procesar.Disabled = true;

                }
                else
                {
                    btnCargarPlano.Disabled = true;
                    btn_procesar.Disabled = false;
                }

            }

            Session["ListaActivos"] = ListaActivos;
            if (dt.Tables[0].Rows.Count < 5000)
            {
                store_listaActivos.DataSource = dt;
                store_listaActivos.DataBind();
            }
            else
            {
                store_listaActivos.DataSource = ConsultarLimiteActivos("LIMIT 100"); ;
                store_listaActivos.DataBind();
            }

        }

        [DirectMethod(ShowMask = true, Msg = "Cargando....", Target = MaskTarget.Page)]
        public void cargarhistorialDepreciacion(String codigo, String Nombre, String Placa)
        {
            codActivo.Text = codigo;
            nombreActivo.Text = Nombre;
            idplaca.Text = Placa;
            win_trazabilidad.SetTitle("Trazabilidad Activo: -" + Placa + " " + Nombre);
            Conexion_Mysql conexion = new Conexion_Mysql();




            DataSet dt = conexion.EjecutarSelectMysql(_reporte.ConsultarTrazabilidadReporte(codigo, "LOCAL"));
            GridPanel2.GetStore().DataSource = dt;
            GridPanel2.DataBind();
        }

        #region INFORMES

        [DirectMethod(ShowMask = true, Msg = "Creando Informe Depreciación en PDF ...", Target = MaskTarget.Page)]
        public void Informe_TrazabilidaNiic_RptDetalle()
        {
            Informe.informeNormaInternacionalDepreciacion(codActivo.Text, idplaca.Text, nombreActivo.Text);
        }

        [DirectMethod(ShowMask = true, Msg = "Creando Informe Trazabilidad en PDF ...", Target = MaskTarget.Page)]
        public void Informe_Trazabilidad_RptDetalle()
        {
            try
            {

                Conexion_Mysql connect = new Conexion_Mysql();
                ReportePDF pdf = new ReportePDF("rptDetalleTrazabilidad");
                List<string> htm = pdf.Template;

                string cad = "SELECT d.idactivo as idactivo,ca.costo_inicial as costoinicial,dd.tipo_doc ,s_c.Componente as descripcion, d.fecha,  dd.base, dd.depreciacion_mes as dep_mes,dd.unidad_dep as cantdep,dd.ImporteLibros as saldo_dep,dd.dep_acum as depacumulada, dd.id_componente,  "
                                                                        + "dd.vr_residual as vresidual,dd.vr_dep_acumulada as ajdepacumulada,dd.ajust_vr_razonable as ajrazonable,dd.vr_deterioro as vrdeterioro, dd.tipo_doc FROM depreciacion d "
                                                                        + "INNER JOIN detalle_depreciacion dd ON d.id_depreciacion = dd.id_depreciacion "
                                                                        + "INNER JOIN activo_componente ca ON dd.id_componente=ca.id_Componente "
                                                                        + "INNER JOIN subclase_componente as s_c ON ca.sub_compID=s_c.id_componente "
                                                                        + "WHERE d.idactivo = '{0}'  ORDER BY descripcion,d.fecha,d.FyH_Real";

                DataSet dt = connect.EjecutarSelectMysql(string.Format(cad, codActivo.Text));

                //** Encabezado
                htm[1] = htm[1].Replace("DETALLE", "DETALLE TRAZABILIDAD DEL ACTIVO: " + idplaca.Text + "-" + nombreActivo.Text);
                htm[1] = string.Format(htm[1], Global.Empresa, Global.Nit, DateTime.Now.ToString("yyyy-MM-dd"));
                cad = pdf.Inicio;
                //**
                int i = 0;
                while (i < dt.Tables[0].Rows.Count)
                {
                    double vr = 0;
                    DataRow[] rows = dt.Tables[0].Select("idactivo='" + dt.Tables[0].Rows[i]["idactivo"] + "'");
                    cad += htm[2];

                    foreach (DataRow item in rows)
                    {
                        cad += string.Format(htm[3], Convert.ToDateTime(item["fecha"]).ToString("yyyy-MM-dd"), item["tipo_doc"], item["descripcion"], string.Format("{0:n}",
                                            item["costoinicial"]), string.Format("{0:n}", item["cantdep"]), string.Format("{0:n}", item["ajrazonable"]),
                                            string.Format("{0:n}", item["ajdepacumulada"]), string.Format("{0:n}", item["vresidual"]),
                                            string.Format("{0:n}", item["vrdeterioro"]), string.Format("{0:n}", item["base"]), string.Format("{0:n}", item["dep_mes"]),
                                            string.Format("{0:n}", item["depacumulada"]), string.Format("{0:n}", item["ImporteLibros"])
                                            );
                    }

                    cad += string.Format(htm[4], "");
                    i += rows.Length;
                }
                //  cad += string.Format(htm[5]);

                cad += pdf.Fin;

                pdf.createPDF(cad, htm[1], "Detalles_Trazabilidad_", new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 25f, 25f, 20f, 20f));
            }
            catch (Exception exc)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Sigc. #_Error.",
                    Message = "Error: " + exc.Message + exc.StackTrace,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR,
                    Closable = false
                });
            }
        }


        public void Excelparte(string json, string parte)
        {
            try
            {
                string id_file = "prueba.txt";
                string FilePath = HttpRuntime.AppDomainAppPath + @"Planos\" + id_file;
                StreamWriter plano = new StreamWriter(FilePath);
                plano.WriteLine(json);
                plano.Close();

                X.AddScript("descargaCSV('Planos/prueba.txt');");

            }
            catch (Exception e)
            {
                this.Response.End();
            }
        }

        #endregion
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
                            //if (temp.Cantidad < filas.Length)
                            //{
                            //    throw new Exception("Error, la cantidad de los activos del archivo supera la indicada en el detalle.\nCantidad de activos:" + filas.Length + ", Cantidad del detalle=" + temp.Cantidad + "");
                            //}
                            datosDepreciacion.Clear();
                            for (int i = 0; i < filas.Length; i++)
                            {
                                AtributosDepreciacion dep = new AtributosDepreciacion();
                                string[] columnas = filas[i].Split(new string[] { txt_separador_c.Value.ToString() }, StringSplitOptions.RemoveEmptyEntries);

                                if (columnas.Length < datos.Count)
                                {
                                    throw new Exception("Las columnas del archivo no corresponden a las seleccionadas, por favor revise el archivo e intente de nuevo.");
                                }
                                dep.Placa = columnas[0].ToString();
                                dep.CodComponente = columnas[1].ToString();
                                dep.TipoDepreciacion = columnas[2].ToString();
                                dep.Cantidad = Convert.ToDouble(columnas[3].ToString());

                                datosDepreciacion.Add(dep);
                            }
                            Session["datosDepreciacion"] = datosDepreciacion;
                            estadoPlano.Text = "true";

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

        protected void IniciarProcesoEjecucion(object sender, DirectEventArgs e)
        {
            bool d = dfd_fecha.IsEmpty;

            if (dfd_fecha.IsEmpty == false)
            {

                this.Session["LongActionProgress"] = 0;
                ThreadPool.QueueUserWorkItem(ProcesarEjecucion);
                this.ResourceManager1.AddScript("{0}.startTask('longactionprogress');", this.TaskManager1.ClientID);
            }
            else
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Campos Vacios",
                    Message = "Faltan Campos por llenar",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR,
                    Closable = false
                });
            }

        }



        /// <summary>
        /// Calcula la depreciacion todos los activos por linea recta Tomando Fecha Referencia La fechaRevision(Formulario)
        /// </summary>
        /// <param name="idactivo"></param>
        /// <param name="fanteriorD"></param>
        /// <param name="fechaRevision"></param>
        /// <returns></returns>

        public List<ActivoComponente> ProDepFecha(int idactivo, string fanteriorD, string fechaRevision)
        {


            List<ActivoComponente> ActComp = new List<ActivoComponente>();
            Conexion_Mysql conexion = new Conexion_Mysql();

            string detComponente = "SELECT"
                                    + "    a.id_Componente, "
                                    + "    a.id_activo, "
                                    + "    a.Porcentaje_ci, "
                                    + "    a.costo_inicial, "
                                    + "    a.vr_dep_acumulada, "
                                    + "    n.Norma, "
                                    + "    a.vida_util, "
                                    + "    a.vida_util_utilizado, "
                                    + "    a.ajus_vr_razonable, "
                                    + "    a.ajus_vr_deterioro, "
                                    + "    a.ajus_vr_residual, "
                                    + "    a.reexpresionVidaUtil, "
                                    + "    sc.Componente, "
                                    + "    a.idtiponorma, "
                                    + "    a.costo_inicial, "
                                    + "    a.base_deprec, "
                                    + "    a.ImporteDepreciable, "
                                    + "    a.vr_importe_libros, "
                                    + "    a.vr_dep_acumulada, "
                                    + "    td.Depreciacion, "
                                    + "    a.id_tipo_depreciacion, "
                                    + "IF(a.vida_util_remanente IS NULL,a.vida_util,a.vida_util_remanente) AS vida_util_temp "
                                    + "FROM activo_componente a "
                                    + "INNER JOIN subclase_componente sc ON a.sub_compID = sc.id_componente "
                                    + "INNER JOIN tb_tipo_depreciacion td ON a.id_tipo_depreciacion = td.id_tipo "
                                    + "INNER JOIN tb_tipo_norma n ON a.idtiponorma = n.id_tipo_Norma "
                                    + "WHERE a.id_activo='{0}'";




            DataSet dt = conexion.EjecutarSelectMysql(string.Format(detComponente, idactivo));
            foreach (DataRow row in dt.Tables[0].Rows)
            {
                ActivoComponente Comp = new ActivoComponente();
                Comp.id_activo = Convert.ToInt32(row["id_activo"].ToString());
                Comp.id_componente = row["id_Componente"].ToString();
                Comp.id_tipodepreciacion = row["id_tipo_depreciacion"].ToString();
                Comp.Porcentaje_ci = Convert.ToDouble(row["Porcentaje_ci"].ToString());
                Comp.costo_inicial = Convert.ToDouble(row["costo_inicial"].ToString());
                Comp.vida_util = Convert.ToDouble(row["vida_util"].ToString());
                Comp.ajusteVidaUtil = Convert.ToDouble(row["reexpresionVidaUtil"].ToString());
                Comp.vida_util_temp = Convert.ToDouble(row["vida_util_temp"].ToString());
                Comp.vida_util_utilizado = Convert.ToDouble(row["vida_util_utilizado"].ToString());
                Comp.ajust_vr_residual = Convert.ToDouble(row["ajus_vr_residual"].ToString());
                Comp.ajust_vr_deterioro = Convert.ToDouble(row["ajus_vr_deterioro"].ToString());
                Comp.ajust_vr_razonable = Convert.ToDouble(row["ajus_vr_razonable"].ToString());
                Comp.vr_importe_libros = Convert.ToDouble(row["vr_importe_libros"].ToString());
                Comp.vr_dep_acumulada = Convert.ToDouble(row["vr_dep_acumulada"].ToString());
                Comp.vr_dep_acumulada_old = Convert.ToDouble(row["vr_dep_acumulada"].ToString());
                Comp.nombre_depreciacion = row["Depreciacion"].ToString();
                Comp.nombre_componente = row["Componente"].ToString();
                Comp.idtiponorma = row["idtiponorma"].ToString();
                Comp.NombreNorma = row["Norma"].ToString();
                Comp.base_deprec = Convert.ToInt32(row["base_deprec"].ToString());
                Comp.base_deprec_old = Convert.ToInt32(row["base_deprec"].ToString());
                Comp.cantidadDias = CalcularDias(fanteriorD, fechaRevision);
                Comp.unidad_dep = Math.Truncate((Convert.ToDouble(CalcularDias(fanteriorD, fechaRevision)) / 30) * 100) / 100;

                if (Comp.vida_util_utilizado < Comp.vida_util)
                {
                    if (Comp.vr_dep_mes <= Comp.base_deprec)
                    {
                        Comp.unidad_dep = Math.Truncate((Convert.ToDouble(CalcularDias(fanteriorD, fechaRevision)) / 30) * 100) / 100;
                        Comp.vr_dep_mes = Math.Round((Comp.base_deprec / Comp.vida_util_temp) * Comp.unidad_dep, 0);
                        Comp.vida_util_utilizado = Math.Round(Comp.vida_util_utilizado + Comp.unidad_dep, 2);
                        Comp.vida_util_remanente = Math.Round(Comp.vida_util - Comp.vida_util_utilizado + Comp.ajusteVidaUtil, 2);
                        Comp.vr_dep_acumulada = Comp.vr_dep_acumulada + Comp.vr_dep_mes;
                        Comp.base_deprec = Math.Round(Comp.base_deprec - Comp.vr_dep_mes, 0);
                        Comp.vr_importe_libros = Math.Round(Comp.vr_importe_libros - Comp.vr_dep_mes, 0);
                    }
                    else
                    {
                        Comp.unidad_dep = 0;
                        Comp.vr_dep_mes = 0;
                    }
                }
                //}
                ActComp.Add(Comp);
            }
            return ActComp;
        }


        /// <summary>
        /// Toma los datos cargados x plano de los activos y los calcula(metodos de depreciacion diferente a linea recta.)
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        public ActivoComponente ProDepPlano(AtributosDepreciacion dep, string FechaUdep, string FechaRevision)
        {
            ActivoComponente Comp = new ActivoComponente();
            Conexion_Mysql conexion = new Conexion_Mysql();

            string detComponente = "SELECT "
                                        + "    sc.Componente, "
                                        + "    td.Depreciacion, "
                                        + "    a.id_Componente, "
                                        + "    a.id_activo, "
                                        + "    a.Porcentaje_ci, "
                                        + "    a.costo_inicial, "
                                        + "    a.base_deprec, "
                                        + "    a.vr_dep_acumulada, "
                                        + "    a.vida_util, "
                                        + "    a.vida_util_utilizado, "
                                        + "    a.id_tipo_depreciacion, "
                                        + "    a.idtiponorma, "
                                        + "    a.ajus_vr_residual, "
                                        + "    a.ajus_vr_razonable, "
                                        + "    a.ajus_vr_deterioro, "
                                        + "    a.vr_importe_libros, "
                                        + "    a.vr_dep_acumulada, "
                                        + "    n.Norma, "
                                        + "IF(a.vida_util_remanente IS NULL,a.vida_util,a.vida_util_remanente) AS vida_util_temp "
                                        + "FROM "
                                        + "    activo_componente a "
                                        + "INNER JOIN subclase_componente sc "
                                        + "ON "
                                        + "    a.sub_compID = sc.id_componente "
                                        + "INNER JOIN tb_tipo_depreciacion td "
                                        + "ON "
                                        + "    a.id_tipo_depreciacion = td.id_tipo "
                                        + "INNER JOIN tb_tipo_norma n "
                                        + "ON "
                                        + "    a.idtiponorma = n.id_tipo_Norma "
                                        + "WHERE "
                                        + "    a.id_Componente = '{0}' ";


            DataSet dt = conexion.EjecutarSelectMysql(string.Format(detComponente, dep.CodComponente));
            Comp.id_activo = Convert.ToInt32(dt.Tables[0].Rows[0]["id_activo"].ToString());
            Comp.id_componente = dt.Tables[0].Rows[0]["id_Componente"].ToString();
            Comp.nombre_componente = dt.Tables[0].Rows[0]["Componente"].ToString();
            Comp.nombre_depreciacion = dt.Tables[0].Rows[0]["Depreciacion"].ToString();
            Comp.NombreNorma = dt.Tables[0].Rows[0]["Norma"].ToString();
            Comp.Porcentaje_ci = Convert.ToDouble(dt.Tables[0].Rows[0]["Porcentaje_ci"].ToString());
            Comp.costo_inicial = Convert.ToDouble(dt.Tables[0].Rows[0]["costo_inicial"].ToString());
            Comp.vida_util = Convert.ToDouble(dt.Tables[0].Rows[0]["vida_util"].ToString());
            Comp.ajust_vr_residual = Convert.ToDouble(dt.Tables[0].Rows[0]["ajus_vr_residual"].ToString());
            Comp.ajust_vr_razonable = Convert.ToDouble(dt.Tables[0].Rows[0]["ajus_vr_razonable"].ToString());
            Comp.ajust_vr_deterioro = Convert.ToDouble(dt.Tables[0].Rows[0]["ajus_vr_deterioro"].ToString());
            Comp.vida_util_utilizado = Convert.ToDouble(dt.Tables[0].Rows[0]["vida_util_utilizado"].ToString());
            Comp.vida_util_temp = Convert.ToDouble(dt.Tables[0].Rows[0]["vida_util_temp"].ToString());
            Comp.vr_importe_libros = Convert.ToDouble(dt.Tables[0].Rows[0]["vr_importe_libros"].ToString());
            Comp.vr_dep_acumulada = Convert.ToDouble(dt.Tables[0].Rows[0]["vr_dep_acumulada"].ToString());
            Comp.vr_dep_acumulada_old = Convert.ToDouble(dt.Tables[0].Rows[0]["vr_dep_acumulada"].ToString());
            Comp.tipodepreciacion = Convert.ToInt16(dt.Tables[0].Rows[0]["id_tipo_depreciacion"].ToString());
            Comp.id_tipodepreciacion = dt.Tables[0].Rows[0]["id_tipo_depreciacion"].ToString();
            Comp.base_deprec = Convert.ToInt32(dt.Tables[0].Rows[0]["base_deprec"].ToString());
            Comp.base_deprec_old = Convert.ToInt32(dt.Tables[0].Rows[0]["base_deprec"].ToString());

            Comp.idtiponorma = dt.Tables[0].Rows[0]["idtiponorma"].ToString();
            Comp.NombreNorma = dt.Tables[0].Rows[0]["Norma"].ToString();

            if (Comp.tipodepreciacion == 1)
            {
                Comp.unidad_dep = Math.Truncate((Convert.ToDouble(CalcularDias(FechaUdep, FechaRevision)) / 30) * 100) / 100;
            }
            else
            {
                Comp.unidad_dep = dep.Cantidad;
            }

            Comp.vr_dep_mes = Math.Round((Comp.base_deprec / Comp.vida_util_temp) * Comp.unidad_dep, 0);

            if (Comp.vr_dep_mes <= Comp.vr_importe_libros)
            {
                Comp.vida_util_utilizado = Math.Round(Comp.vida_util_utilizado + Comp.unidad_dep, 2);
                Comp.vida_util_remanente = Comp.vida_util - Comp.vida_util_utilizado;
                Comp.vr_dep_acumulada = Comp.vr_dep_acumulada + Comp.vr_dep_mes;
                Comp.base_deprec = Math.Round(Comp.base_deprec - Comp.vr_dep_mes, 0);
                Comp.vr_importe_libros = Math.Round(Comp.vr_importe_libros - Comp.vr_dep_mes, 0);
            }
            //}

            return Comp;
        }

        /// <summary>
        /// Calculo los dias segun el mes laboral(30 dias) 
        /// </summary>
        /// <param name="oldDate"></param>
        /// <param name="newDate"></param>
        /// <returns></returns>
        public double CalcularDias(string oldDate, string newDate)
        {

            DateTime fecha1 = Convert.ToDateTime(oldDate);
            DateTime fecha2 = Convert.ToDateTime(newDate);

            int diastrancurrido = 0;
            int diasmes = DateTime.DaysInMonth(Convert.ToDateTime(fecha1).Year, Convert.ToDateTime(fecha1).Month); //ultimo dia del mes
            if (fecha1.Year == fecha2.Year && fecha1.Month == fecha2.Month)
            {// dos fechas pertenencen al mismo mes
                if (fecha1.Day != 30 && diasmes == fecha2.Day)
                { //fecha dos es el ultimo dia del mes entonces  tome el mes de 30
                    diastrancurrido = 30 - fecha1.Day;
                }
                else
                {    // sino solo reste
                    diastrancurrido = fecha2.Day - fecha1.Day;
                }
            }
            else
            {
                // fecha dos es superior al mes de la fecha 1
                int diasmesinicio = 0;
                int mesestrancurridos = Convert.ToInt16(Math.Abs((fecha2.Month - fecha1.Month) + 12 * (fecha2.Year - fecha1.Year))) - 1;
                if (fecha2.Day >= 30 || fecha2.Day == DateTime.DaysInMonth(Convert.ToDateTime(fecha2).Year, Convert.ToDateTime(fecha2).Month))
                {  // fecha 2 es mayor al dia 30 o terminal en el ultimo dia del mes
                    if (fecha1.Day >= 30 || fecha1.Day == DateTime.DaysInMonth(Convert.ToDateTime(fecha1).Year, Convert.ToDateTime(fecha1).Month))
                    { // si fecha uno es igual al ultimo dia del mes o superior al 30
                        diasmesinicio = 0;
                    }
                    else
                    {
                        diasmesinicio = 30 - fecha1.Day;

                    }
                    diastrancurrido = 30 * mesestrancurridos + diasmesinicio + 30;
                }
                else
                {
                    diasmesinicio = 30 - fecha1.Day;
                    diastrancurrido = 30 * mesestrancurridos + diasmesinicio + fecha2.Day;
                }
            }

            return diastrancurrido;
        }

        /// <summary>
        // Consulto si existe ya una depreciacion de ese activo  antes de realizar calculos;
        /// </summary>
        /// <returns></returns>
        private int ExisteDocumentos()
        {

            Conexion_Mysql conexion = new Conexion_Mysql();


            String sql = "SELECT COUNT(*) AS ExisteDep "
                            + "FROM activos_fijos.depreciacion d ";
            int ExisteDepAntes = Convert.ToInt32(conexion.EjecutarSelectMysql(sql).Tables[0].Rows[0]["ExisteDep"].ToString());
            return ExisteDepAntes;
        }

        /// <summary>
        /// Actualiza la barra de progreso cada vez que se deprecia un activo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RefreshProgress(object sender, DirectEventArgs e)
        {
            object progress = this.Session["LongActionProgress"];
            if (cantPlano.Text == "")
            {
                return;
            }
            else
            {
                float cant = Convert.ToSingle(cantPlano.Text);
                if (progress != null)
                {
                    this.Progress1.UpdateProgress(((int)progress) / cant, string.Format("Registro {0} De {1}...", progress.ToString(), cantPlano.Text));
                }
                else
                {
                    this.ResourceManager1.AddScript("{0}.stopTask('longactionprogress');", this.TaskManager1.ClientID);
                    this.Progress1.UpdateProgress(1, "Finalizadoo... !");
                }
            }

        }

        //[DirectMethod(ShowMask = true, Msg = "Grabando...")]
        //public void grabarDepreciacion()
        //{
        //    //try
        //    //{
        //    List<string> sql = new List<string>();
        //    string fechaRevision = Convert.ToDateTime(dfd_fecha.Text).ToString("yyyy-MM-dd");
        //    //if (Session["ListaActivos"] != null)
        //    //{
        //    Conexion_Mysql conexion = new Conexion_Mysql();
        //    conexion.Tiempo = " ; default command timeout=990";
        //    string est = estadoProcDe.Text;
        //    // Genero una copia antes de iniciar la grabación de la primera depreciaciación en el sistema
        //    // Motivo:Si se requiera restaurar los datos de los activos a su estado Inicial. 
        //    if (doc.ConsultarExistenciaDepreciacion() == 0)
        //    {
        //        sql = doc.InsertDepreciacionInicial(fechaRevision);
        //    }

        //    int Codigo = doc.AutoGeneradorID(8);
        //    string Rcontrol = "DP00" + Codigo;
        //    if (Session["estadoProcDe"].ToString() == "true")
        //    {
        //        sql.Add(doc.InsertarDocumentoDepreciacion(Rcontrol, "Depreciacion", fechaRevision));
        //        string etapaguardar = HTIPOGUARDAR.Text;
        //        List<Activo> FiltroActivo = null;
        //        if (etapaguardar == "1")
        //        {
        //            FiltroActivo = Global.listDep.Where(item => item.idsubclase == etapaguardar).ToList();
        //        }
        //        else
        //        {
        //            FiltroActivo = Global.listDep.Where(item => item.idsubclase != etapaguardar).ToList();
        //        }
        //        foreach (Activo act in Global.listDep)
        //        {

        //            sql.Add(string.Format("INSERT INTO depreciacion(idactivo,responsable,vr_residual,vr_deterioro,vr_razonable,vr_depreciacion,ImporteLibros,dep_mes,fecha,login,FyH_Real,IDcontrol,baseDepreciable,IdGrupoAsignacionCentroCosto) "
        //                                                 + " SELECT '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',now(),'{10}','{11}', "
        //                                                   + " (SELECT a.IdgrupoAsignacionCentro from activos_fijos.asignacionactivo a WHERE a.idactivo ='" + act.idactivo + "' AND a.estado='A')",
        //                                                 act.idactivo, act.idresponsable, act.VrResidual.ToString().Replace(',', '.'), act.VrDeterioro.ToString().Replace(',', '.'), act.VrRazonable.ToString().Replace(',', '.'),
        //                                                 act.VrDepreciacion.ToString().Replace(',', '.'), act.Importe_libros.ToString().Replace(',', '.'), act.dep_mes.ToString().Replace(',', '.'), fechaRevision,
        //                                                 HttpContext.Current.User.Identity.Name.ToUpper(), Rcontrol, act.baseDepreciable.ToString().Replace(',', '.')));

        //            sql.Add(string.Format("UPDATE activo SET Fecha_Udepreciacion='{0}',vr_Depreciacion = '{1}',"
        //                                  + "Importe_libros = '{2}',vr_dep_mes = '{3}',BaseDepreciable='{4}'  "
        //                                  + " WHERE idactivo= '{5}'",
        //                                  fechaRevision, act.VrDepreciacion.ToString().Replace(',', '.'), act.Importe_libros.ToString().Replace(',', '.'), act.dep_mes.ToString().Replace(',', '.'), act.baseDepreciable.ToString().Replace(',', '.'), act.idactivo));

        //            foreach (ActivoComponente com in act.Componente)
        //            {
        //                sql.Add(string.Format("INSERT INTO detalle_depreciacion(id_depreciacion,id_componente,depreciacion_mes,"
        //                    + " baseDepreciable,ImporteLibros,dep_acum,unidad_dep,vr_residual,ajust_dep_acum,ajust_vr_razonable, "
        //                    + " vr_deterioro,costo_inicial,idnorma,Fecha,FyH_Real,login,idtipodepreciacion,reexpresionVidaUtil,idactivo,vida_remanente,vida_utilizada,vidaUtil) "
        //                    + "SELECT "
        //                            + "(SELECT max(id_depreciacion) FROM depreciacion), "
        //                            + "'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',"
        //                            + "'{10}','{11}','{12}',now(),'{13}','{14}','{15}','{16}','{17}','{18}','{19}' ",
        //                            com.id_componente, com.vr_dep_mes.ToString().Replace(',', '.'), com.base_deprec.ToString().Replace(',', '.'), com.vr_importe_libros.ToString().Replace(',', '.'),
        //                            com.vr_dep_acumulada.ToString().Replace(',', '.'), com.unidad_dep.ToString().Replace(',', '.'), com.ajust_vr_residual.ToString().Replace(',', '.'), com.vr_dep_acumulada.ToString().Replace(',', '.'),
        //                            com.ajust_vr_razonable.ToString().Replace(',', '.'), com.ajust_vr_deterioro.ToString().Replace(',', '.'), com.costo_inicial.ToString().Replace(',', '.'), com.idtiponorma, act.Fecha_Revision, HttpContext.Current.User.Identity.Name.ToUpper(), com.id_tipodepreciacion, com.ajusteVidaUtil.ToString().Replace(',', '.'), com.id_activo, com.vida_util_remanente.ToString().Replace(',', '.'), com.vida_util_utilizado.ToString().Replace(',', '.'), com.vida_util));

        //                sql.Add(string.Format("UPDATE activo_componente SET vr_dep_acumulada= '{0}',vr_importe_libros= '{1}',"
        //                                     + " vida_util_utilizado='{2}', "
        //                                     + "vida_util_remanente='{3}',base_deprec='{4}', vr_dep_mes='{5}' "
        //                                     + " WHERE id_Componente = '{6}'",
        //                                     com.vr_dep_acumulada.ToString().Replace(',', '.'), com.vr_importe_libros.ToString().Replace(',', '.'),
        //                                     com.vida_util_utilizado.ToString().Replace(',', '.'),
        //                                     com.vida_util_remanente.ToString().Replace(',', '.'),
        //                                     com.base_deprec.ToString().Replace(',', '.'), com.vr_dep_mes.ToString().Replace(',', '.'), com.id_componente));
        //            }

        //        }
        //           sql.Add("update registro_control set Numero=" + (Codigo + 1) + " where Codigo=8");
        //        this.Session.Remove("estadoProcDe");
        //        //this.Session.Remove("ListaActivos");

        //        if (conexion.EjecutarTransaccion(sql))
        //        {

        //            if (etapaguardar == "1")
        //            {
        //                HTIPOGUARDAR.Text = "2";
        //                btnGrabar.Enable();
        //            }
        //            else
        //            {
        //                BGUARDARFALT.Enable();
        //                Global.listDep = null;
        //            }


        //            X.Msg.Info(new InfoPanel
        //            {
        //                Title = "Notificación",
        //                Icon = Icon.Accept,
        //                Html = "Registros Grabados.",
        //                HideDelay = 1500,

        //            }).Show();
        //        }
        //        else
        //        {
        //            X.Msg.Info(new InfoPanel
        //            {
        //                Title = "Notificación",
        //                Icon = Icon.Accept,
        //                Html = "Error al guardar.",
        //                HideDelay = 1500,

        //            }).Show();
        //        }

        //        btnCargarPlano.Disabled = true;
        //        btn_procesar.Disabled = true;
        //        //btnGrabar.Disabled = true;
        //        cargarGrillaActivos();
        //        FechaUDepreciacionMinima();
        //        Progress1.UpdateProgress(0, "    ");



        //    }
        //    else
        //    {
        //        X.Msg.Info(new InfoPanel
        //        {
        //            Title = "DEPRECIACION NO REALIZADA",
        //            Icon = Icon.Exclamation,
        //            Html = "DEBE REALIZAR EL PROCESO DE DEPRECIACION ANTES."
        //        }).Show();
        //    }
        //    //}
        //    //else
        //    //{
        //    //    X.Msg.Info(new InfoPanel
        //    //    {
        //    //        Title = "CARGA DE ACTIVOS ERRONEA",
        //    //        Icon = Icon.Exclamation,
        //    //        Html = "VUELVE A CARGAR LOS ACTIVOS"
        //    //    }).Show();
        //    //}

        //    //}
        //    //catch (Exception exc)
        //    //{
        //    //    X.MessageBox.Show(new MessageBoxConfig
        //    //    {
        //    //        Title = "Sigc. #_Error.",
        //    //        Message = "Error: " + exc.Message + exc.StackTrace,
        //    //        Buttons = MessageBox.Button.OK,
        //    //        Icon = MessageBox.Icon.ERROR,
        //    //        Closable = false
        //    //    });
        //    //}
        //}



        protected void IniciarProcesoGuardar(object sender, DirectEventArgs e)
        {

                formarSql();
                Conexion_Mysql conexion = new Conexion_Mysql();
                conexion.Tiempo = " ; default command timeout=990";
                cantPlano.Text = Global.sqldep.Count.ToString();

                this.Session["LongActionProgress"] = 0;
                ThreadPool.QueueUserWorkItem(guardar);
                this.ResourceManager1.AddScript("{0}.startTask('longactionprogress');", this.TaskManager1.ClientID);
               
        }


        [DirectMethod]
        public void guardar(object state)
        {
            
            Conexion_Mysql conexion = new Conexion_Mysql();
            conexion.Tiempo = " ; default command timeout=990";
            conexion.EjecutarTransaccion1(Global.sqldep,Session);
            this.Session.Remove("LongActionProgress");
            //if (conexion.EjecutarTransaccion(Global.sqldep))
            //{

            //    X.Msg.Info(new InfoPanel
            //    {
            //        Title = "Notificación",
            //        Icon = Icon.Accept,
            //        Html = "Registros Grabados.",
            //        HideDelay = 1500,

            //    }).Show();
            //}
            //else
            //{
            //    X.Msg.Info(new InfoPanel
            //    {
            //        Title = "Notificación",
            //        Icon = Icon.Accept,
            //        Html = "Error al guardar.",
            //        HideDelay = 1500,

            //    }).Show();
            //}
        }

       
        public void formarSql()
        {
            List<string> sql = new List<string>();
            string fechaRevision = Convert.ToDateTime(dfd_fecha.Text).ToString("yyyy-MM-dd");
            try
            {
                if (doc.ConsultarExistenciaDepreciacion() == 0)
                {
                    sql = doc.InsertDepreciacionInicial(fechaRevision);
                }
                int Codigo = doc.AutoGeneradorID(8);
                string Rcontrol = "DP00" + Codigo;
                if (Session["estadoProcDe"].ToString() == "true")
                {
                    sql.Add(doc.InsertarDocumentoDepreciacion(Rcontrol, "Depreciacion", fechaRevision));

                    foreach (Activo act in Global.listDep)
                    {

                        sql.Add(string.Format("UPDATE activo SET Fecha_Udepreciacion='{0}',vr_Depreciacion = '{1}',"
                                              + "Importe_libros = '{2}',vr_dep_mes = '{3}',BaseDepreciable='{4}'  "
                                              + " WHERE idactivo= '{5}'",
                                              fechaRevision, act.VrDepreciacion.ToString().Replace(',', '.'), act.Importe_libros.ToString().Replace(',', '.'), act.dep_mes.ToString().Replace(',', '.'), act.baseDepreciable.ToString().Replace(',', '.'), act.idactivo));

                        sql.Add(string.Format("INSERT INTO depreciacion(idactivo,responsable,vr_residual,vr_deterioro,vr_razonable,vr_depreciacion,ImporteLibros,dep_mes,fecha,login,FyH_Real,IDcontrol,baseDepreciable,IdGrupoAsignacionCentroCosto) "
                                                             + " VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',now(),'{10}','{11}', "
                                                               + " (SELECT a.IdgrupoAsignacionCentro from activos_fijos.asignacionactivo a WHERE a.idactivo ='" + act.idactivo + "' AND a.estado='A'));",
                                                             act.idactivo, act.idresponsable, act.VrResidual.ToString().Replace(',', '.'), act.VrDeterioro.ToString().Replace(',', '.'), act.VrRazonable.ToString().Replace(',', '.'),
                                                             act.VrDepreciacion.ToString().Replace(',', '.'), act.Importe_libros.ToString().Replace(',', '.'), act.dep_mes.ToString().Replace(',', '.'), fechaRevision,
                                                             HttpContext.Current.User.Identity.Name.ToUpper(), Rcontrol, act.baseDepreciable.ToString().Replace(',', '.')));

                        //sql.Add("Select @var:=max(id_depreciacion) FROM depreciacion ;");

                        foreach (ActivoComponente com in act.Componente)
                        {
                            sql.Add(string.Format("INSERT INTO detalle_depreciacion(id_depreciacion,id_componente,depreciacion_mes,"
                                + " baseDepreciable,ImporteLibros,dep_acum,unidad_dep,vr_residual,ajust_dep_acum,ajust_vr_razonable, "
                                + " vr_deterioro,costo_inicial,idnorma,Fecha,FyH_Real,login,idtipodepreciacion,reexpresionVidaUtil,idactivo,vida_remanente,vida_utilizada,vidaUtil) "
                                + "Values ("
                                        + "LAST_INSERT_ID(), "
                                        + "'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',"
                                        + "'{10}','{11}','{12}',now(),'{13}','{14}','{15}','{16}','{17}','{18}','{19}') ",
                                        com.id_componente, com.vr_dep_mes.ToString().Replace(',', '.'), com.base_deprec.ToString().Replace(',', '.'), com.vr_importe_libros.ToString().Replace(',', '.'),
                                        com.vr_dep_acumulada.ToString().Replace(',', '.'), com.unidad_dep.ToString().Replace(',', '.'), com.ajust_vr_residual.ToString().Replace(',', '.'), com.vr_dep_acumulada.ToString().Replace(',', '.'),
                                        com.ajust_vr_razonable.ToString().Replace(',', '.'), com.ajust_vr_deterioro.ToString().Replace(',', '.'), com.costo_inicial.ToString().Replace(',', '.'), com.idtiponorma, act.Fecha_Revision, HttpContext.Current.User.Identity.Name.ToUpper(), com.id_tipodepreciacion, com.ajusteVidaUtil.ToString().Replace(',', '.'), com.id_activo, com.vida_util_remanente.ToString().Replace(',', '.'), com.vida_util_utilizado.ToString().Replace(',', '.'), com.vida_util));

                            sql.Add(string.Format("UPDATE activo_componente SET vr_dep_acumulada= '{0}',vr_importe_libros= '{1}',"
                                                 + " vida_util_utilizado='{2}', "
                                                 + "vida_util_remanente='{3}',base_deprec='{4}', vr_dep_mes='{5}' "
                                                 + " WHERE id_Componente = '{6}'",
                                                 com.vr_dep_acumulada.ToString().Replace(',', '.'), com.vr_importe_libros.ToString().Replace(',', '.'),
                                                 com.vida_util_utilizado.ToString().Replace(',', '.'),
                                                 com.vida_util_remanente.ToString().Replace(',', '.'),
                                                 com.base_deprec.ToString().Replace(',', '.'), com.vr_dep_mes.ToString().Replace(',', '.'), com.id_componente));
                        }
                    }

                    sql.Add("update registro_control set Numero=" + (Codigo + 1) + " where Codigo=8");
                    Global.listDep = null;
                    Global.sqldep = sql;

                }
                //X.MessageBox.Notify("Error", "Datos Cargados").Show();

            }
            catch (Exception)
            {
                X.MessageBox.Notify("Error", "Datos no Cargados").Show();
            }

        }

        /// <summary>
        /// metodo inicializa la barra de progreso e inicia proceso de depreciacion.
        /// </summary>
        /// <param name="state"></param>
        private void ProcesarEjecucion(object state)
        {
            Session["estadoProcDe"] = "false";
            int posicion = 0;
            ListaActivos = (List<Activo>)Session["ListaActivos"];
            int cant = ListaActivos.Count;
            string est = estadoPlano.Text;
            string fechaRegistro = Convert.ToDateTime(dfd_fecha.Text).ToString("yyyy-MM-dd");

            try
            {
                var count = ListaActivos
                    .Where(x => Convert.ToDateTime(fechaRegistro) > Convert.ToDateTime(x.Fecha_Udepreciacion))
                    .Count();
                datosDepreciacion = (List<AtributosDepreciacion>)Session["datosDepreciacion"];

                if (est == "true")
                {
                    foreach (Activo activo in ListaActivos)
                    {

                        List<AtributosDepreciacion> FiltroDepActivo = datosDepreciacion.Where(item => item.IdActivo == activo.idactivo).ToList();
                        int cantFiltro = FiltroDepActivo.Count;
                        activo.Componente = new List<ActivoComponente>();
                        foreach (AtributosDepreciacion Componente in FiltroDepActivo)
                        {
                            activo.Componente.Add(ProDepPlano(Componente, activo.Fecha_Udepreciacion, fechaRegistro));
                        }

                        activo.dep_mes = activo.Componente.Where(item => item.idtiponorma == "1").Sum(y => y.vr_dep_mes);
                        activo.VrDepreciacion = activo.Componente.Where(item => item.idtiponorma == "1").Sum(y => y.vr_dep_acumulada);

                        activo.Fecha_Revision = fechaRegistro;

                        activo.baseDepreciable = activo.Componente.Where(item => item.idtiponorma == "1").Sum(y => y.base_deprec);
                        activo.Importe_libros = activo.Componente.Where(item => item.idtiponorma == "1").Sum(y => y.vr_importe_libros);

                        posicion = posicion + 1;


                        // Thread.Sleep(1);
                        this.Session["LongActionProgress"] = posicion;
                    }
                    //TempDep(ListaActivos);
                    Global.listDep = ListaActivos;
                    Session.Remove("ListaActivos");
                    Session.Remove("datosDepreciacion");

                    //TempDep(ListaActivos);
                    //Session["ListaActivos"] = ListaActivos;
                    Session["estadoProcDe"] = "true";
                }
                else
                {

                    foreach (Activo activo in ListaActivos)
                    {
                        activo.Componente = new List<ActivoComponente>();

                        foreach (ActivoComponente Componente in ProDepFecha(activo.idactivo, activo.Fecha_Udepreciacion, fechaRegistro))
                        {
                            activo.Componente.Add(Componente);
                        }


                        var FiltroNorma = activo.Componente.Where(item => item.id_activo == activo.idactivo);
                        activo.dep_mes = FiltroNorma.Where(item => item.idtiponorma == "1").Sum(y => y.vr_dep_mes);
                        activo.VrDepreciacion = FiltroNorma.Where(item => item.idtiponorma == "1").Sum(y => y.vr_dep_acumulada);
                        activo.baseDepreciable = FiltroNorma.Where(item => item.idtiponorma == "1").Sum(y => y.base_deprec);
                        activo.Importe_libros = FiltroNorma.Where(item => item.idtiponorma == "1").Sum(y => y.vr_importe_libros);
                        activo.Fecha_Revision = fechaRegistro;
                        posicion = posicion + 1;

                        this.Session["LongActionProgress"] = posicion;

                    }
                    Global.listDep = ListaActivos;
                    Session.Remove("ListaActivos");
                    Session.Remove("datosDepreciacion");

                    //TempDep(ListaActivos);
                    //Session["ListaActivos"] = ListaActivos;
                    Session["estadoProcDe"] = "true";
                }



                this.Session.Remove("LongActionProgress");

            }
            catch (Exception exc)
            {

                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Sigc. #_Error.",
                    Message = "Error: " + exc.Message + exc.StackTrace,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR,
                    Closable = false
                });
            }


        }
        #region PLANOS

        /// <summary>
        /// Guarda temporalmente los datos de la depreciacion.
        /// </summary>
        /// <param name="json"></param>
        /// <param name="parte"></param>
        public void TempDep(List<Activo> ListaActivos)
        {
            try
            {
                var activo = JsonConvert.SerializeObject(ListaActivos);
                string id_file = "activo.txt";
                string FilePath = HttpRuntime.AppDomainAppPath + @"TempDep\" + id_file;
                StreamWriter plano = new StreamWriter(FilePath);
                plano.WriteLine(activo);
                plano.Close();
                foreach (Activo row in ListaActivos)
                {

                    foreach (ActivoComponente com in row.Componente)
                    {
                        var compIndividual = JsonConvert.SerializeObject(row.Componente);

                    }


                }
                //";




            }
            catch (Exception e)
            {
                this.Response.End();
            }
        }
        [DirectMethod(Msg = "Cargando...", ShowMask = true, Target = MaskTarget.CustomTarget)]
        public void Excel()
        {

            try
            {
                List<Activo> ListaActivos = Global.listDep;
                string jsonn1 = JsonConvert.SerializeObject(Informe.ListaActivosDeprExcel(ListaActivos, "Parte1"));
                ListaActivos = null;
                string id_file = "prueba.txt";
                string FilePath = HttpRuntime.AppDomainAppPath + @"Planos\" + id_file;
                StreamWriter plano = new StreamWriter(FilePath);
                plano.WriteLine(jsonn1);
                plano.Close();
                Thread.Sleep(5000);
                X.AddScript("descargaCSV('../Planos/prueba.txt');");
            }
            catch (Exception)
            {

                Response.End();
            }

        }
        #endregion


        [DirectMethod]
        public void cargarDatosDepreciacion(int idactivo)
        {


            try
            {
                ListaActivos = (List<Activo>)Session["ListaActivos"];
                Activo FiltroActivo = ListaActivos.Find(item => item.idactivo == idactivo);
                if (FiltroActivo.Componente.Count > 0)
                {
                    store_depTem.DataSource = FiltroActivo.Componente;
                    store_depTem.DataBind();
                    win_componente.Show();
                }
                else
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Notificación",
                        Message = "Este Activo no esta depreciado!",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.INFO,
                        Closable = true
                    });
                }
            }
            catch (Exception exc)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Sigc",
                    Message = "Debe Primero Procesar La Depreciaciòn",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.INFO,
                    Closable = true
                });

            }
        }
        #region Anulaciones
        [DirectMethod(ShowMask = true, Msg = "Consultando..", Target = MaskTarget.Page)]
        public void ConsultarUltimaDepreciacion()
        {
            DataTable dt = doc.ConsultarUltimaFechaDepreciacion().Tables[0];
            string TitleAnulacion = "ANULAR DEPRECIACION N°" + dt.Rows[0]["Ncontrol"].ToString() + " - Realizado:" + dt.Rows[0]["FechaDepreciacion"].ToString();
            WANULACIONMESDEPRECIACION.Title = TitleAnulacion;

        }
        [DirectMethod(ShowMask = true, Msg = "Restaurando Cambios..", Target = MaskTarget.Page)]
        public void AnularDepreciacionMes(string ConceptoAnulacion)
        {
            DataSet dt = doc.ConsultarDepreciacionAnterior();
            if (dt.Tables[0].Rows.Count > 0)
            {
                string Control = dt.Tables[0].Rows[0]["Ncontrol"].ToString();
                string FechaDep = Convert.ToDateTime(dt.Tables[0].Rows[0]["Fecha"].ToString()).ToString("yyyy-MM-dd");
                if (doc.AnularDepreciacionporMes(FechaDep, Control, ConceptoAnulacion, HttpContext.Current.User.Identity.Name.ToUpper()))
                {
                    mens.CuadroMensaje("Notificación", "Información Restaurada a la Depreciacón: (" + Control + ") " + FechaDep);
                }
                else
                {
                    mens.NotificacionArriba("Notificación", "Ha ocurrido un error..");
                }
            }

            else
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Notificación",
                    Message = "Esta es la Primera Depreciación Registrada en el Sistema!",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.INFO,
                    Closable = true
                });
            }
        }

        #endregion

        #region Notificaciones de Advertencias.
        //public void ConsultarActivosPendientesAlta() { 
        //      string sql = 
        //}
        #endregion


    }
}