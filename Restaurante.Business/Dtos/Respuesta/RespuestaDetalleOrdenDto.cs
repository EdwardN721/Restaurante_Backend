namespace Restaurante.Business.Dtos.Respuesta;

public record RespuestaDetalleOrdenDto(
    int Id,
    int Cantidad,
    decimal PrecioUnitario,
    int OrdenId,
    int ProductoId
    );
