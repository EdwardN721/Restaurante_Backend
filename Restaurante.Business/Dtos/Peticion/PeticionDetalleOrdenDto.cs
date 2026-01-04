namespace Restaurante.Business.Dtos.Peticion;

public record PeticionDetalleOrdenDto(
        int Cantidad,
        decimal PrecioUnitario,
        int OrdenId,
        int ProductoId
    );