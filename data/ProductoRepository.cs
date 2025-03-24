using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using context.ordenes;
using context.productos;
using data.interfaces;
using MongoDB.Driver;

namespace data
{
    public class ProductoRepository : IProductoRepository
    {
        IMongoCollection<Producto>? _collection;

        public ProductoRepository()
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("mydatabase");
            _collection = database.GetCollection<Producto>("facturas");
        }

        public async Task<List<Producto>> GetAll()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Producto> GetById(string id)
        {
            return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(Producto product)
        {
            await _collection.InsertOneAsync(product);
        }

        public async Task Update(string id, Producto product)
        {
            await _collection.ReplaceOneAsync(p => p.Id == id, product);
        }

        public async Task Delete(string id)
        {
            await _collection.DeleteOneAsync(p => p.Id == id);
        }
    }
}
