using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using context.ordenes;

namespace data.interfaces
{
    public interface IOrdenRepository
    {
        IEnumerable<Orden> GetAll();
        Orden GetById(int id);
        void Add(Orden order);
        void Update(Orden updatedOrder);
        void Delete(int id);
    }
}
