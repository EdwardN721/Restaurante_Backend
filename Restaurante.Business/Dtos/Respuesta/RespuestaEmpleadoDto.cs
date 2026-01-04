namespace Restaurante.Business.Dtos.Respuesta;

public record RespuestaEmpleadoDto(
    Guid Id,
    string Nombre,
    string PrimerApellido,
    string? SegundoApellido,
    string Rol,
    bool Activo,
    DateTime FechaRegistro,
    DateTime? FechaModificacion
    );