using System.ComponentModel.DataAnnotations;

namespace MedicalCatalog.Domain.Entities;

public class Producto
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Descripcion { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Precio { get; set; }

    [Range(0, int.MaxValue)]
    public int Stock { get; set; }

    [Required]
    public int CategoriaId { get; set; }

    public Categoria? Categoria { get; set; }
}
