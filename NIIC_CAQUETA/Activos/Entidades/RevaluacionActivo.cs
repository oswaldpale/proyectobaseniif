using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Activos.Clases
{
    public class RevaluacionActivo
    {
        public string idactivo { get; set; }

        public string idresponsable { get; set; }
        public int VrDepreciacion { get; set; }
        public int VrRazonable { get; set; }
        public int CostoInicial { get; set; }

        public int BaseDepreciable { get; set; }
        public int Importe_Depreciable { get; set; }
        public int Importe_libros { get; set; }

        public List<RevaluacionComponente> RevComponente { get; set; }
    }

  

}