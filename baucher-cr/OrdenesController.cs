using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BettingCMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdenesController : ControllerBase
    {
        // In a real application, you would use a database.
        private static List<Orden> _ordenes = new List<Orden>();

        //Event endpoints
        [HttpGet("ordenes")]
        public ActionResult<IEnumerable<Orden>> GetOrdenes()
        {
            return _ordenes;
        }

        [HttpGet("ordenes/{id}")]
        public ActionResult<Orden> GetOrden(int id)
        {
            var @event = _ordenes.FirstOrDefault(e => e.Id == id);
            if (@event == null)
            {
                return NotFound();
            }
            return @event;
        }

        [HttpPost("ordenes/add")]
        public ActionResult<Orden> CreateOrden(Orden @orden)
        {
            @orden.Id = _ordenes.Count + 1; // Simple ID generation
            _ordenes.Add(@orden);
            return CreatedAtAction(nameof(GetOrden), new { id = @orden.Id }, @orden);
        }

        [HttpPut("ordenes/{id}")]
        public IActionResult UpdateOrden(int id, Orden @orden)
        {
            if (id != @orden.Id)
            {
                return BadRequest();
            }

            var existingEvent = _ordenes.FirstOrDefault(e => e.Id == id);
            if (existingEvent == null)
            {
                return NotFound();
            }

            // Update properties
            existingEvent.Id = @orden.Id;
            existingEvent.NumeroOrden = @orden.NumeroOrden;
            existingEvent.Fecha = @orden.Fecha;
            existingEvent.Proveedor = @orden.Proveedor;
            existingEvent.Items = @orden.Items;

            return NoContent();
        }

        [HttpDelete("ordenes/{id}")]
        public IActionResult DeleteOrden(int id)
        {
            var @event = _ordenes.FirstOrDefault(e => e.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            _ordenes.Remove(@event);
            return NoContent();
        }
    }
}