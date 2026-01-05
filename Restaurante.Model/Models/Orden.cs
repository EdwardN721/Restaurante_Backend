using Restaurante.Model.Enums;

namespace Restaurante.Model.Models;

public class Orden
{
    public int Id { get; set; }
    public DateTime FechaOrden { get; set; }
    public Estado Estado { get; set; }
    public decimal Total { get; set; }
    public DateTime? FechaModificacion { get; set; }
    
    public int MesaId { get; set; }
    public Mesa? Mesa { get; set; }
    
    public Guid EmpleadoId { get; set; }
    public Empleado? Empleado { get; set; }
    
    public ICollection<DetalleOrden> DetalleOrden { get; set; } = new List<DetalleOrden>();
}