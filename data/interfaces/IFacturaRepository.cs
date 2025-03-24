using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using context.facturas;
using MongoDB.Driver;

namespace data.interfaces
{
    public interface IFacturaRepository
    {
        IEnumerable<Factura> GetAll();
        Factura GetById(int id);
        void Add(Factura bill);
        void Update(Factura facturaActualizada);
        void Delete(int id);
        void AddItem(int billId, FacturaDetalle item);
        void UpdateItem(int billId, FacturaDetalle updatedItem);
        void RemoveItem(int billId, int itemId);
        IEnumerable<Factura> ObtenerFacturasPorIdCuenta(int idCuenta);
        Factura ObtenerFacturaPorId(int idFactura);
        object MarcarFacturaComoPaga(int idFactura);
    }
}
