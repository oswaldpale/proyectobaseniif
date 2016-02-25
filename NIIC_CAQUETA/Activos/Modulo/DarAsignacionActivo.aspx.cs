using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Activos.Clases;
using Ext.Net;
using System.IO;

namespace Activos.Modulo
{
    public partial class AsignacionActivo : SigcWeb.API.Shared.Page
    {
        public List<AtributosDepreciacion> PlanoAsignacion = new List<AtributosDepreciacion>();
        private Parametrizacion doc = new Parametrizacion();
        public List<AsignarActivo> listAsignacion = new List<AsignarActivo>();
        protected override void Page_Load(object sender, EventArgs e)
        {
            //base.Page_Load(sender, e);
            Global.path2 = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.LastIndexOf("/"));
            if (!X.IsAjaxRequest)
            {
                try
                {
                    CargarDatos();

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

        private void CargarDatos()
        {
            List<ItemsAsignacion> Items_Detalle = new List<ItemsAsignacion>();
            DataTable FacturaTemp = new DataTable();
            DataTable FacturaBorrada = new DataTable();
            DataTable itemsActivo = new DataTable();
            Session["Items_Detalle"] = Items_Detalle;
            Conexion_Mysql conexion = new Conexion_Mysql();
            DataSet dt = conexion.EjecutarSelectMysql("SELECT id_empleado,No_documento,CONCAT_WS(' ',Nombres, Apellido1,Apellido2) as NombreEmpleado FROM empleado");
            Store_empleado.DataSource = dt;
            Store_empleado.DataBind();

            dt = conexion.EjecutarSelectMysql("SELECT MAltaId as codigo,Motivo as motivo FROM tb_motivo_alta");
            cbx_motivo.GetStore().DataSource = dt;
            cbx_motivo.DataBind();

            dt = conexion.EjecutarSelectMysql("SELECT codigo,id_centro as idcosto,Upper(Centro_costo) as costo FROM  tb_centro_costo WHERE estado='A' ");

            storeScosto.DataSource = dt;
            storeScosto.DataBind();


            dt = conexion.EjecutarSelectMysql("SELECT id_centro as codigo,Centro_Economico as economico, codigo as codCentro FROM tb_centro_economico");
            Session["ListaCentroEconomico"] = dt;
            cbx_economico.GetStore().DataSource = dt;
            cbx_economico.DataBind();

            dt = conexion.EjecutarSelectMysql("SELECT Codigo as codigo, Nombre as dependencia FROM dependencia");
            cbx_dependencia.GetStore().DataSource = dt;
            cbx_dependencia.DataBind();

            store_fact_dep.DataSource = dt;
            store_fact_dep.DataBind();

            dt = conexion.EjecutarSelectMysql("SELECT id_uge as codigo,Unidad_g_Efectivo as uge FROM tb_uge");
            cbx_uge.GetStore().DataSource = dt;
            cbx_uge.DataBind();


            dt = conexion.EjecutarSelectMysql("SELECT id_funcion as codigo,Funcion as funcion  FROM tb_funcion");
            cbxFuncion.GetStore().DataSource = dt;
            cbxFuncion.DataBind();

            store_fact_func.DataSource = dt;
            store_fact_func.DataBind();

            dt = conexion.EjecutarSelectMysql(" SELECT a.idActivo AS codigo, a.placa, a.id_subclase AS IdSubClase, "
                                                + " a.Nombre AS nombre, c.NCompra AS NoCompra,c.NFactura AS "
                                                + " NFactura,a.CostoInicial,a.Importe_libros "
                                                + " FROM activo a "
                                                + " INNER JOIN compra c ON a.NoCompra=c.NCompra AND NOrdenCompra IS NULL AND c.Estado='AC' "
                                                + " WHERE a.id_empleado IS NULL AND a.Estado='A' ");
            DataColumn[] dcpk = new DataColumn[1];
            dcpk[0] = dt.Tables[0].Columns["codigo"];

            dt.Tables[0].PrimaryKey = dcpk;

            Session["Items"] = (DataTable)dt.Tables[0];

            dt = conexion.EjecutarSelectMysql("SELECT DISTINCT a.NCompra,a.NFactura,a.Fecha,a.Observacion, "
                                            + " CONCAT(c.Clase,'(',s.Subclase,')') AS Items, d.IdClase,d.IdSubClase "
                                            + " FROM compra a "
                                            + " INNER JOIN detallecompra d ON a.NCompra = d.NCompra "
                                            + " INNER JOIN( "
                                                + " SELECT * "
                                                + " FROM activo ac "
                                                + " WHERE ac.id_empleado IS NULL AND ac.Estado='A') ac "
                                            + " ON d.NCompra = ac.NoCompra AND 	d.IdSubClase = ac.id_subclase "
                                            + " INNER JOIN tb_clase c ON d.IdClase = c.id_clase "
                                            + " INNER JOIN tb_subclase s ON d.IdSubClase = s.id_subclase "
                                            + " WHERE a.Estado='AC' AND a.NOrdenCompra IS NULL ");


            FacturaTemp = (DataTable)dt.Tables[0];
            Session["FacturaTemp"] = FacturaTemp;

            dfd_fecha.MaxDate = DateTime.Today;

        }

        protected void TriggerField1_Click(object sender, DirectEventArgs e)
        {
            Conexion_Mysql conexion = new Conexion_Mysql();
            DataTable dt = (DataTable)Session["items"];
            GridActivo.GetStore().DataSource = dt;
            GridActivo.GetStore().DataBind();
        }


        protected void Select_CentroCosto(object sender, DirectEventArgs e)
        {
            CodigoCosto.Text = e.ExtraParams["CodigoCosto"].ToString();
            string NombreCosto = e.ExtraParams["NombreCosto"].ToString();
            Drop2.SetValue(NombreCosto);

        }
        protected void TriggerField2_Click(object sender, DirectEventArgs e)
        {
            Conexion_Mysql conexion = new Conexion_Mysql();
            DataSet dt = conexion.EjecutarSelectMysql("SELECT id_funcion as codigo,Funcion as funcion  FROM tb_funcion");
            Store_funcion.DataSource = dt;
            Store_funcion.DataBind(); ;
        }
        protected void TriggerField3_Click(object sender, DirectEventArgs e)
        {
            Conexion_Mysql conexion = new Conexion_Mysql();
            DataSet dt = conexion.EjecutarSelectMysql("SELECT id_empleado, CONCAT_WS(' ',Nombres, Apellido1,Apellido2) as " + "NombreEmpleado FROM empleado");

            Store_empleado.DataSource = dt;
            Store_empleado.DataBind();
        }


        /// <summary>
        /// agregar item(activo) a la grilla de forma temporal
        /// </summary>
        [DirectMethod(ShowMask = true, Msg = "Ingresando ...", Target = MaskTarget.Page)]
        public void ingresarItems(Newtonsoft.Json.Linq.JArray record, string[] values)
        {
            try
            {
                DataTable ItemsTemp = (DataTable)Session["items"];
                DataRow nRow = ItemsTemp.Rows.Find(idActivo.Text);

                ItemsTemp.Rows.Remove(nRow);

                Session["items"] = ItemsTemp;
                Boolean ing = true;

                for (int i = 0; i < record.Count; i++)
                {

                    if (record[i]["placa"].ToString().Equals(PlacaActivo.Text))
                    {

                        ing = false;
                        TriggerField1.Reset();
                        throw new Exception("Este Activo ya se ha ingresado..");
                    }

                }

                if (ing == true)
                {
                    IngresarItemsLista(idActivo.Text, PlacaActivo.Text, NombreActivo.Text, values[0].ToString(), values[1].ToString(), values[2].ToString(), values[3].ToString(), NCompra.Text, NFactura.Text, idSubclase.Text);
                }
                TriggerField1.Reset();
                cbx_dependencia.Reset();
                cbxFuncion.Reset();
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


        public void IngresarItemsLista(string idactivo, string placa, string nombreActivo, string iddepdendencia, string dependencia, string idfuncion, string funcion, string ncompra, string nfactura, string idsubclase)
        {
            List<ItemsAsignacion> Items_Detalle = (List<ItemsAsignacion>)Session["Items_Detalle"];
            ItemsAsignacion items = new ItemsAsignacion()
            {
                idactivo = idactivo,
                placa = placa,
                activo = nombreActivo,
                iddependecia = iddepdendencia,
                dependencia = dependencia,
                idfuncion = idfuncion,
                funcion = funcion,
                ncompra = ncompra,
                nfactura = nfactura,
                idsubclase = idSubclase.Text

            };
            Items_Detalle.Add(items);
            store_asignaciones.DataSource = Items_Detalle;
            store_asignaciones.DataBind();
            Session["Items_Detalle"] = Items_Detalle;
        }


        [DirectMethod(ShowMask = true, Msg = "Guardando ...", Target = MaskTarget.Page)]
        public void grabarAsignaciones(Newtonsoft.Json.Linq.JArray recordCcosto)
        {
            try
            {
                Conexion_Mysql conexion = new Conexion_Mysql();
                string fecha = Convert.ToDateTime(dfd_fecha.Text).ToString("yyyy-MM-dd");
                List<string> sql = new List<string>();
                DataSet dt = conexion.EjecutarSelectMysql("SELECT rc.Numero FROM registro_control rc WHERE Codigo=6");
                long codigo = Convert.ToInt64(dt.Tables[0].Rows[0][0].ToString());
                string NAsig = string.Empty, _cons = string.Empty;
                _cons = "00000000" + codigo.ToString();
                NAsig = "AS-" + _cons.Substring(_cons.Length - 8);
                NAsignacion.Text = NAsig;

                List<ItemsAsignacion> recordActivo = (List<ItemsAsignacion>)Session["Items_Detalle"];
                int PrimaryKey = doc.nextPrimaryKey("IdgrupoAsignacionCentro", "grupo_asignacionescentrocosto");
                string sentencia = "";
                sentencia = "INSERT "
                                  + "INTO "
                                  + "    grupo_asignacionescentrocosto "
                                  + "    ( "
                                  + "        IdgrupoAsignacionCentro, "
                                  + "        Idempleado, "
                                  + "        IdUge, "
                                  + "        idMotivo, "
                                  + "        idcentroeconomico, "
                                  + "        RControl, "
                                  + "        Fecha, "
                                  + "        Observacion "

                                  + "    ) "
                                  + "    VALUES "
                                  + "    ( "
                                  + "        " + PrimaryKey + ", "
                                  + "        " + idresponsable.Text + ", "
                                  + "        " + cbx_uge.Text + ", "
                                  + "        " + cbx_motivo.Text + ", "
                                  + "        " + cbx_economico.Text + ", "
                                  + "        '" + NAsignacion.Text + "', "
                                  + "        '" + fecha + "', "
                                  + "        '" + txt_conceptos.Text + "' "
                                  + "    )";
                sql.Add(sentencia);

                for (int i = 0; i < recordCcosto.Count; i++)
                {

                    sentencia = "INSERT "
                            + "INTO "
                            + "    detalle_centrocosto_asignacionactivo "
                            + "    ( "
                            + "        id_centro, "
                            + "        porcentaje, "
                            + "        login, "
                            + "        FyH_Real, "
                            + "        IdgrupoAsignacionCentro "
                            + "    ) "
                            + "    VALUES "
                            + "    ( "
                            + "        '" + recordCcosto[i]["idcentro"] + "', "
                            + "        '" + recordCcosto[i]["porcentaje"] + "', "
                            + "        '" + HttpContext.Current.User.Identity.Name.ToUpper() + "', "
                            + "        now(), "
                            + "        '" + PrimaryKey + "'"
                            + "    )";
                    sql.Add(sentencia);
                }

                foreach (ItemsAsignacion record in recordActivo)
                {



                    sql.Add(string.Format("UPDATE activo SET id_empleado = '{0}',id_centro_economico='{1}',  id_condicion ='1', "
                                         + "id_uge='{2}',id_dependencia='{3}',id_funcion='{4}' "
                                         + " WHERE idActivo = '{5}'",
                                         idresponsable.Text,
                                         cbx_economico.Text,
                                         cbx_uge.Text,
                                         record.iddependecia,
                                         record.idfuncion,
                                         record.idactivo
                                        )
                           );
                    sql.Add(" UPDATE "
                             + " asignacionactivo "
                             + " SET "
                             + "    FyH_Real = NOW(), "
                             + "    estado = 'I' "
                             + " WHERE "

                             + "   idactivo  =' " + record.idactivo + "'");

                    sentencia = "INSERT "
                                         + "INTO "
                                         + "    asignacionactivo "
                                         + "    ( "
                                         + "        idactivo, "
                                         + "        idfuncion, "
                                         + "        FyH_Real, "
                                         + "        iddependencia, "
                                         + "        fecha, "
                                         + "        IdgrupoAsignacionCentro, "
                                         + "        estado, "
                                         + "        IdFactura, "
                                         + "        login, "
                                         + "        CostoInicial, "
                                         + "        Importe_libros "
                                         + "    ) "
                                         + "    VALUES "
                                         + "    ( "
                                         + "        '" + record.idactivo + "', "
                                         + "        '" + record.idfuncion + "', "
                                         + "        now(), "
                                         + "        '" + record.iddependecia + "', "
                                         + "        '" + fecha + "', "
                                         + "        '" + PrimaryKey + "', "
                                         + "        'A', "
                                         + "        '" + record.nfactura + "', "
                                         + "        '" + HttpContext.Current.User.Identity.Name.ToUpper() + "', "
                                         + " (SELECT Importe_libros FROM activos_fijos.activo WHERE idActivo = '" + record.idactivo + "'), "
                                         + " (SELECT CostoInicial FROM activos_fijos.activo WHERE idActivo = '" + record.idactivo + "') "
                                         + "    )";

                    sql.Add(sentencia);

                }
                sql.Add("update registro_control set Numero=" + (codigo + 1) + " where Codigo=6");
                if (conexion.EjecutarTransaccion(sql
                    ))
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Sigc.",
                        Message = "La asignacion se ha realizado con éxito",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.INFO,
                        Closable = true
                    });

                }
                else
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Sigc.",
                        Message = "Error,A ocurrido un error en la Transacción",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.INFO,
                        Closable = true
                    });
                }

