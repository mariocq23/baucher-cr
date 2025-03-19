using System.ComponentModel.DataAnnotations;

public class Orden
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string NumeroOrden { get; set; }

    public DateTime Fecha { get; set; } = DateTime.UtcNow;

    [Required]
    public string Proveedor { get; set; }

    public List<OrdenDetalle> Items { get; set; } = new List<OrdenDetalle>();

    public decimal MontoTotal
    {
        get { return Items.Sum(item => item.Cantidad * item.PrecioUnitario); }
    }
}