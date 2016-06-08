using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Activos.Clases
{
    public class ActivoComponente
    {
        public int id_activo { get; set; }

        public string fechaRevision { get; set; }

        public string id_tipodepreciacion { get; set; }

        public string id_componente { get; set; }
        public string nombre_componente { get; set; }
        public string sub_compID { get; set; }
        public  double vida_util { get; set; }
        public double Porcentaje_ci { get; set; }
       
        public double ajust_vr_residual { get; set; }
        public double ajust_vr_razonable { get; set; }
        public double ajust_vr_deterioro { get; set; }
        public double costo_inicial { get; set; }
        public double vr_dep_acumulada_old { get; set; }
        public double vr_dep_acumulada { get; set; }
        public double vr_importe_libros { get; set; }
        public double base_deprec_old { get; set; }
        public double base_deprec { get; set; }
        public string nombre_depreciacion { get; set; }
        public string Depreciable { get; set; }
        public string  vu_indefinida { get; set; }
        public double vida_util_remanente { get; set; }
        public double vida_util_utilizado { get; set; }
        public string idtiponorma { get; set; }

        public string NombreNorma { get; set; }
        public string porc_revaluacion { get; set; }
        public double vr_dep_mes { get; set; }
        public double vida_util_temp { get; set; }
        public double ajusteVidaUtil { get; set; }

        public double unidad_dep { get; set; }

        public int tipodepreciacion { get; set; }

        public double cantidadDias { get; set; }

        public double importeDepreciable { get; set; }

    }
}