                btn_grabar.Disabled = true;


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


        /// <summary>
        /// Restaura el items se invoca del boton eliminar de la grilla
        /// </summary>
        /// <param name="idactivo"></param>
        /// <param name="placa"></param>
        /// <param name="nombreActivo"></param>
        /// 
        [DirectMethod(ShowMask = true, Msg = "Cargando ...", Target = MaskTarget.Page)]
        public void RestaurarItems(string idactivo, string placa, string nombreActivo, string idsubclase, string ncompra, string nfactura)
        {
            DataTable itemsTemp = (DataTable)Session["items"];
            List<ItemsAsignacion> itemsDetalle = (List<ItemsAsignacion>)Session["Items_Detalle"];
            ItemsAsignacion FiltroActivo = itemsDetalle.Find(x => x.idactivo == idactivo);
            DataRow row = itemsTemp.NewRow();
            row["codigo"] = idactivo;
            row["placa"] = placa;
            row["nombre"] = nombreActivo;
            row["IdSubClase"] = idsubclase;
            row["NoCompra"] = ncompra;
            row["NFactura"] = nfactura;
            itemsTemp.Rows.Add(row);
            itemsDetalle.Remove(FiltroActivo);
            Session["items"] = itemsTemp;
            Session["Items_Detalle"] = itemsDetalle;
        }

