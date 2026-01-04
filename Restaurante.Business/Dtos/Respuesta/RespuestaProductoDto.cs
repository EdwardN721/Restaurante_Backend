namespace Restaurante.Business.Dtos.Respuesta;

public record RespuestaProductoDto(
        int Id,
        string Nombre,
        decimal Precio,
        bool Activo,
        DateTime FechaRegistro,
        DateTime? FechaModificacion,
        int CategoriaId
    );