using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace context.facturas
{
    public class FacturaDetalle
    {
        [Key]
        public int Id { get; set; }

        public int IdFactura { get; set; }
        public Factura Factura { get; set; }

        [Required]
        public string Descripcion { get; set; }

        public int Cantidad { get; set; } = 1;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal PrecioUnitario { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalDetalle
        {
            get { return Cantidad * PrecioUnitario; }
        }
    }
}
