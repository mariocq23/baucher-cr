using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using context.clientes;
using context.facturas;
using context.ordenes;

namespace context.cuentas
{
    public class Cuenta
    {
        public int Id { get; set; }
        public int IdCliente { get; set; } // Foreign key linking to Customer
        public Cliente Cliente { get; set; } // Navigation property to the Customer
        public List<Orden> Ordenes { get; set; } = new List<Orden>();
        public List<Factura> Facturas { get; set; } = new List<Factura>();
    }
}
