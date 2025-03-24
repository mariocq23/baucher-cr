using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using context.clientes;
using context.empleados;
using context.facturas;
using context.ordenes;
using data.interfaces;
using MongoDB.Driver;

namespace data
{
    public class ClienteRepository: IClienteRepository
    {
        private static int _nextId = 1;
        private static int _nextItemId = 1;
        IMongoCollection<Cliente>? _collection;

        public ClienteRepository()
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("mydatabase");
            _collection = database.GetCollection<Cliente>("facturas");
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public object? GetAll()
        {
            throw new NotImplementedException();
        }

        public object GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Cliente clienteActualizado)
        {
            throw new NotImplementedException();
        }

        public void Add(Cliente cliente)
        {
            cliente.Id = _nextId++;
            _collection.InsertOneAsync(cliente);
        }
    }
}
