using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using context.productos;

namespace data.interfaces
{
    public interface IProductoRepository
    {
        Task<List<Producto>> GetAll();
        Task<Producto> GetById(string id);
        Task Create(Producto product);
        Task Update(string id, Producto product);
        Task Delete(string id);
    }
}
