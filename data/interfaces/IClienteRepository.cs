using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using context.clientes;

namespace data.interfaces
{
    public interface IClienteRepository
    {
        void Delete(int id);
        object? GetAll();
        object GetById(int id);
        void Update(Cliente clienteActualizado);
        void Add(Cliente cliente);
    }
}
