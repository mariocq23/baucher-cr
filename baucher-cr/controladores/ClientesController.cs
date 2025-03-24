using context.clientes;
using context.facturas;
using context.ordenes;
using data;
using data.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace baucher_api.controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        // In a real application, you would use a database.
        private readonly IClienteRepository _clienteRepository;


        public ClientesController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> Get()
        {
            return Ok(_clienteRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Cliente> Get(int id)
        {
            var cliente = _clienteRepository.GetById(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpPost]
        public ActionResult<Cliente> Post([FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _clienteRepository.Add(cliente);
            return CreatedAtAction(nameof(Get), new { id = cliente.Id }, cliente);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Cliente clienteActualizado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != clienteActualizado.Id)
            {
                return BadRequest("The ID in the URL does not match the ID in the request body.");
            }

            var clienteExistente = _clienteRepository.GetById(id);
            if (clienteExistente == null)
            {
                return NotFound();
            }

            _clienteRepository.Update(clienteActualizado);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var clienteABorrar = _clienteRepository.GetById(id);
            if (clienteABorrar == null)
            {
                return NotFound();
            }

            _clienteRepository.Delete(id);
            return NoContent();
        }
    }
}