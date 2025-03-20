using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using context.cuentas;
using context.facturas;
using MongoDB.Bson;
using MongoDB.Driver;

namespace data
{
    public class FacturaRepository
    {
        private static int _nextId = 1;
        private static int _nextItemId = 1;
        IEnumerable<Factura> _facturas;
        IMongoCollection<Factura>? _collection;
        public FacturaRepository()
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("mydatabase");
            var collection = database.GetCollection<Factura>("facturas");
            var filter = Builders<Factura>.Filter.Empty;
            var cursor = collection.Find(filter);
            _collection = collection;
            _facturas = collection.AsQueryable();
        }

        public IEnumerable<Factura> GetAll()
        {
            return _facturas;
        }

        public Factura GetById(int id)
        {
            return _facturas.FirstOrDefault(b => b.Id == id);
        }

        public void Add(Factura bill)
        {
            bill.Id = _nextId++;
            _collection.InsertOne(bill);
        }

        public void Update(Factura facturaActualizada)
        {
            var update = Builders<Factura>.Update.Set(b => b, facturaActualizada);
            _collection.UpdateOne(b => b.Id == facturaActualizada.Id, update);
        }

        public void Delete(int id)
        {
            var filter = Builders<Factura>.Filter.Eq("Id", id);
            if (filter != null)
            {
                _collection.DeleteOne(filter);
            }
        }

        public void AddItem(int billId, FacturaDetalle item)
        {
            var bill = _facturas.FirstOrDefault(b => b.Id == billId);
            if (bill != null)
            {
                item.Id = _nextItemId++;
                item.IdFactura = billId;
                bill.Items.Add(item);
            }
        }

        public void UpdateItem(int billId, FacturaDetalle updatedItem)
        {
            var bill = _facturas.FirstOrDefault(b => b.Id == billId);
            if (bill != null)
            {
                var existingItem = bill.Items.FirstOrDefault(i => i.Id == updatedItem.Id);
                if (existingItem != null)
                {
                    bill.Items.Remove(existingItem);
                    bill.Items.Add(updatedItem);
                }
            }
        }

        public void RemoveItem(int billId, int itemId)
        {
            var bill = _facturas.FirstOrDefault(b => b.Id == billId);
            if (bill != null)
            {
                var itemToRemove = bill.Items.FirstOrDefault(i => i.Id == itemId);
                if (itemToRemove != null)
                {
                    bill.Items.Remove(itemToRemove);
                }
            }
        }

        public IEnumerable<Factura> ObtenerFacturasPorIdCuenta(int idCuenta)
        {
            return _facturas.Where(b => b.IdCuenta == idCuenta);
        }

        public Factura ObtenerFacturaPorId(int idFactura)
        {
            return _facturas.FirstOrDefault(b => b.Id == idFactura);
        }

        public object MarcarFacturaComoPaga(int idFactura)
        {
            var update = Builders<Factura>.Update.Set(b => b.Estado, "Pago");
            _collection.UpdateOne(b => b.Id == idFactura, update);
            return ObtenerFacturaPorId(idFactura);
        }
    }
}
