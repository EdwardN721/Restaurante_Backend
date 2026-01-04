namespace Restaurante.Business.Dtos.Respuesta;

public record RespuestaMesaDto(
        int Id,
        string NumeroMesa,
        int Capacidad,
        string? Ubicacion,
        bool Activo,
        DateTime FechaRegistro,
        DateTime? FechaModificacion
    );