namespace Restaurante.Model.Models;

public class DetalleOrden
{
    public int Id { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    
    public int OrdenId { get; set; }
    public virtual Orden? Orden { get; set; }
    
    public int ProductoId { get; set; }
    public virtual Producto? Productos { get; set; }
}