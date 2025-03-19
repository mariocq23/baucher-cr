using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class OrdenDetalle
{
    public int Id { get; set; }
    public int IdOrden { get; set; }
    public Orden Orden { get; set; }

    [Required]
    public string Producto { get; set; }

    public int Cantidad { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal PrecioUnitario { get; set; }
}