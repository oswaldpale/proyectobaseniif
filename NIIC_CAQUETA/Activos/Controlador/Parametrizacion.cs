using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Activos.Clases
{
    public class Parametrizacion
    {
        Conexion_Mysql conexion = new Conexion_Mysql();
        public int AutoGeneradorID(int IdDocumento)
        {
            string sql = "SELECT "
                         + "    r.Numero "
                         + "FROM "
                         + "    registro_control r "
                         + "WHERE "
                         + "    r.Codigo = '" + IdDocumento + "'";
            return Convert.ToInt32(conexion.EjecutarSelectMysql(sql).Tables[0].Rows[0]["Numero"].ToString());

        }
        
        public int nextPrimaryKey(string TablaID, string Tabla)
        {
            string sql = " SELECT"
                          + "    COALESCE(MAX(E." + TablaID + "), 0) + 1 AS PK"
                          + " FROM "
                          + Tabla + " E";
            return Convert.ToInt32(conexion.EjecutarSelectMysql(sql).Tables[0].Rows[0]["PK"].ToString());
        }
        public string NextPrimaryKeyTransaccion(string TablaId, string Tabla)
        {
            string sql = "(SELECT "
                         + "COALESCE(MAX(e." + TablaId + "), 0) + 1 AS PK "
                         + "  FROM "
                         + " " + Tabla + " e)";

            return sql;
        }
        public DataSet ConsultarActivosEnProduccion() {
             string sql = "SELECT "
                        + "    idActivo AS codigo, "
                        + "    Nombre   AS nombre, "
                        + "    placa, "
                        + "    vr_deterioro AS deterioro, "
                        + "    vr_residual as residual, "
                        + "    vr_razonable as revaluacion,"
                        + "    BaseDepreciable, "
                        + "    Importe_libros "
                        + "FROM "
                        + "    activo "
                        + "WHERE "
                        + "    Fecha_Alta IS NOT NULL "
                        + "AND id_empleado IS NOT NULL "
                        + "AND Fecha_Baja IS NULL "
                        + "AND Estado ='A'";
             return conexion.EjecutarSelectMysql(sql);
        }

        public DataSet ConsultarClases() {
            string sql = "SELECT "
                        + "    id_clase, "
                        + "    Clase "
                        + "FROM "
                        + "    tb_clase "
                        + "where Estado ='A'";
            return conexion.EjecutarSelectMysql(sql);
        }
        #region Gestionar Control Documento Depreciacion

        public string ConsultarFechaDepreciacionMin() {
            string sql = "SELECT "
                       + "    Date_format(MIN(a.Fecha_Udepreciacion),'%d-%m-%Y') AS FechaUltimoDoc "
                       + "FROM "
                       + "    activo a "
                       + "WHERE "
                       + "    a.Estado='A' "
                       + "AND a.id_empleado IS NOT NULL "
                       + "AND a.Fecha_Udepreciacion IS NOT NULL "
                       + "AND a.Fecha_Baja IS NULL";
          return conexion.EjecutarSelectMysql(sql).Tables[0].Rows[0]["FechaUltimoDoc"].ToString();
                                                 
                                                  
        }

        public bool InsertarDatosPrincipales() {
            List<string> sentencias = new List<string>();
            string sql =  "INSERT "
                            + "INTO "
                            + "    depreciacion "
                            + "    ( "
                            + "        id_depreciacion, "
                            + "        IDcontrol, "
                            + "        responsable, "
                            + "        fecha, "
                            + "        FyH_Real, "
                            + "        login, "
                            + "        vr_depreciacion, "
                            + "        idactivo, "
                            + "        ImporteLibros, "
                            + "        vr_residual, "
                            + "        vr_deterioro, "
                            + "        vr_razonable, "
                            + "        baseDepreciable, "
                            + "        dep_mes "
                            + "    ) "
                            + "    VALUES "
                            + "    ( "
                            + "        0, "
                            + "        '', "
                            + "        '', "
                            + "        '', "
                            + "        '', "
                            + "        '', "
                            + "        0, "
                            + "        0, "
                            + "        0, "
                            + "        0, "
                            + "        0, "
                            + "        0, "
                            + "        0, "
                            + "        0 "
                            + "    )";
            sentencias.Add(sql);
            return conexion.EjecutarTransaccion(sentencias);
        }
        public int ConsultarExistenciaDepreciacion() {
            string sql = "SELECT Count(*) AS DepCantidad  FROM depreciacion";
            return  Convert.ToInt32(conexion.EjecutarSelectMysql(sql).Tables[0].Rows[0]["DepCantidad"].ToString());
        }
        /// <summary>
        /// Inserta los datos contables de los activos en depreciacion de las entradas realizas
        /// </summary>
        /// <param name="FechaPrimeraDepreciacion"></param>
        /// <returns></returns>
        public List<string> InsertDepreciacionInicial(string FechaPrimeraDepreciacion)
        {
            List<string> sentencias = new List<string>();
            string NControl = "DP000";

            string sql = InsertarDocumentoDepreciacion(NControl,"Entrada",FechaPrimeraDepreciacion);
            sentencias.Add(sql);
                   sql  = "INSERT "
                        + "INTO "
                        + "    depreciacion "
                        + "    ( "
                        + "        idactivo, "
                        + "        IDcontrol, "
                        + "        fecha, "
                        + "        vr_residual, "
                        + "        vr_deterioro, "
                        + "        vr_razonable, "
                        + "        dep_mes, "
                        + "        vr_depreciacion, "
                        + "        baseDepreciable, "
                        + "        ImporteLibros, "
                        + "        responsable, "
                        + "        IdGrupoAsignacionCentroCosto"
                        + "    ) "
                        + "SELECT "
                        + "    a.idActivo, "
                        + "    '" + NControl + "' AS IDcontrol, "
                        + "    a.Fecha_Udepreciacion, "
                        + "    a.vr_residual, "
                        + "    a.vr_deterioro, "
                        + "    a.vr_razonable, "
                        + "    a.vr_dep_mes, "
                        + "    a.vr_Depreciacion, "
                        + "    a.BaseDepreciable, "
                        + "    a.Importe_libros, "
                        + "    a.id_empleado, "
                        + "   (SELECT g.IdgrupoAsignacionCentro from activos_fijos.asignacionactivo g WHERE g.idactivo = a.idActivo AND g.estado='A') "
                        + "FROM "
                        + "    activo a "
                        + "WHERE a.Fecha_Alta IS NOT NULL AND a.id_empleado IS NOT NULL  AND a.Fecha_Baja IS NULL "
                        + " AND a.Estado ='A' "
                        + " AND   a.Fecha_Udepreciacion < '" +  FechaPrimeraDepreciacion + "'";
            sentencias.Add(sql);
                    sql = "INSERT "
                        + "INTO "
                        + "    detalle_depreciacion "
                        + "    ( "
                        + "        id_depreciacion, "
                        + "        Costo_Inicial, "
                        + "        id_componente, "
                        + "        Fecha, "
                        + "        unidad_dep, "
                        + "        vr_residual, "
                        + "        vr_deterioro, "
                        + "        ajust_vr_razonable, "
                        + "        depreciacion_mes, "
                        + "        dep_acum, "
                        + "        baseDepreciable, "
                        + "        ImporteLibros, "
                        + "        vidaUtil, "
                        + "        vida_utilizada, "
                        + "        vida_remanente, "
                        + "        idactivo, "
                        + "        idnorma, "
                        + "        idtipodepreciacion, "
                        + "        reexpresionVidautil "
                        + "    ) "
                        + "SELECT "
                        + "    d.id_depreciacion, "
                        + "    ac.costo_inicial, "
                        + "    ac.id_Componente, "
                        + "    d.fecha, "
                        + "    '0' AS unidadMes, "
                        + "    ac.ajus_vr_residual, "
                        + "    ac.ajus_vr_deterioro, "
                        + "    ac.ajus_vr_razonable, "
                        + "    ac.vr_dep_mes, "
                        + "    ac.vr_dep_acumulada, "
                        + "    ac.base_deprec, "
                        + "    ac.vr_importe_libros, "
                        + "    ac.vida_util, "
                        + "    ac.vida_util_utilizado, "
                        + "    ac.vida_util_remanente, "
                        + "    ac.id_activo, "
                        + "    ac.idtiponorma, "
                        + "    ac.id_tipo_depreciacion, "
                        + "    ac.reexpresionVidaUtil "
                        + "FROM "
                        + "    activo_componente ac "
                        + "INNER JOIN depreciacion d "
                        + "ON "
                        + "    ac.id_activo = d.idactivo "
                        + "WHERE "
                        + "    d.IDcontrol= '" + NControl + "'";
               sentencias.Add(sql);
               return sentencias;
        }

        public string InsertarDocumentoDepreciacion(string Rcontrol,string Documento,string Fecha)
        {
            string sql = "INSERT "
                        + "INTO "
                        + "    registro_control_depreciacion "
                        + "    ( "
                        + "        Ncontrol, "
                        + "        Descripcion, "
                        + "        Fecha, "
                        + "        Estado, "
                        + "        Concepto "
                        + "    ) "
                        + "    VALUES "
                        + "    ( "
                        + "        '" + Rcontrol + "', "
                        + "        '" + Documento + "', "
                        + "        '" + Fecha + "', "
                        + "        'Activo', "
                        + "        '' "
                        + "    );";
            return sql;
        }
        public DataSet ConsultarUltimaFechaDepreciacion(){
            string sql = "SELECT "
                        + "    d.Ncontrol, "
                        + "    Date_format(MAX(d.Fecha),'%d-%m-%Y') FechaDepreciacion "
                        + "FROM "
                        + "    registro_control_depreciacion d "
                        + "WHERE "
                        + "    d.Descripcion = 'Depreciacion' "
                        + "AND d.estado = 'Activo' "
                        + "ORDER BY "
                        + "    d.Fecha DESC LIMIT  0,1";
            
        return conexion.EjecutarSelectMysql(sql); 
        }
        public DataSet ConsultarDocumentosAfectadosAnulacion(string fecha)
        {
            string sql = "SELECT "
                       + " Ncontrol, "
                       + "    Descripcion, "
                       + "    Fecha, "
                       + "FROM "
                       + "    registro_control_depreciacion d "
                       + "WHERE "
                       + "d.Fecha > '" + fecha + "' "
                       + "AND d.Estado='ACTIVO'";

            return conexion.EjecutarSelectMysql(sql); 
        }

        public DataSet ConsultarDepreciacionAnterior() { 
            string sql = ""
            + "SELECT "
            + "    d.Ncontrol, "
            + "    Date_format(d.Fecha,'%d-%m-%Y') as Fecha   "
            + "FROM "
            + "    registro_control_depreciacion d "
            + "WHERE "
            + "    d.Descripcion = 'Depreciacion' "
            + "AND d.estado = 'Activo' "
            + "ORDER BY "
            + "    d.Fecha DESC LIMIT 1,1";
            return conexion.EjecutarSelectMysql(sql);
        }

        /// <summary>
        /// Restaurar los datos de los activos al mes anterior
        ///Anula los cambios que se hallan desarrollado durante el mes anulado:
        ///  Anulaciones realizadas:
        ///  - Revalucion.
        ///  - Deterioro.
        ///  - Residual.
        ///  - Cambio Metodo.
        ///  - Bajas de Activos(Bajas y Ventas).
        ///  - Estimacion Vida Util.
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="Ncontrol"></param>
        /// <returns></returns>
        public bool AnularDepreciacionporMes(string fecha, string Ncontrol,string ConceptoAnulacion,string Usuario) {
            List<string> setencias = new List<string>();
         
            String sql = "UPDATE "
                        + "    registro_control_depreciacion r, "
                        + "    ( "
                        + "        SELECT "
                        + "            Ncontrol "
                        + "        FROM "
                        + "            registro_control_depreciacion d "
                        + "        WHERE "
                        + "            d.Fecha > '" + fecha + "' "
                        + "        AND d.Estado='ACTIVO' "
                        + "    ) "
                        + "    anu "
                        + "SET "
                        + "    r.Estado = 'Anulado', "
                        + "    r.Concepto = '" + ConceptoAnulacion + "', "
                        + "    r.FyH_Real = now(), "
                        + "    r.login = '" + Usuario + "' "
                        + "WHERE "
                        + "    r.Ncontrol = anu.Ncontrol";
            setencias.Add(sql);
                      sql = "UPDATE "
                          + "    activo act, "
                          + "    ( "
                          + "        SELECT "
                          + "            d.idactivo, "
                          + "            d.fecha, "
                          + "            d.vr_residual, "
                          + "            d.vr_deterioro, "
                          + "            d.vr_razonable, "
                          + "            d.dep_mes, "
                          + "            d.vr_depreciacion, "
                          + "            d.baseDepreciable, "
                          + "            d.ImporteLibros "
                          + "        FROM "
                          + "            depreciacion d "
                          + "        WHERE "
                          + "            d.IDcontrol = '"+Ncontrol+"' "
                          + "    ) "
                          + "    ant "
                          + "SET "
                          + "    act.Fecha_Udepreciacion = ant.fecha, "
                          + "    act.Importe_libros = ant.ImporteLibros, "
                          + "    act.vr_Depreciacion = ant.vr_depreciacion, "
                          + "    act.vr_dep_mes = ant.dep_mes, "
                          + "    act.vr_deterioro = ant.vr_deterioro, "
                          + "    act.vr_residual = ant.vr_residual, "
                          + "    act.vr_razonable = ant.vr_razonable, "
                          + "    act.BaseDepreciable = ant.baseDepreciable "
                          + "WHERE "
                          + "    act.idActivo = ant.idactivo";
                          
                         setencias.Add(sql);

                 sql = "UPDATE "
                          + "    activo_componente ac, "
                          + "    ( "
                          + "        SELECT "
                          + "            dd.idactivo, "
                          + "            dd.id_depreciacion, "
                          + "            dd.Costo_Inicial, "
                          + "            dd.id_componente, "
                          + "            dd.Fecha, "
                          + "            dd.vr_residual, "
                          + "            dd.ajust_vr_razonable, "
                          + "            dd.vr_deterioro, "
                          + "            dd.baseDepreciable, "
                          + "            dd.depreciacion_mes, "
                          + "            dd.vida_remanente, "
                          + "            dd.vida_utilizada, "
                          + "            dd.dep_acum, "
                          + "            dd.ImporteLibros, "
                          + "            dd.idtipodepreciacion, "
                          + "            dd.vidaUtil "
                          + "        FROM "
                          + "            detalle_depreciacion dd "
                          + "        INNER JOIN depreciacion d "
                          + "        ON "
                          + "            dd.id_depreciacion = d.id_depreciacion "
                          + "        WHERE "
                          + "            d.IDcontrol='" + Ncontrol + "' "
                          + "    ) "
                          + "    comp "
                          + "SET "
                          + "    ac.vida_util = comp.vidaUtil, "
                          + "    ac.ajus_vr_residual = comp.vr_residual, "
                          + "    ac.ajus_vr_razonable = comp.ajust_vr_razonable, "
                          + "    ac.ajus_vr_deterioro = comp.vr_deterioro, "
                          + "    ac.vr_dep_acumulada = comp.dep_acum, "
                          + "    ac.vr_importe_libros = comp.ImporteLibros, "
                          + "    ac.base_deprec = comp.baseDepreciable, "
                          + "    ac.vida_util_utilizado = comp.vida_utilizada, "
                          + "    ac.vida_util_remanente = comp.vida_remanente, "
                          + "    ac.vr_dep_mes = comp.depreciacion_mes "
                          + "WHERE "
                          + "    ac.id_Componente = comp.id_componente";
                          setencias.Add(sql);
            return conexion.EjecutarTransaccion(setencias);
        }
        #endregion

        #region Gestionar Alta-Asignaciones
        public List<String> FormatoCentroCostoAsignaciones(DataSet dt)
        {
            string concVaCcosto = "";
            string ConcatenadorCcosto = "";
            List<String> Formato = new List<String>();
            foreach (DataRow record in dt.Tables[0].Rows)
            {
                string ForCcosto = "[{0},{1}%] ";
                string FormatoCentroCosto = "({0})- {1}";
                ConcatenadorCcosto = ConcatenadorCcosto + string.Format(FormatoCentroCosto, record["codigo"].ToString(),
                                                                       record["centroCosto"].ToString());

                concVaCcosto = concVaCcosto + string.Format(ForCcosto, record["codigo"].ToString(),
                                                                       record["porcentaje"].ToString());
            }
            Formato.Add(concVaCcosto);
            Formato.Add(ConcatenadorCcosto);

            return Formato;
        }
        public DataSet ConsultarCentroCostoAsignado(string RControl)
        {
            string sql = "SELECT "
                            + "    a.id_centro AS idcentro, "
                            + "    a.porcentaje, "
                            + "    c.codigo, "
                            + "    c.Centro_costo AS centroCosto "
                            + "FROM "
                            + "    detalle_centrocosto_asignacionactivo a "
                            + "INNER JOIN grupo_asignacionescentrocosto g "
                            + "ON "
                            + "    a.IdgrupoAsignacionCentro = g.IdgrupoAsignacionCentro "
                            + "INNER JOIN activos_fijos.tb_centro_costo c "
                            + "ON "
                            + "    a.id_centro = c.id_centro "
                            + "WHERE "
                            + "    g.RControl= '" + RControl + "'";

            return conexion.EjecutarSelectMysql(sql);
        }

        public DataSet ConsultarGrupoAsignaciones(string nomenclatura)
        {

            string sql = "SELECT "
                         + "    g.IdgrupoAsignacionCentro AS Codigo,"
                         + "    Date_format(g.fecha, '%d-%m-%Y')                  AS FechaRegistro, "
                         + "    CONCAT_WS(' ',e.Nombres, e.Apellido1,e.Apellido2) AS Nombre, "
                         + "    g.Rcontrol, "
                         + "    g.observacion AS Observacion, "
                         + "    g.idMotivo as Idmotivo, "
                         + "    g.IdUge, "
                         + "    g.idcentroeconomico, "
                         + "    g.Idempleado "
                         + "FROM "
                         + "    grupo_asignacionescentrocosto g "
                         + "INNER JOIN activos_fijos.empleado e "
                         + "ON "
                         + "    g.Idempleado = e.id_empleado "
                         + "WHERE "
                         + "    g.Rcontrol LIKE '%" + nomenclatura + "%'";

            return conexion.EjecutarSelectMysql(sql);
        }
        public DataSet ConsultarActivosAsignadoGrupo(string RControl)
        {

            string sql = " SELECT  a.idactivo, "
                            + "    a.idfuncion, "
                            + "    a.iddependencia, "
                            + "    ac.Nombre AS activo, "
                            + "    ac.placa, "
                            + "    ac.id_subclase AS idsubclase, "
                            + "    ac.NoCompra    AS ncompra, "
                            + "    a.IdFactura    AS nfactura, "
                            + "    ac.id_subclase, "
                            + "    f.Funcion AS funcion, "
                            + "    d.Nombre  AS depedencia "
                            + "FROM "
                            + "    activos_fijos.asignacionactivo a "
                            + "INNER JOIN grupo_asignacionescentrocosto g "
                            + "ON "
                            + "    a.IdgrupoAsignacionCentro = g.IdgrupoAsignacionCentro "
                            + "INNER JOIN activos_fijos.activo ac "
                            + "ON "
                            + "    a.idactivo = ac.idActivo "
                            + "INNER JOIN activos_fijos.tb_funcion f "
                            + "ON "
                            + "    a.idfuncion = f.id_funcion "
                            + "INNER JOIN activos_fijos.dependencia d "
                            + "ON "
                            + "    a.iddependencia = d.Codigo "
                            + "WHERE "
                            + "    g.RControl= '" + RControl + "'";
            return conexion.EjecutarSelectMysql(sql);
        }

        public string RestaurarAsignaciones(string Rcontrol)
        {
            string sql = ""
                    + "UPDATE "
                    + "    activo a, "
                    + "        (SELECT "
                    + "            a.idactivo "
                    + "        FROM "
                    + "            asignacionactivo a "
                    + "        INNER JOIN grupo_asignacionescentrocosto gc "
                    + "        ON "
                    + "            a.IdgrupoAsignacionCentro = gc.IdgrupoAsignacionCentro "
                    + "        WHERE "
                    + "            gc.RControl = '" + Rcontrol + "' "
                    + "    )  r "
                    + " "
                    + "SET "
                    + "    a.id_empleado= NULL, "
                    + "    a.id_dependencia=NULL, "
                    + "    a.id_funcion=NULL "
                    + "   WHERE a.idActivo = r.idactivo";
            return sql;
        }
        public string RestaurarAsignacionesAlta(string Rcontrol)
        {
            string sql = ""
                    + "UPDATE "
                    + "    activo a, "
                    + "        (SELECT "
                    + "            a.idactivo "
                    + "        FROM "
                    + "            asignacionactivo a "
                    + "        INNER JOIN grupo_asignacionescentrocosto gc "
                    + "        ON "
                    + "            a.IdgrupoAsignacionCentro = gc.IdgrupoAsignacionCentro "
                    + "        WHERE "
                    + "            gc.RControl = '" + Rcontrol + "' "
                    + "    )  r "
                    + " "
                    + "SET "
                    + "    a.id_empleado= NULL, "
                    + "    a.Fecha_Alta= NULL, "
                    + "    a.Fecha_Udepreciacion= NULL, "
                    + "    a.id_dependencia=NULL, "
                    + "    a.id_funcion=NULL "
                    + "   WHERE a.idActivo = r.idactivo";
            return sql;
        }
        public string QuitarAsignacionesRealizadas(string Rcontrol)
        {
            string sql = "DELETE "
                        + "FROM "
                        + "    activos_fijos.asignacionactivo "
                        + "WHERE "
                        + "    IdgrupoAsignacionCentro = "
                        + "    ( "
                        + "        SELECT "
                        + "            g.IdgrupoAsignacionCentro "
                        + "        FROM "
                        + "            activos_fijos.grupo_asignacionescentrocosto g "
                        + "        WHERE "
                        + "             g.RControl='" + Rcontrol + "'"
                        + "    ) ";
            return sql;
        }
        public string QuitarCentroCosto(string Rcontrol)
        {
            string sql = "DELETE "
                    + "FROM "
                    + "    activos_fijos.detalle_centrocosto_asignacionactivo "
                    + "WHERE "
                    + "    IdgrupoAsignacionCentro = "
                    + "    ( "
                    + "        SELECT "
                    + "            g.IdgrupoAsignacionCentro "
                    + "        FROM "
                    + "            activos_fijos.grupo_asignacionescentrocosto g "
                    + "        WHERE "
                    + "             g.RControl='" + Rcontrol + "' "
                    + "    )";
            return sql;
        }
        public string ActualizarGrupoAsignacioncCosto(string empleado, string uge, string motivo, string observacion, string fecha, string centroeconomico, string Rcontrol)
        {
            string sql = "UPDATE "
                    + "    grupo_asignacionescentrocosto "
                    + "SET "
                    + "    Idempleado = '" + empleado + "', "
                    + "    IdUge = '" + uge + "', "
                    + "    idMotivo = '" + motivo + "', "
                    + "    Observacion = '" + observacion + "', "
                    + "    fecha = '" + fecha + "', "
                    + "    idcentroeconomico = '" + centroeconomico + "' "
                    + "WHERE "
                    + "    RControl = '" + Rcontrol + "'";
            return sql;
        }

        #endregion
        #region DETERIORO
        public DataSet ConsultarDatosActivoDeterioro (string placa) {
            string sql = "SELECT "
                    + "    Nombre, "
                    + "    idActivo, "
                    + "    vr_deterioro, "
                    + "    BaseDepreciable, "
                    + "    Importe_libros "
                    + "FROM "
                    + "    ACTIVO "
                    + "WHERE "
                    + "    placa ='" + placa + "' "
                    + "AND Estado ='A'";
          return conexion.EjecutarSelectMysql(sql);
        }
        public DataSet ConsultarPorcentajeParticipacionComponente(string idactivo) {
             string sql = "SELECT id_Componente as id, "
                    + " Porcentaje_ci as porc FROM activo_componente ca "
                    + " INNER JOIN activo a on ca.id_activo=a.idActivo "
                    + " WHERE idtiponorma=1 AND a.idActivo=" + idactivo;
             return conexion.EjecutarSelectMysql(sql);
        }
        public DataSet ConsultarNoIngresoDeterioro() {
            string sql = "SELECT DISTINCT "
                        + "    c.Ncontrol, "
                        + "    c.Fecha, "
                        + "    c.TipoDeterioro, "
                        + "    c.MotivoDeterioro, "
                        + "    c.Observacion "
                        + "FROM "
                        + "    registro_control_depreciacion c "
                        + "INNER JOIN deterioro d "
                        + "ON "
                        + "    c.Ncontrol = d.Ncontrol "
                        + "WHERE "
                        + "    c.Estado='Activo'";
            return conexion.EjecutarSelectMysql(sql);

        }
        public DataSet ConsultarActivosDeterioro(string Idcontrol) {
            string sql = "SELECT "
                       + "    d.idactivo, "
                       + "    d.Deterioro, "
                       + "    d.BaseDepreciable, "
                       + "    d.ImporteLibros "
                       + "FROM "
                       + "    activos_fijos.deterioro d "
                       + "WHERE "
                       + "    d.Ncontrol='" + Idcontrol + "'";
            return conexion.EjecutarSelectMysql(sql);
        }

        public string InsertarDocumentoDeterioro(string Rcontrol, string Documento, string Fecha,string tipo,string motivo,string observacion)
        {
            string sql = "INSERT "
                        + "INTO "
                        + "    registro_control_depreciacion "
                        + "    ( "
                        + "        Ncontrol, "
                        + "        Descripcion, "
                        + "        Fecha, "
                        + "        Estado, "
                        + "        Concepto, "
                        + "        TipoDeterioro, "
                        + "        MotivoDeterioro, "
                        + "        Observacion "
                        + "    ) "
                        + "    VALUES "
                        + "    ( "
                        + "        '" + Rcontrol + "', "
                        + "        '" + Documento + "', "
                        + "        '" + Fecha + "', "
                        + "        'Activo', "
                        + "        '', "
                        + "'"+ tipo + "', "
                        + "'" + motivo + "', "
                        + "'" + observacion + "'"
                        + "    );";
            return sql;
        }
        #endregion
        #region REVALUACION
        public DataSet ConsultarDatosActivoRevaluacion(string placa)
        {
            string sql = "SELECT "
                    + "    Nombre, "
                    + "    idActivo, "
                    + "    vr_razonable, "
                    + "    BaseDepreciable, "
                    + "    Importe_libros "
                    + "FROM "
                    + "    ACTIVO "
                    + "WHERE "
                    + "    placa ='" + placa + "' "
                    + "AND Estado ='A'";
            return conexion.EjecutarSelectMysql(sql);
        }
        #endregion
        #region VALOR RESIDUAL
        public DataSet ConsultarDatosActivoValorResidual(string placa) {
            string sql = "SELECT "
                    + "    Nombre, "
                    + "    a.idActivo, "
                    + "    a.placa, "
                    + "    a.vr_residual, "
                    + "    a.BaseDepreciable, "
                    + "    a.Importe_libros "
                    + "FROM "
                    + "    activo a "
                    + "WHERE a.placa='" + placa + "'";
            return conexion.EjecutarSelectMysql(sql);
        }
        public string InsertarDocumentoResidual(string Rcontrol, string Documento, string Fecha,string observacion)
        {
            string sql = "INSERT "
                        + "INTO "
                        + "    registro_control_depreciacion "
                        + "    ( "
                        + "        Ncontrol, "
                        + "        Descripcion, "
                        + "        Fecha, "
                        + "        Estado, "
                        + "        Concepto, "
                        + "        Observacion "
                        + "    ) "
                        + "    VALUES "
                        + "    ( "
                        + "        '" + Rcontrol + "', "
                        + "        '" + Documento + "', "
                        + "        '" + Fecha + "', "
                        + "        'Activo', "
                        + "        '', "
                        + "        '" + observacion + "' "
                        + "    );";
            return sql;
        }

        #endregion

    }
}