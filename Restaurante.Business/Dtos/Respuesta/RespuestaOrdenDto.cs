namespace Restaurante.Business.Dtos.Respuesta;

public record RespuestaOrdenDto(
        int Id,
        DateTime FechaOrdem,
        string Estado,
        int EstadoId,
        decimal Total,
        DateTime? FechaModificacion,
        int MesaId,
        Guid EmpleadoId
    );