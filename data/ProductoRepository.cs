using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using context.productos;
using MongoDB.Driver;

namespace data
{
    public class ProductoRepository
    {
        private readonly IMongoCollection<Producto> _productos;

        public ProductoRepository(IMongoClient client, string databaseName, string collectionName = "Productos")
        {
            var database = client.GetDatabase(databaseName);
            _productos = database.GetCollection<Producto>(collectionName);
        }

        public async Task<List<Producto>> GetAllAsync()
        {
            return await _productos.Find(_ => true).ToListAsync();
        }

        public async Task<Producto> GetByIdAsync(string id)
        {
            return await _productos.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Producto product)
        {
            await _productos.InsertOneAsync(product);
        }

        public async Task UpdateAsync(string id, Producto product)
        {
            await _productos.ReplaceOneAsync(p => p.Id == id, product);
        }

        public async Task DeleteAsync(string id)
        {
            await _productos.DeleteOneAsync(p => p.Id == id);
        }
    }
}
