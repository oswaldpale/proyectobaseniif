using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Activos.Clases
{
    public class CambiarMetodo
    {
        public string IdActivo { get; set; }
        public string Activo { get; set; }
        public string IdComponente { get; set; }
        public string nombre_componente { get; set; }
        public string Porcentaje_ci { get; set; }
        public float vida_util { get; set; }
        public float vida_util_utilizado { get; set; }
        public float vida_util_remanente { get; set; }

        public string IDdepreciacion { get; set; }
        public string Depreciacion { get; set; }
        public string IdsubComponente { get; set; }


        public float AjusteDepreciacionAcumulada { get; set; }
        public float AjusteResidual { get; set; }
        public float AjusteDeterioro { get; set; }
        public float AjusteRazonable { get; set; }
        public float BaseDepreciable { get; set; }
        public float DepreciacionMes { get; set; }
        public float DepreciacionAcumulada { get; set; }
        public float ImporteDepreciable { get; set; }
        public float ImporteLibros { get; set; }

    }
}