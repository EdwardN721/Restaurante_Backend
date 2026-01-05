namespace Restaurante.Model.Models;

public class DetalleOrden
{
    public int Id { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal SubTotal { get; set; }
    
    public int OrdenId { get; set; }
    public Orden? Orden { get; set; }
    
    public Guid ProductoId { get; set; }
    public Producto? Producto { get; set; }
}