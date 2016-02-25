using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Activos.Clases
{
    public class ReexpresarRemanente
    {
       
        public string IdActivo { get; set; }
        public string Activo { get; set; }
        public string IdComponente { get; set; }
        public string nombre_componente { get; set; }
        public string vida_util { get; set; }
        public string vida_util_utilizado { get; set; }
        public float vida_util_remanente { get; set; }

        public float reexpresionVidaUtil { get; set; }

        public string Depreciacion { get; set; }

        public string Porcentaje_ci { get; set; }

        public string IdsubComponente { get; set; }
    }
}