using context.facturas;
using data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[ApiController]
[Route("api/cuentas/{idCuenta}/facturas")]
public class FacturasController : ControllerBase
{
    private readonly FacturaRepository _facturaRepository;

    public FacturasController(FacturaRepository facturaRepository)
    {
        _facturaRepository = facturaRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Factura>> Get(int idCuenta)
    {
        var facturas = _facturaRepository.ObtenerFacturasPorIdCuenta(idCuenta);
        return Ok(facturas);
    }

    [HttpGet("{idFactura}", Name = "ObtenerFactura")]
    public ActionResult<Factura> Get(int idCuenta, int idFactura)
    {
        var factura = _facturaRepository.ObtenerFacturaPorId(idFactura);
        if (factura == null || factura.IdCuenta != idCuenta)
        {
            return NotFound();
        }
        return Ok(factura);
    }

    [HttpPut("{idFactura}/pagar")]
    public ActionResult<Factura> PagarFactura(int idCuenta, int idFactura)
    {
        var bill = _facturaRepository.ObtenerFacturaPorId(idFactura);
        if (bill == null || bill.IdCuenta != idCuenta)
        {
            return NotFound();
        }
        var updatedBill = _facturaRepository.MarcarFacturaComoPaga(idFactura);
        return Ok(updatedBill);
    }

    // Add PUT and DELETE endpoints if needed
}