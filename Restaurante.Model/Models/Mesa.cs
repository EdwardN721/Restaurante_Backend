namespace Restaurante.Model.Models;

public class Mesa
{
    public int Id { get; set; }
    public string NumeroMesa { get; set; } = string.Empty;
    public int Capacidad { get; set; }
    public string? Ubicacion { get; set; }
    public bool Activo { get; set; }
    public DateTime FechaRegistro { get; set; }
    public DateTime? FechaModificacion { get; set; }
}