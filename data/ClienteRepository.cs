using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using context.clientes;
using context.facturas;
using context.ordenes;
using MongoDB.Driver;

namespace data
{
    public class ClienteRepository
    {
        private static int _nextId = 1;
        private static int _nextItemId = 1;
        List<Cliente> _clientes;
        IMongoCollection<Cliente>? _collection;
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
            _clientes.Add(cliente);
        }
    }
}