        /// <summary>
        /// Se asigna los centros costo con sus respectivo porcentajes
        /// </summary>
        /// <param name="record"></param>
        /// <param name="values"></param>
        [DirectMethod(ShowMask = true, Msg = "Cargando ...", Target = MaskTarget.Page)]
        public void AgregarCentroCosto(Newtonsoft.Json.Linq.JArray record, string[] values)
        {
            Conexion_Mysql conexion = new Conexion_Mysql();


            try
            {
                DataSet dt = conexion.EjecutarSelectMysql(string.Format("SELECT codigo FROM tb_centro_costo WHERE id_centro='{0}'", values[0].ToString()));
                string cod = dt.Tables[0].Rows[0]["codigo"].ToString();
                Boolean ing = true;
                int sumaPorc = Convert.ToInt32(values[2].ToString());

                for (int i = 0; i < record.Count; i++)
                {
                    sumaPorc += Convert.ToInt32(record[i]["porcentaje"]);

                    if (record[i]["idcentro"].ToString().Equals(values[0].ToString()))
                    {
                        ing = false;
                        Drop2.Reset();
                        throw new Exception("Este Centro de Costo ya se ha ingresado");
                    }
                    if (sumaPorc > 100)
                    {
                        ing = false;
                        throw new Exception("La sumatoria de los porcentajes debe ser igual al  100%");
                    }
                    if (sumaPorc == 100)
                    {
                        ing = true;
                    }


                }
                if (ing == true)
                {
                    string id = values[0].ToString();
                    string centro = values[1].ToString();
                    string porc = values[2].ToString();

                    store_cCosto.Add(
                                   new
                                   {
                                       idcentro = id,
                                       codigo = cod,
                                       centroCosto = centro,
                                       porcentaje = porc
                                   }
                                     );
                    FrmCentrocosto.Reset();
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

        [DirectMethod]
        public void ValidarPorcentajesCcosto(Newtonsoft.Json.Linq.JArray record)
        {


            try
            {
                string concVaCcosto = "";
                string ConcatenadorCcosto = "";
                int sumaPorc = 0;
                for (int i = 0; i < record.Count; i++)
                {
                    string ForCcosto = "[{0},{1}%] ";

                    string FormatoCentroCosto = "({0})- {1}";


                    ConcatenadorCcosto = ConcatenadorCcosto + string.Format(FormatoCentroCosto, record[i]["codigo"],
                                                                           record[i]["centroCosto"]);
                    sumaPorc += Convert.ToInt32(record[i]["porcentaje"]);

                    concVaCcosto = concVaCcosto + string.Format(ForCcosto, record[i]["codigo"],
                                                                           record[i]["porcentaje"]);
                }
                if (sumaPorc < 100)
                {
                    int dif = 100 - sumaPorc;
                    win_centroCosto.Show();
                    throw new Exception("Faltan el " + dif + "% .¿Desea Cerrar La ventana?   ");
                }
                ConcatenadorCCosto.Text = ConcatenadorCcosto;
                TriggerCentro.Text = concVaCcosto;
            }
            catch (Exception s)
            {


                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Advertencia!",
                    Message = "" + s.Message,
                    Buttons = MessageBox.Button.YESNO,
                    Icon = MessageBox.Icon.WARNING,
                    Closable = false,
                    Fn = new JFunction { Fn = "showResult" }
                });
            }
        }

        protected void GridDatoEmp_SelectRow(object sender, DirectEventArgs e)
        {
            TriggerField2.Text = e.ExtraParams["Nombre"].ToString();
            this.SelectDatos(e.ExtraParams["Codigo"].ToString());
            NombreFuncionario.Text = e.ExtraParams["No_documento"].ToString() + " - " + e.ExtraParams["Nombre"].ToString();
            TriggerField1.Disabled = false;
            cbx_dependencia.Disabled = false;
            idresponsable.Text = e.ExtraParams["Codigo"].ToString();
            cbx_uge.SetValue(1);
        }

        [DirectMethod]
        public void CargarFacturas()
        {
            DataTable FacturaTemp = (DataTable)Session["FacturaTemp"];
            Store_factura.DataSource = FacturaTemp;
            Store_factura.DataBind();

        }

        /// <summary>
        /// Despues de seleecionar la factura, consultas los activos vinculados para luegos cargalos en la grilla de items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FacturaSelect(object sender, DirectEventArgs e)
        {
            NFactura.Text = e.ExtraParams["Nfactura"].ToString();
            NCompra.Text = e.ExtraParams["Ncompra"].ToString();
            idSubclase.Text = e.ExtraParams["IdSubClase"].ToString();
            idClase.Text = e.ExtraParams["IdClase"].ToString();
            string FechaIngreso = e.ExtraParams["FechaIngreso"].ToString();
            dfd_fecha.MinDate = Convert.ToDateTime(DateTime.Parse(FechaIngreso).ToString("yyyy-MM-dd"));
            DataTable FacturaTemp = (DataTable)Session["FacturaTemp"];



        }


        [DirectMethod(ShowMask = true, Msg = "Ingresando ...", Target = MaskTarget.Page)]
        public void AgregarItemsPorFactura(string[] values)
        {

            DataTable Items = (DataTable)Session["items"];

            DataRow[] FiltroItems = Items.Select("NoCompra ='" + NCompra.Text + "' AND IdSubClase ='"
                                                               + idSubclase.Text + "'");

            List<ItemsAsignacion> Items_Detalle = (List<ItemsAsignacion>)Session["Items_Detalle"];
            foreach (DataRow item in FiltroItems)
            {
                ItemsAsignacion itemActivo = new ItemsAsignacion()
                {

                    idactivo = item[0].ToString(),
                    placa = item[1].ToString(),
                    idsubclase = item[2].ToString(),
                    activo = item[3].ToString(),
                    ncompra = item[4].ToString(),
                    nfactura = item[5].ToString(),
                    costoinicial = item[6].ToString(),
                    importelibros = item[7].ToString(),
                    iddependecia = values[0].ToString(),
                    dependencia = values[1].ToString(),
                    idfuncion = values[2].ToString(),
                    funcion = values[3].ToString()
                };
                Items_Detalle.Add(itemActivo);
                Items.Rows.Remove(item);
            }
            store_asignaciones.DataSource = Items_Detalle;
            store_asignaciones.DataBind();
            Session["items"] = Items;
        }

        protected void GridComodato_SelectRow(object sender, DirectEventArgs e)
        {
            TriggerField1.Text = e.ExtraParams["Nombre"].ToString();
            idActivo.Text = e.ExtraParams["NComodato"].ToString();
            PlacaActivo.Text = e.ExtraParams["Placa"].ToString();
            NombreActivo.Text = e.ExtraParams["Nombre"].ToString();
            NCompra.Text = e.ExtraParams["NoCompra"].ToString();
            NFactura.Text = e.ExtraParams["NoFactura"].ToString();
            idSubclase.Text = e.ExtraParams["idSubclase"].ToString();
        }

        protected void GridDatoEmp_SelectRow3(object sender, DirectEventArgs e)
        {
            //  TriggerField3.Text = e.ExtraParams["Funcion"].ToString();
            idfuncion.Text = e.ExtraParams["Codigo"].ToString();


        }


        /// <summary>
        /// Carga los datos de Centro Economico asociado al Funcionario
        /// </summary>
        /// <param name="id"></param>

        [DirectMethod]
        public void SelectDatos(string id)
        {

            Conexion_Mysql conexion = new Conexion_Mysql();
            DataSet dt = conexion.EjecutarSelectMysql("SELECT idcentroeconomico as ce FROM empleado "
                                                      + " WHERE id_empleado=" + id);

            int ce = Convert.ToInt32(dt.Tables[0].Rows[0]["ce"].ToString());

            cbx_economico.Disabled = false;
            cbx_economico.
                SetValue(ce);
        }
        [DirectMethod]
        public void limpiarFormulario()
        {
            Session.Remove("Items");
            Session.Remove("Items_Detalle");
            FormPanel1.Reset();
            TriggerField1.Disabled = true;
            cbx_dependencia.Disabled = true;
            CargarDatos();

        }

        [DirectMethod(ShowMask = true, Msg = "Creando Informe Asignacion en PDF ...", Target = MaskTarget.Page)]
        public void Informe_AsignacionActivo_RptDetalle(Newtonsoft.Json.Linq.JArray recordCcosto, string[] values)
        {
            try
            {

                Conexion_Mysql connect = new Conexion_Mysql();
                ReportePDF pdf = new ReportePDF("rptAsignacion");
                List<string> htm = pdf.Template;

                string cad = "";

                //** Encabezado

                htm[1] = string.Format(htm[1], Global.Empresa, Global.Nit, NAsignacion.Text,
                 DateTime.Parse(dfd_fecha.Text).ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));
                cad = pdf.Inicio;
                //**
                cad += string.Format(htm[2], NombreFuncionario.Text.ToString(), values[0].ToString().ToUpper(),
                                         ConcatenadorCCosto.Text, values[1].ToString().ToUpper());
                cad += htm[3];
                int CostoTotalAlta = 0;
                int ImporteTotalAlta = 0;
                int i = 1;
                List<ItemsAsignacion> recordActivo = (List<ItemsAsignacion>)Session["Items_Detalle"];
                int j = 1;
                foreach (ItemsAsignacion record in recordActivo)
                {
                    string sql = "SELECT Date_format(FechaEntrada, '%d-%m-%Y') as FechaEntrada,CostoInicial, Importe_libros FROM activo WHERE placa ='" + record.placa.ToString() + "'";
                    DataSet dt = connect.EjecutarSelectMysql(sql);

                    foreach (DataRow item in dt.Tables[0].Rows)
                    {
                        cad += string.Format(htm[4], j, item["FechaEntrada"].ToString(), record.placa.ToString().ToUpper(),
                                                   record.activo.ToString().ToUpper(),
                                                   string.Format("{0:n}", item["CostoInicial"]),
                                                   string.Format("{0:n}", item["Importe_libros"])
                                            );

                        CostoTotalAlta = CostoTotalAlta + Convert.ToInt32(item["CostoInicial"].ToString());
                        ImporteTotalAlta = ImporteTotalAlta + Convert.ToInt32(item["Importe_libros"].ToString());
                        j = j + 1;
                    }
                }
                cad += string.Format(htm[5], string.Format("{0:n}", CostoTotalAlta), string.Format("{0:n}", ImporteTotalAlta));
                cad += htm[6];
                cad += string.Format(htm[7], txt_conceptos.Text.ToUpper());
                cad += htm[8];
                cad += htm[9];



                //    cad += string.Format(htm[4], "");


                cad += string.Format(htm[6]);


                cad += pdf.Fin;

                pdf.createPDF(cad, htm[1], "Informe_Asignacion_", pdf.getPageConfig("LETTER", false));
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

        #region RECUPERAR ASIGNACION

        [DirectMethod(ShowMask = true, Msg = "Cargando ...", Target = MaskTarget.Page)]
        public void RecuperarNoAsignacion()
        {

            SRECUPERACION.DataSource = doc.ConsultarGrupoAsignaciones("AS");
            SRECUPERACION.DataBind();
        }
        protected void GRRECUPERACIONASIGNACION(object sender, DirectEventArgs e)
        {
            btn_grabar.Hidden = true;
            BTNACTUALIZAR.Hidden = false;
            TRECUPERARASIGNACION.Text = e.ExtraParams["HRcontrol"].ToString();
            NAsignacion.Text = e.ExtraParams["HRcontrol"].ToString();
            HCodigoGrupo.Text = e.ExtraParams["HCodigo"].ToString();
            dfd_fecha.Text = e.ExtraParams["HFechaRegistro"].ToString();
            TriggerField2.Text = e.ExtraParams["HNombre"].ToString();
            idresponsable.Text = e.ExtraParams["HIdempleado"].ToString();
            NombreFuncionario.Text = TriggerField2.Text;
            cbx_uge.SetValue(Convert.ToInt32(e.ExtraParams["HUge"].ToString()));
            cbx_motivo.SetValue(Convert.ToInt32(e.ExtraParams["HIdmotivo"].ToString()));
            cbx_economico.SetValue(Convert.ToInt32(e.ExtraParams["HIdcentroeconomico"].ToString()));
            txt_conceptos.Text = e.ExtraParams["HObservacion"].ToString();
            DataSet dt = doc.ConsultarCentroCostoAsignado(TRECUPERARASIGNACION.Text);
            store_cCosto.DataSource = dt;
            store_cCosto.DataBind();
            TriggerCentro.Text = doc.FormatoCentroCostoAsignaciones(dt).ElementAt(0);
            ConcatenadorCCosto.Text = doc.FormatoCentroCostoAsignaciones(dt).ElementAt(1);
            dt = doc.ConsultarActivosAsignadoGrupo(TRECUPERARASIGNACION.Text);
            Session.Remove("Items_Detalle");
            List<ItemsAsignacion> Items_Detalle = new List<ItemsAsignacion>();
            foreach (DataRow items in dt.Tables[0].Rows)
            {
                ItemsAsignacion values = new ItemsAsignacion()
                {
                    idactivo = items["idactivo"].ToString(),
                    placa = items["placa"].ToString(),
                    activo = items["activo"].ToString(),
                    iddependecia = items["iddependencia"].ToString(),
                    dependencia = items["depedencia"].ToString(),
                    idfuncion = items["idfuncion"].ToString(),
                    funcion = items["funcion"].ToString(),
                    ncompra = items["ncompra"].ToString(),
                    nfactura = items["nfactura"].ToString(),
                    idsubclase = items["idsubclase"].ToString(),
                };
                Items_Detalle.Add(values);
            }
            store_asignaciones.DataSource = Items_Detalle;
            store_asignaciones.DataBind();
            Session["Items_Detalle"] = Items_Detalle;
        }
        [DirectMethod(ShowMask = true, Msg = "Actualizando ...", Target = MaskTarget.Page)]
        public void ActualizarAsignacion(Newtonsoft.Json.Linq.JArray recordCcosto)
        {
            Conexion_Mysql conexion = new Conexion_Mysql();
            List<string> sql = new List<string>();
            try
            {

                string fecha = Convert.ToDateTime(dfd_fecha.Text).ToString("yyyy-MM-dd");
                sql.Add(doc.RestaurarAsignaciones(TRECUPERARASIGNACION.Text));
                sql.Add(doc.QuitarAsignacionesRealizadas(TRECUPERARASIGNACION.Text));
                sql.Add(doc.QuitarCentroCosto(TRECUPERARASIGNACION.Text));
                sql.Add(doc.ActualizarGrupoAsignacioncCosto(idresponsable.Text, cbx_uge.Text, cbx_motivo.Text, txt_conceptos.Text, fecha, cbx_economico.Text, TRECUPERARASIGNACION.Text));
                for (int i = 0; i < recordCcosto.Count; i++)
                {

                    string sentencia = "INSERT "
                              + "INTO "
                              + "    detalle_centrocosto_asignacionactivo "
                              + "    ( "
                              + "        id_centro, "
                              + "        porcentaje, "
                              + "        login, "
                              + "        FyH_Real, "
                              + "        IdgrupoAsignacionCentro "
                              + "    ) "
                              + "    VALUES "
                              + "    ( "
                              + "        '" + recordCcosto[i]["idcentro"] + "', "
                              + "        '" + recordCcosto[i]["porcentaje"] + "', "
                              + "        '" + HttpContext.Current.User.Identity.Name.ToUpper() + "', "
                              + "        now(), "
                              + "        '" + HCodigoGrupo.Text + "'"
                              + "    )";
                    sql.Add(sentencia);
                }
                List<ItemsAsignacion> recordActivo = (List<ItemsAsignacion>)Session["Items_Detalle"];
                foreach (ItemsAsignacion record in recordActivo)
                {



                    sql.Add(string.Format("UPDATE activo SET id_empleado = '{0}',id_centro_economico='{1}',  id_condicion ='1', "
                                         + "id_uge='{2}',id_dependencia='{3}',id_funcion='{4}' "
                                         + " WHERE idActivo = '{5}'",
                                         idresponsable.Text.ToString(),
                                         cbx_economico.Text,
                                         cbx_uge.Text,
                                         record.iddependecia,
                                         record.idfuncion,
                                         record.idactivo
                                        )
                           );

                    sql.Add("DELETE "
                            + "FROM "
                            + "    asignacionactivo "
                            + "WHERE "
                            + "    idactivo = '" + record.idactivo + "' "
                            + "AND estado = 'I'");

                    string sentencia = "INSERT "
                                         + "INTO "
                                         + "    asignacionactivo "
                                         + "    ( "
                                         + "        idactivo, "
                                         + "        idfuncion, "
                                         + "        FyH_Real, "
                                         + "        iddependencia, "
                                         + "        fecha, "
                                         + "        IdgrupoAsignacionCentro, "
                                         + "        estado, "
                                         + "        IdFactura, "
                                         + "        login, "
                                         + "        CostoInicial, "
                                         + "        Importe_libros "
                                         + "    ) "
                                         + "    VALUES "
                                         + "    ( "
                                         + "        '" + record.idactivo + "', "
                                         + "        '" + record.idfuncion + "', "
                                         + "        now(), "
                                         + "        '" + record.iddependecia + "', "
                                         + "        '" + fecha + "', "
                                         + "        '" + HCodigoGrupo.Text + "', "
                                         + "        'A', "
                                         + "        '" + record.nfactura + "', "
                                         + "        '" + HttpContext.Current.User.Identity.Name.ToUpper() + "', "
                                         + " (SELECT Importe_libros FROM activos_fijos.activo WHERE idActivo = '" + record.idactivo + "'), "
                                         + " (SELECT CostoInicial FROM activos_fijos.activo WHERE idActivo = '" + record.idactivo + "') "
                                         + "    )";

                    sql.Add(sentencia);
                }
                if (conexion.EjecutarTransaccion(sql))
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Sigc.",
                        Message = "Actualizado La Asignación",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.INFO,
                        Closable = true
                    });

                }
                else
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Sigc.",
                        Message = "Error,A ocurrido un error en la Transacción",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.INFO,
                        Closable = true
                    });
                }

                btn_grabar.Disabled = true;

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
        #endregion
        #region REASIGNACION DE ACTIVOS
        [DirectMethod(ShowMask = true, Msg = "Consultando ...", Target = MaskTarget.Page)]
        public void CambiaReasignacion(){
            TRECUPERARASIGNACION.Text = "";
            FRECUPERAR.Collapsible = true;
            FRECUPERAR.Collapsed = true;
            BTNGUARDAREASIGANCION.Hidden = false;
            btn_reporte.Disabled = true;
            dfd_fecha.Reset();
            BTNACTUALIZAR.Hidden = true;

        }
        [DirectMethod(ShowMask = true, Msg = "Guardando ...", Target = MaskTarget.Page)]
        public void grabarReasignacion(Newtonsoft.Json.Linq.JArray recordCcosto) {
            Conexion_Mysql conexion = new Conexion_Mysql();
            string fecha = Convert.ToDateTime(dfd_fecha.Text).ToString("yyyy-MM-dd");
            List<string> sql = new List<string>();
            DataSet dt = conexion.EjecutarSelectMysql("SELECT rc.Numero FROM registro_control rc WHERE Codigo=10");
            long codigo = Convert.ToInt64(dt.Tables[0].Rows[0][0].ToString());
            string NAsig = string.Empty, _cons = string.Empty;
            _cons = "00000000" + codigo.ToString();
            NAsig = "RAS-" + _cons.Substring(_cons.Length - 8);
            NAsignacion.Text = NAsig;

            List<ItemsAsignacion> recordActivo = (List<ItemsAsignacion>)Session["Items_Detalle"];
            int PrimaryKey = doc.nextPrimaryKey("IdgrupoAsignacionCentro", "grupo_asignacionescentrocosto");
            string sentencia = "";
            sentencia = "INSERT "
                              + "INTO "
                              + "    grupo_asignacionescentrocosto "
                              + "    ( "
                              + "        IdgrupoAsignacionCentro, "
                              + "        Idempleado, "
                              + "        IdUge, "
                              + "        idMotivo, "
                              + "        idcentroeconomico, "
                              + "        RControl, "
                              + "        Fecha, "
                              + "        Observacion "

                              + "    ) "
                              + "    VALUES "
                              + "    ( "
                              + "        " + PrimaryKey + ", "
                              + "        " + idresponsable.Text + ", "
                              + "        " + cbx_uge.Text + ", "
                              + "        " + cbx_motivo.Text + ", "
                              + "        " + cbx_economico.Text + ", "
                              + "        '" + NAsignacion.Text + "', "
                              + "        '" + fecha + "', "
                              + "        '" + txt_conceptos.Text + "' "
                              + "    )";
            sql.Add(sentencia);

            for (int i = 0; i < recordCcosto.Count; i++)
            {

                sentencia = "INSERT "
                        + "INTO "
                        + "    detalle_centrocosto_asignacionactivo "
                        + "    ( "
                        + "        id_centro, "
                        + "        porcentaje, "
                        + "        login, "
                        + "        FyH_Real, "
                        + "        IdgrupoAsignacionCentro "
                        + "    ) "
                        + "    VALUES "
                        + "    ( "
                        + "        '" + recordCcosto[i]["idcentro"] + "', "
                        + "        '" + recordCcosto[i]["porcentaje"] + "', "
                        + "        '" + HttpContext.Current.User.Identity.Name.ToUpper() + "', "
                        + "        now(), "
                        + "        '" + PrimaryKey + "'"
                        + "    )";
                sql.Add(sentencia);
            }

            foreach (ItemsAsignacion record in recordActivo)
            {
                
                sql.Add(string.Format("UPDATE activo SET id_empleado = '{0}',id_centro_economico='{1}',  id_condicion ='1', "
                                     + "id_uge='{2}',id_dependencia='{3}',id_funcion='{4}' "
                                     + " WHERE idActivo = '{5}'",
                                     idresponsable.Text,
                                     cbx_economico.Text,
                                     cbx_uge.Text,
                                     record.iddependecia,
                                     record.idfuncion,
                                     record.idactivo
                                    )
                       );
                sql.Add(" UPDATE "
                         + " asignacionactivo "
                         + " SET "
                         + "    FyH_Real = NOW(), "
                         + "    FechaInact= Date_format(NOW(),'%Y-%m-%d'), "
                         + "    estado = 'I' "
                         + " WHERE "

                         + "   idactivo  =' " + record.idactivo + "'"
                         + "   AND  estado ='A' "
                         );

                sentencia = "INSERT "
                                     + "INTO "
                                     + "    asignacionactivo "
                                     + "    ( "
                                     + "        idactivo, "
                                     + "        idfuncion, "
                                     + "        FyH_Real, "
                                     + "        iddependencia, "
                                     + "        fecha, "
                                     + "        IdgrupoAsignacionCentro, "
                                     + "        estado, "
                                     + "        IdFactura, "
                                     + "        login, "
                                     + "        CostoInicial, "
                                     + "        Importe_libros "
                                     + "    ) "
                                     + "    VALUES "
                                     + "    ( "
                                     + "        '" + record.idactivo + "', "
                                     + "        '" + record.idfuncion + "', "
                                     + "        now(), "
                                     + "        '" + record.iddependecia + "', "
                                     + "        '" + fecha + "', "
                                     + "        '" + PrimaryKey + "', "
                                     + "        'A', "
                                     + "        '" + record.nfactura + "', "
                                     + "        '" + HttpContext.Current.User.Identity.Name.ToUpper() + "', "
                                     + " (SELECT Importe_libros FROM activos_fijos.activo WHERE idActivo = '" + record.idactivo + "'), "
                                     + " (SELECT CostoInicial FROM activos_fijos.activo WHERE idActivo = '" + record.idactivo + "') "
                                     + "    )";

                sql.Add(sentencia);

            }
            sql.Add("update registro_control set Numero=" + (codigo + 1) + " where Codigo=10");
            if (conexion.EjecutarTransaccion(sql))
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Sigc.",
                    Message = "La Reasignación se ha realizado con éxito",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.INFO,
                    Closable = true
                });

            }
            else
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Sigc.",
                    Message = "Error,A ocurrido un error en la Transacción",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.INFO,
                    Closable = true
                });
            }
        }
        #endregion
    }
}