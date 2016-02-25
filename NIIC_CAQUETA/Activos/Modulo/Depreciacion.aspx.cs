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
    public partial class Depreciacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.path2 = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.LastIndexOf("/"));

            if (!X.IsAjaxRequest)
            {
                try
                {
                  //  cargarAjusteDepreciacion();

                  //  cargarhistorialDepreciacion();

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
            DataSet dt = conexion.EjecutarSelectMysql("select Nombre as nombre, idActivo as codigo, placa from activo WHERE Fecha_Alta is not null and id_empleado is not null and Estado ='A'");
            GridActivo.GetStore().DataSource = dt;
            GridActivo.GetStore().DataBind();
        }
        protected void GridComodato_SelectRow(object sender, DirectEventArgs e)
        {
            TriggerField1.Text = e.ExtraParams["Nombre"].ToString();
            codigo.Text = e.ExtraParams["NComodato"].ToString();
            cargarAjusteDepreciacion();
            cargarhistorialDepreciacion();
        }
        [DirectMethod(ShowMask = true, Msg = "Cargando....", Target = MaskTarget.Page)]
        public void cargarAjusteDepreciacion()
        {
            try
            {
                Conexion_Mysql conexion = new Conexion_Mysql();

                
               
                DataSet dt = conexion.EjecutarSelectMysql("SELECT  a.Importe_libros as importe,a.vr_deterioro as deterioro,a.vr_razonable as razonable, "+
                                                          " a.vr_residual as residual,a.CostoInicial as costo, a.ajus_dep_acum, a.Fecha_Alta as fecha, " +
                                                          " c.Centro_costo as ccentro, ec.Centro_Economico as economico, " +
                                                          " a.placa as placa, a.vr_Depreciacion as depacum, a.Fecha_Udepreciacion as fechaanterior, "    +
                                                          " a.id_empleado as empleado,CONCAT_WS(' ',e.Nombres, e.Apellido1,e.Apellido2) as nombre FROM activo as a INNER JOIN empleado as e " +
                                                          " ON a.id_empleado=e.id_empleado INNER JOIN tb_centro_costo as c ON a.id_centro_costo=c.id_centro " +
                                                          " INNER JOIN tb_centro_economico as ec ON a.id_centro_economico=ec.id_centro where a.Estado=" + "'A'" + "AND a.idactivo=" + codigo.Text);
                           
                hiddenfechaant.SetValue(dt.Tables[0].Rows[0]["fechaanterior"].ToString());

                txt_valor.Text = dt.Tables[0].Rows[0]["depacum"].ToString();
                dfd_fechaAlta.SetValue(DateTime.Parse(dt.Tables[0].Rows[0]["fecha"].ToString()).ToString("yyyy-MM-dd"));
                txt_ajusAcum.Text = dt.Tables[0].Rows[0]["ajus_dep_acum"].ToString();
                txt_placa.Text = dt.Tables[0].Rows[0]["placa"].ToString();
                txt_costo.Text = dt.Tables[0].Rows[0]["costo"].ToString();
                txt_ccosto.Text = dt.Tables[0].Rows[0]["ccentro"].ToString();
                txt_economico.Text = dt.Tables[0].Rows[0]["economico"].ToString();
                txt_residual.Text = dt.Tables[0].Rows[0]["residual"].ToString();
                txt_razonable.Text = dt.Tables[0].Rows[0]["razonable"].ToString();
                txt_deterioro.Text = dt.Tables[0].Rows[0]["deterioro"].ToString();
                txt_importe.Text = dt.Tables[0].Rows[0]["importe"].ToString();

                txt_fecha_anterior.SetValue(DateTime.Parse(dt.Tables[0].Rows[0]["fechaanterior"].ToString()).ToString("yyyy-MM-dd"));
                if (dt.Tables[0].Rows[0]["fechaanterior"].ToString() != "")
                {
                 //   txt_fecha_anterior.SetValue(DateTime.Parse(dt.Tables[0].Rows[0]["fechaanterior"].ToString()).ToString("yyyy-MM-dd"));
                    dfd_fecha.MinDate = Convert.ToDateTime(txt_fecha_anterior.Text);
                }
                else
                {
                  //  dfd_fechaAlta.SetValue(DateTime.Parse(dt.Tables[0].Rows[0]["fecha"].ToString()).ToString("yyyy-MM-dd"));
                    dfd_fecha.MinDate = Convert.ToDateTime(dfd_fechaAlta.Text);
                }



               // dfd_fecha.MinDate = Convert.ToDateTime(dfd_fechaAlta.Text);

                string codempleado = dt.Tables[0].Rows[0]["empleado"].ToString();
                hiddenoidemp.SetValue(codempleado);
                txt_resp.Text = dt.Tables[0].Rows[0]["nombre"].ToString();
               
                dt = conexion.EjecutarSelectMysql("select ca.id_Componente as codigocomponente, s_c.Componente as descripcioncomponente, " + 
                                                  " ca.Porcentaje_ci as porcentaje, ca.vida_util as vutil, ca.vr_importe_libros,ca.costo_inicial as costoinicial, " +
                                                  " ca.ajus_vr_residual as residual,ca.ajus_vr_razonable as razonable,ca.ajus_vr_deterioro as deterioro,ca.ajust_dep_acum as ajuste_dep_acum, " + 
                                                  " ca.vr_dep_acumulada as depacumulada,ca.ajust_dep_acum as ajdepacumulada, ca.id_tipo_depreciacion as codigodepreciacion, " + 
                                                  " td.Depreciacion as tipodepreciacion from activo_componente as ca inner join tb_tipo_depreciacion as td on "+
                                                  " ca.id_tipo_depreciacion=td.id_tipo INNER JOIN subclase_componente as s_c ON ca.sub_compID=s_c.id_componente " +
                                                  " where ca.id_activo=" + codigo.Text);
               // cargarhistorialDepreciacion();
                Store_componente.DataSource = dt;
                Store_componente.DataBind();

            }
            catch (Exception s)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Sigc.",
                    Message = "Error: " + s.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR,
                    Closable = false
                });
            }
            
        }
        [DirectMethod(ShowMask = true, Msg = "Cargando....", Target = MaskTarget.Page)]
        public void cargarhistorialDepreciacion()
        {
            Conexion_Mysql conexion = new Conexion_Mysql();

            DataSet dt = conexion.EjecutarSelectMysql(string.Format("SELECT s_c.Componente as descripcion, d.fecha,  dd.base, dd.depreciacion_mes as dep_mes,dd.unidad_dep as cantdep,dd.saldo_depreciacion as saldo_dep,dd.dep_acum as depacumulada, dd.id_componente,  "
                                                                        + "dd.vr_residual as vresidual,dd.ajust_dep_acum as ajdepacumulada,dd.ajust_vr_razonable as ajrazonable,dd.vr_deterioro as vrdeterioro, dd.tipo_doc FROM depreciacion d "
                                                                        + "INNER JOIN detalle_depreciacion dd ON d.id_depreciacion = dd.id_depreciacion "
                                                                        + "INNER JOIN activo_componente ca ON dd.id_componente=ca.id_Componente "
                                                                        + "INNER JOIN subclase_componente as s_c ON ca.sub_compID=s_c.id_componente "
                                                                        + "WHERE d.idactivo = '{0}' ORDER BY dd.id_componente", codigo.Text));
            GridPanel2.GetStore().DataSource = dt;
            GridPanel2.DataBind();
        }

        public DateTime consultarfechaAlta()
        {
            Conexion_Mysql conexion = new Conexion_Mysql();
            //  int codigo = Convert.ToInt16(Session["codigo"]);
            DataSet dt = conexion.EjecutarSelectMysql("SELECT Fecha_Alta as fecha, vr_Depreciacion as valor, Fecha_Udepreciacion as fechaanterior FROM activo where idactivo=" + codigo.Text);

            return DateTime.Parse(dt.Tables[0].Rows[0]["fecha"].ToString());
        }
        [DirectMethod]
        public void Depreciar(Newtonsoft.Json.Linq.JArray record, string[] values)
        {
            Boolean ing = true;
            Conexion_Mysql conexion = new Conexion_Mysql();
            List<string> sql = new List<string>();
            
            // int codigo = Convert.ToInt16(Session["codigo"]);
            string fecha = Convert.ToDateTime(values[1]).ToString("yyyy-MM-dd");
            int ajrazonable= Convert.ToInt32(txt_razonable.Text);
            int ajdepAcum= Convert.ToInt32(txt_ajusAcum.Text);
            int deterioro=Convert.ToInt32(txt_deterioro.Text);
            int depAcumActivo = Convert.ToInt32(txt_valor.Text);
            int residual = Convert.ToInt32(txt_residual.Text);
            int costoActivo = Convert.ToInt32(txt_costo.Text);
            for (int i = 0; i < record.Count; i++)
            {
                if (record[i]["vidautil"].ToString() == "0")
                {
                    ing = false;
                }
            }
            try
            {
                if (ing == false)
                {
                    throw new Exception("Favor llenar todos los campos");
                }
                else
                {

                    sql.Add(string.Format("insert into depreciacion(idactivo,responsable,fecha,FyH_Real) values('{0}','{1}','{2}',now())", codigo.Text, values[0], fecha));
                    int recuperarIDdep = Convert.ToInt16(conexion.EjecutarSelectMysql("select max(id_depreciacion)+1 as id from depreciacion").Tables[0].Rows[0]["id"].ToString());
                    double importeActivo = 0;
                    double ActivoDespAcumulada = 0;
                    double sumaImporteComponente = 0;
                    double DepmesActivo=0;
                    /* depreacion por componentes  */
                    for (int i = 0; i < record.Count; i++)
                    {
                        DataSet dt = conexion.EjecutarSelectMysql(string.Format("select vida_util_utilizado,ajus_vr_razonable as ajrazonable,ajust_dep_acum as ajdepacum,vr_dep_acumulada as depacum,ajus_vr_residual as vresidual,ajus_vr_deterioro as vdeterioro from activo_componente where id_Componente='{0}'", record[i]["codigocomponente"].ToString()));
                        double ajvrazonable = Convert.ToDouble(dt.Tables[0].Rows[0]["ajrazonable"].ToString()); 
                        double ajdepacum = Convert.ToDouble(dt.Tables[0].Rows[0]["ajdepacum"].ToString()); 
                        double depacum = Convert.ToDouble(dt.Tables[0].Rows[0]["depacum"].ToString()); //depreciacion acumulada
                        double vresidual = Convert.ToDouble(dt.Tables[0].Rows[0]["vresidual"].ToString()); 
                        double vdeterioro = Convert.ToDouble(dt.Tables[0].Rows[0]["vdeterioro"].ToString()); 
                        double costoInicial = Convert.ToDouble(record[i]["costoinicial"].ToString());  //costo inicial componente
                        double vimporte = Convert.ToDouble(record[i]["vr_importe_libros"].ToString());//importe componente
                        double vutildep =Math.Round(Convert.ToDouble(record[i]["vidautil"].ToString()),1);    //unidades depreciadas 
                        double vutil =Math.Round(Convert.ToDouble((record[i]["vutil"]).ToString()),1);
                        /* si existe un deterioro */
                        double vutilUtilizado = Convert.ToDouble((dt.Tables[0].Rows[0]["vida_util_utilizado"]).ToString());
                        double baseDepreciable = Math.Round(((costoInicial + ajvrazonable) - (vresidual + vdeterioro + ajdepacum)), 0);
                        double vr_dep_mes = Math.Round(Depreciar_mes(vutil, baseDepreciable,vutildep), 0);
                        if (vr_dep_mes <= vimporte)
                        { 
                            double nVutilUtilizado = Math.Round(vutilUtilizado+vutildep, 2);
                            DepmesActivo=vr_dep_mes+DepmesActivo;
                            double depAcumulada =vr_dep_mes+depacum;
                            double vutilremantente = vutil - vutildep;
                     
                            double importeComponente = Math.Round((costoInicial + ajvrazonable) - (ajdepacum + vdeterioro + depAcumulada + vresidual), 0);
                            ActivoDespAcumulada = depAcumulada + ActivoDespAcumulada;
                            sumaImporteComponente = sumaImporteComponente + importeComponente;

                            sql.Add(string.Format("INSERT INTO detalle_depreciacion(id_depreciacion,id_componente,depreciacion_mes,base,saldo_depreciacion,dep_acum,unidad_dep,vr_residual, " +
										    " ajust_dep_acum,ajust_vr_razonable,vr_deterioro,tipo_doc,costo_inicial) " +
											"SELECT " +
											"(SELECT max(id_depreciacion) FROM depreciacion), " +
										    "'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','DEPRECIACIÓN','{10}'",
                                            record[i]["codigocomponente"].ToString(), vr_dep_mes,baseDepreciable, importeComponente, 
                                            depAcumulada, vutildep.ToString().Replace(',', '.'), vresidual,
                                            ajdepacum, ajvrazonable, vdeterioro,costoInicial));

                            //sql.Add(string.Format("insert into detalle_depreciacion(id_depreciacion,id_componente,depreciacion_mes,base,saldo_depreciacion,dep_acum,unidad_dep,vr_residual,ajust_dep_acum,ajust_vr_razonable,vr_deterioro,tipo_doc,costo_inicial) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','DEPRECIACIÓN','{11}')",
                            //                     recuperarIDdep, record[i]["codigocomponente"].ToString(), vr_dep_mes.ToString().Replace(',', '.'), baseDepreciable.ToString().Replace(',', '.'), importeComponente.ToString().Replace(',', '.'), depAcumulada.ToString().Replace(',', '.'),
                            //                     vutildep.ToString().Replace(',', '.'), vresidual.ToString().Replace(',', '.'), ajdepacum.ToString().Replace(',', '.'), ajvrazonable.ToString().Replace(',', '.'), vdeterioro.ToString().Replace(',', '.'),costoInicial.ToString()));
                            sql.Add(string.Format("update activo_componente set vr_dep_acumulada= '{0}',vr_importe_libros= '{1}', vida_util_utilizado='{2}', vida_util='{3}',vida_util_remanente='{4}',base_deprec='{5}', vr_dep_mes='{6}' where id_Componente = '{7}'", depAcumulada.ToString().Replace(',', '.'), importeComponente.ToString().Replace(',', '.'), nVutilUtilizado.ToString().Replace(',', '.'), vutil.ToString().Replace(',', '.'), vutilremantente.ToString().Replace(',', '.'), baseDepreciable.ToString().Replace(',', '.'), vr_dep_mes.ToString().Replace(',', '.'), record[i]["codigocomponente"].ToString()));
                        }
                        else
                        {
                            X.Msg.Info(new InfoPanel
                            { 
                                Title = "Server time",
                                Icon = Icon.Exclamation,
                                Html = "LA DEPRECIACION MES ES INFERIOR AL SALDO DEPRECIABLE"
                            }).Show();
                            break;
                        }
                    }
                    importeActivo = (costoActivo + ajrazonable) - (ajdepAcum + deterioro + ActivoDespAcumulada);
                    sql.Add(string.Format("update depreciacion set vr_depreciacion='{0}',saldo_dep='{1}',dep_mes='{2}',tipo_doc_activo='DEPRECIACIÓN'  where id_depreciacion={3}", ActivoDespAcumulada.ToString().Replace(',', '.'), importeActivo.ToString().Replace(',', '.'), DepmesActivo, recuperarIDdep));
                    sql.Add(string.Format("update activo set Fecha_Udepreciacion='{0}',vr_Depreciacion = '{1}',Importe_libros = '{2}' where idactivo= '{3}'", fecha, ActivoDespAcumulada.ToString().Replace(',', '.'), importeActivo.ToString().Replace(',', '.'), codigo.Text));
                                       
                    conexion.EjecutarTransaccion(sql);
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Sigc.",
                        Message = "Depreciacion exitosa",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.INFO,
                        Closable = true
                    });

                  cargarAjusteDepreciacion();
                  cargarhistorialDepreciacion();
                  dfd_fecha.Reset();



                }
            }
            catch (Exception s)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Sigc.",
                    Message = "Error: " + s.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR,
                    Closable = false
                });
            }
        }
        public double calcularImporteComp(double importeComponente, double depAcumuladaComponente)
        {
            return (importeComponente - depAcumuladaComponente);
        }

        /*  la base depreciable / vida util */
        public double Depreciar_mes(double vidautil, double baseComponente, double vidautilizada)
        {
            double vr_dep = ( baseComponente/vidautil)*vidautilizada;
            return (vr_dep);
        }
        public double SacarBaseComponente(double importe, double vidautil)
        {
            return (importe / vidautil);
        }
       // [DirectMethod(ShowMask=true, Msg="Cargndo", Target=MaskTarget.CustomTarget, CustomTarget="App.GridPanel1" )]
        [DirectMethod(ShowMask = true, Msg = "Cargando", Target = MaskTarget.Page)]
        public void restarfechas(Newtonsoft.Json.Linq.JArray record)
        {
            string fanteriorD = Convert.ToDateTime(txt_fecha_anterior.Text).ToString("yyyy-MM-dd");
            string factualD = Convert.ToDateTime(dfd_fecha.Text).ToString("yyyy-MM-dd");

            for (int i = 0; i < record.Count; i++)
            {
                if (record[i]["tipodepreciacion"].ToString().Equals("LINEA RECTA"))
                {
                      
                    record[i]["vidautil"] = Math.Round(Convert.ToDouble(CalcularDias(fanteriorD, factualD)) / 30, 1);
                }
            }
            
            Store_componente.DataSource = record.ToList();
            Store_componente.DataBind();

        }

        //0corregir fecha comercial de 30+1 dias
        public double CalcularDias(string oldDate, string newDate)
        {
         
            DateTime fecha1 = Convert.ToDateTime(oldDate);
            DateTime fecha2 = Convert.ToDateTime(newDate);
         
            int diastrancurrido=0;
            int diasmes = DateTime.DaysInMonth(Convert.ToDateTime(fecha1).Year, Convert.ToDateTime(fecha1).Month); //ultimo dia del mes
            if (fecha1.Year == fecha2.Year && fecha1.Month == fecha2.Month) {// dos fechas pertenencen al mismo mes
                    if (fecha1.Day < 30 && diasmes ==fecha2.Day) { //fecha dos es el ultimo dia del mes entonces  tome el mes de 30
                        diastrancurrido = 30 - fecha1.Day;
                    }
                    else
                    {    // sino solo reste
                        diastrancurrido = fecha2.Day - fecha1.Day;
                    }
             }
             else {
                // fecha dos es superior al mes de la fecha 1
                 int diasmesinicio = 0;
                    int mesestrancurridos =Convert.ToInt16(Math.Abs((fecha2.Month-fecha1.Month)+12*(fecha2.Year-fecha1.Year)))-1;
                    if (fecha2.Day >= 30 || fecha2.Day == DateTime.DaysInMonth(Convert.ToDateTime(fecha2).Year, Convert.ToDateTime(fecha2).Month))
                    {  // fecha 2 es mayor al dia 30 o igual al ultimo dia del mes
                        if (fecha1.Day >= 30 || fecha1.Day == DateTime.DaysInMonth(Convert.ToDateTime(fecha1).Year, Convert.ToDateTime(fecha1).Month))
                        { // si fecha uno es igual al ultimo dia del mes o superior al 30
                            diasmesinicio = 0;
                        }
                        else
                        {
                            diasmesinicio= 30 - fecha1.Day;
                            
                        }
                        diastrancurrido = 30 * mesestrancurridos + diasmesinicio + 30;
                    }
                    else
                    {

                        diastrancurrido = 30 * mesestrancurridos + diasmesinicio + fecha2.Day;
                    }   
            }

            return diastrancurrido;
        }
    }
}