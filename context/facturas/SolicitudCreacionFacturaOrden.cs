using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace context.facturas
{
    public class SolicitudCreacionFacturaOrden
    {
        public string Cliente { get; set; } // Optional: Could be different from the supplier
        public DateTime? FechaExpiracion { get; set; }
        public decimal PorcentajeImpuestos { get; set; } = 0;
    }
}
