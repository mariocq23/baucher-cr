using context.empleados;
using data;
using Microsoft.AspNetCore.Mvc;

namespace baucher_api.controladores
{

    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadosController : ControllerBase
    {
        private readonly EmpleadoRepository _empleadoRepository;

        public EmpleadosController(EmpleadoRepository empleadoRepository)
        {
            _empleadoRepository = empleadoRepository;
        }

        // Implement similar CRUD operations for EmployeesController
        // (Get all, Get by ID, Create, Update, Delete)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetEmployees()
        {
            var employees = await _empleadoRepository.GetAllAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> GetEmployee(string id)
        {
            var employee = await _empleadoRepository.GetByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Empleado>> CreateEmployee(Empleado empleado)
        {
            await _empleadoRepository.CreateAsync(empleado);
            return CreatedAtAction(nameof(GetEmployee), new { id = empleado.Id }, empleado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(string id, Empleado empleado)
        {
            var existingEmployee = await _empleadoRepository.GetByIdAsync(id);
            if (existingEmployee == null)
            {
                return NotFound();
            }

            empleado.Id = id; // Ensure the ID is set for the update
            await _empleadoRepository.UpdateAsync(id, empleado);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            var existingEmployee = await _empleadoRepository.GetByIdAsync(id);
            if (existingEmployee == null)
            {
                return NotFound();
            }

            await _empleadoRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}