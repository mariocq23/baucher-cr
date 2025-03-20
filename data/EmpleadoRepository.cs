using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using context.empleados;
using MongoDB.Driver;

namespace data
{
    public class EmpleadoRepository
    {
        private readonly IMongoCollection<Empleado> _empleados;

        public EmpleadoRepository(IMongoClient client, string databaseName, string collectionName = "Empleados")
        {
            var database = client.GetDatabase(databaseName);
            _empleados = database.GetCollection<Empleado>(collectionName);
        }

        public async Task<List<Empleado>> GetAllAsync()
        {
            return await _empleados.Find(_ => true).ToListAsync();
        }

        public async Task<Empleado> GetByIdAsync(string id)
        {
            return await _empleados.Find(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Empleado employee)
        {
            await _empleados.InsertOneAsync(employee);
        }

        public async Task UpdateAsync(string id, Empleado employee)
        {
            await _empleados.ReplaceOneAsync(e => e.Id == id, employee);
        }

        public async Task DeleteAsync(string id)
        {
            await _empleados.DeleteOneAsync(e => e.Id == id);
        }
    }
}
