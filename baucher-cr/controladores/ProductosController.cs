using context.productos;
using data;
using data.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ProductEmployeeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductoRepository _productoRepository;

        public ProductsController(IProductoRepository productRepository)
        {
            _productoRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProducts()
        {
            var productos = await _productoRepository.GetAll();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProduct(string id)
        {
            var producto = await _productoRepository.GetById(id);

            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> CreateProduct(Producto producto)
        {
            await _productoRepository.Create(producto);
            return CreatedAtAction(nameof(GetProduct), new { id = producto.Id }, producto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, Producto producto)
        {
            var productoExistente = await _productoRepository.GetById(id);
            if (productoExistente == null)
            {
                return NotFound();
            }

            producto.Id = id; // Ensure the ID is set for the update
            await _productoRepository.Update(id, producto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var existingProduct = await _productoRepository.GetById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            await _productoRepository.Delete(id);
            return NoContent();
        }
    }
}