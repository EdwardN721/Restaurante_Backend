namespace Restaurante.Business.Dtos.Peticion;

public record PeticionOrdenDto(
        int EstadoId,
        decimal Total,
        int MesaId,
        Guid EmpleadoId
    );