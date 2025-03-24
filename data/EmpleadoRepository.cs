using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using context.empleados;
using context.facturas;
using data.interfaces;
using MongoDB.Driver;

namespace data
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private static int _nextId = 1;
        private static int _nextItemId = 1;
        IMongoCollection<Empleado>? _collection;

        public EmpleadoRepository()
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("mydatabase");
            _collection = database.GetCollection<Empleado>("facturas");
        }

        public async Task<List<Empleado>> GetAll()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Empleado> GetById(string id)
        {
            return await _collection.Find(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(Empleado employee)
        {
            await _collection.InsertOneAsync(employee);
        }

        public async Task Update(string id, Empleado employee)
        {
            await _collection.ReplaceOneAsync(e => e.Id == id, employee);
        }

        public async Task Delete(string id)
        {
            await _collection.DeleteOneAsync(e => e.Id == id);
        }
    }
}
