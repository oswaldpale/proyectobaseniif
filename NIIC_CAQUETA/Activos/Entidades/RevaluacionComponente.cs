using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Activos.Clases
{
    
        public class RevaluacionComponente
        {
            public string idactivo { get; set; }
            public string id_componente { get; set; }
            public float Porcentaje_ci { get; set; }
            public float ajust_vr_razonable { get; set; }
            public float costo_inicial { get; set; }
            public float vr_dep_acumulada { get; set; }
            public float vr_residual { get; set; }
            public float vr_importe_depreciable { get; set; }
            public float vr_importe_libros { get; set; }
            public float base_deprec { get; set; }
            public float vr_dep_mes { get; set; }
        }
    
}
