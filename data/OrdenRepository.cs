using context.empleados;
using context.facturas;
using context.ordenes;
using data.interfaces;
using MongoDB.Driver;

namespace data
{
    public class OrdenRepository : IOrdenRepository
    {
        IMongoCollection<Orden>? _collection;
        private static int _nextId = 1;

        public OrdenRepository() 
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("mydatabase");
            _collection = database.GetCollection<Orden>("facturas");
        }

        public IEnumerable<Orden> GetAll()
        {
            return _collection.AsQueryable();
        }

        public Orden GetById(int id)
        {
            return _collection.AsQueryable().FirstOrDefault(o => o.Id == id);
        }

        public void Add(Orden order)
        {
            order.Id = _nextId++;
            _collection.InsertOneAsync(order);
        }

        public void Update(Orden updatedOrder)
        {
            var existingOrder = _collection.AsQueryable().FirstOrDefault(o => o.Id == updatedOrder.Id);
            if (existingOrder != null)
            {
                _collection.DeleteOneAsync(o => o.Id == updatedOrder.Id);
                _collection.InsertOneAsync(updatedOrder);
            }
        }

        public void Delete(int id)
        {
            var orderToRemove = _collection.AsQueryable().FirstOrDefault(o => o.Id == id);
            if (orderToRemove != null)
            {
                _collection.DeleteOneAsync(o => o.Id == id);
            }
        }
    }
}
