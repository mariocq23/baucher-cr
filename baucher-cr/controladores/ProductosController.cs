using context.productos;
using data;
using Microsoft.AspNetCore.Mvc;

namespace ProductEmployeeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductoRepository _productoRepository;

        public ProductsController(ProductoRepository productRepository)
        {
            _productoRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProducts()
        {
            var productos = await _productoRepository.GetAllAsync();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProduct(string id)
        {
            var producto = await _productoRepository.GetByIdAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> CreateProduct(Producto producto)
        {
            await _productoRepository.CreateAsync(producto);
            return CreatedAtAction(nameof(GetProduct), new { id = producto.Id }, producto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, Producto producto)
        {
            var productoExistente = await _productoRepository.GetByIdAsync(id);
            if (productoExistente == null)
            {
                return NotFound();
            }

            producto.Id = id; // Ensure the ID is set for the update
            await _productoRepository.UpdateAsync(id, producto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var existingProduct = await _productoRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            await _productoRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}