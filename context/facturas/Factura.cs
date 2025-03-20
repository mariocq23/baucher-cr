using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using context.ordenes;
using System.Security.Principal;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using context.cuentas;

namespace context.facturas
{
    public class Factura
    {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }

        public string NumeroFactura { get; set; } = Guid.NewGuid().ToString(); // Unique identifier

        public DateTime FechaExpedicion { get; set; } = DateTime.UtcNow;

        public DateTime? FechaExpiracion { get; set; }

        public string Cliente { get; set; }

        public List<FacturaDetalle> Items { get; set; } = new List<FacturaDetalle>();

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Subtotal
        {
            get { return Items.Sum(item => item.Cantidad * item.PrecioUnitario); }
        }

        // Optional: Add fields for discounts, taxes (could be a list for different tax types)
        [Column(TypeName = "decimal(18, 2)")]
        public decimal MontoDescuento { get; set; } = 0;

        // Example for a single tax rate
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PorcentajeImpuestos { get; set; } = 0;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal MontoImpuestos
        {
            get { return (Subtotal - MontoDescuento) * PorcentajeImpuestos; }
        }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal MontoTotal
        {
            get { return (Subtotal - MontoDescuento) + MontoImpuestos; }
        }

        public string Estado { get; set; } = "Expedido"; // Example: Issued, Paid, Overdue, Cancelled

        [BsonRepresentation(BsonType.ObjectId)]
        public int IdOrden {  get; set; }

        [BsonIgnore]
        public Orden Orden { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public int IdCuenta { get; set; }
        [BsonIgnore]
        public Cuenta Cuenta { get; set; }
        public bool EstaPago => Estado == "Pago";

    }
}
