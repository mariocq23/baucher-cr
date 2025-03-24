using context.facturas;
using context.ordenes;
using data;
using data.interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace baucher_api.controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdenesController : ControllerBase
    {
        // In a real application, you would use a database.
        private readonly IOrdenRepository _ordenRepository;
        private readonly IFacturaRepository _facturaRepository;


        public OrdenesController(IOrdenRepository ordenRepository, IFacturaRepository facturaRepository)
        {
            _ordenRepository = ordenRepository;
            _facturaRepository = facturaRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Orden>> Get()
        {
            return Ok(_ordenRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Orden> Get(int id)
        {
            var orden = _ordenRepository.GetById(id);
            if (orden == null)
            {
                return NotFound();
            }
            return Ok(orden);
        }

        [HttpPost]
        public ActionResult<Orden> Post([FromBody] Orden order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _ordenRepository.Add(order);
            return CreatedAtAction(nameof(Get), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Orden ordenActualizada)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != ordenActualizada.Id)
            {
                return BadRequest("The ID in the URL does not match the ID in the request body.");
            }

            var existingOrder = _ordenRepository.GetById(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            _ordenRepository.Update(ordenActualizada);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var orderToDelete = _ordenRepository.GetById(id);
            if (orderToDelete == null)
            {
                return NotFound();
            }

            _ordenRepository.Delete(id);
            return NoContent();
        }

        // New action to create a Bill from a Purchase Order
        [HttpPost("{orderId}/factura")]
        public ActionResult<Factura> CrearFacturaOrden(int orderId, [FromBody] SolicitudCreacionFacturaOrden request)
        {
            var order = _ordenRepository.GetById(orderId);
            if (order == null)
            {
                return NotFound($"Orden con ID {orderId} no fue encontrada.");
            }

            if (order.IdFactura.HasValue)
            {
                return BadRequest($"A bill has already been created for Purchase Order with ID {orderId} (Bill ID: {order.IdFactura}).");
            }

            var bill = new Factura
            {
                NumeroFactura = $"FACTURA-{order.NumeroOrden}", // Generate a bill number
                FechaExpedicion = DateTime.UtcNow,
                Cliente = request.Cliente ?? order.Proveedor, // Default to supplier if not provided
                FechaExpiracion = request.FechaExpiracion,
                IdOrden = orderId,
                Items = order.Items.Select(oi => new FacturaDetalle
                {
                    Descripcion = oi.Producto,
                    Cantidad = oi.Cantidad,
                    PrecioUnitario = oi.PrecioUnitario
                }).ToList(),
                PorcentajeImpuestos = request.PorcentajeImpuestos
            };

            _facturaRepository.Add(bill);

            // Update the Purchase Order to link to the Bill
            order.IdFactura = bill.Id;
            _ordenRepository.Update(order);

            return CreatedAtAction("GetBill", "Bills", new { id = bill.Id }, bill);
        }
    }
}