namespace Restaurante.Business.Dtos.Respuesta;

public record RespuestaProductoDto(
        Guid Id,
        string Nombre,
        decimal Precio,
        bool Activo,
        DateTime FechaRegistro,
        DateTime? FechaModificacion,
        int CategoriaId
    );