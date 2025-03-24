using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using context.empleados;
using MongoDB.Driver;

namespace data.interfaces
{
    public interface IEmpleadoRepository
    {
        Task<List<Empleado>> GetAll();

        Task<Empleado> GetById(string id);
        Task Create(Empleado employee);
        Task Update(string id, Empleado employee);
        Task Delete(string id);
    }
}
