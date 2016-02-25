using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Activos.Clases
{
    public class AtributosDepreciacion
    {
        public int IdActivo { get; set; }
        public string CodComponente { get; set; }
        public string FechaRevision { get; set; }
        public string TipoDepreciacion { get; set; }
        public double Cantidad { get; set; }
        public string Placa { get; set; }
    }
}