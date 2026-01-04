namespace Restaurante.Model.Models;

public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public bool Activo { get; set; }
    public DateTime FechaRegistro { get; set; }
    public DateTime? FechaModificacion { get; set; }
    
    public int CategoriaId { get; set; }
    public virtual Categoria? Categoria { get; set; }
}