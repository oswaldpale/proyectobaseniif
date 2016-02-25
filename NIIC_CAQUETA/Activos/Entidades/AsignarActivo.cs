using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Activos.Clases
{
    public class AsignarActivo
    {
        public string idactivo { get; set; }
        public string nombreActivo { get; set; }

        public int codUge { get; set; }
        public string codCentroCosto { get; set; }
       // public int idcentro_costo
        public string codCentroEconomico { get; set; }
        public int idubicacion { get; set; }
        public int idempleado { get; set; }
        public int idmotivo_asignacion { get; set; }

        public int idfuncion { get; set; }
       
        public string fecha { get; set; }
        public string observacion { get; set; }

        public string placa { get; set; }

        public int idcentroCosto { get; set; }

        public int idcentroEconomico { get; set; }

        public int iduge { get; set; }
    }
}