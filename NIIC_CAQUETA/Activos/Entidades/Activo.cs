using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Activos.Clases
{
    public class Activo
    {
        public int idactivo { get; set; }
        public string idsubclase { get; set; }
        public string nombresubclase { get; set; }
        public string idresponsable { get; set; }
       
        public string Nombre { get; set; }
        public string Placa { get; set; }
        public string Estado { get; set; }
        public string Fecha_Udepreciacion { get; set; }
        public double baseDepreciable { get; set; }
        public double VrDepreciacion { get; set; }

        public double VrDeterioro { get; set; }
        public double VrResidual { get; set; }
        public double VrRazonable { get; set; }
        public double AjusteDepreAcum { get; set; }
        public string NoCompra { get; set; }

        public int Cant_Dif_LRecta { get; set; }

        public string Fecha_Revision { get; set; }
        public double CostoInicial { get; set; }

        public double dep_mes { get; set; }

       

        public double ImporteDepreciable { get; set; }

        public double Importe_libros { get; set; }

        public double cantidadDias { get; set; }    

        public List<ActivoComponente> Componente { get; set; }

    }
}