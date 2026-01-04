namespace Restaurante.Business.Dtos.Respuesta;

public record RespuestaCategoriaDto(
    int Id,
    string Nombre,
    string Descripcion,
    bool Activo,
    DateTime FechaRegistro,
    DateTime? FechaModificacion
    );