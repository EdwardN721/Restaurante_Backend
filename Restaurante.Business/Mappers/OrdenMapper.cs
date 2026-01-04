using Restaurante.Business.Dtos.Peticion;
using Restaurante.Business.Dtos.Respuesta;
using Restaurante.Model.Enums;
using Restaurante.Model.Models;

namespace Restaurante.Business.Mappers;

public static class OrdenMapper
{
    public static Orden ToEntity(this PeticionOrdenDto dto)
    {
        return new Orden
        {
            FechaOrden = DateTime.UtcNow,
            Estado = (Estado)dto.EstadoId,
            Total = dto.Total,
            MesaId = dto.MesaId,
            EmpleadoId = dto.EmpleadoId
        };
    }

    public static void UpdateFromDto(this Orden mesa, PeticionOrdenDto dto)
    {
        mesa.Estado = (Estado)dto.EstadoId;
        mesa.Total = dto.Total;
        mesa.MesaId = dto.MesaId;
        mesa.EmpleadoId = dto.EmpleadoId;
        mesa.FechaModificacion = DateTime.UtcNow;
    }

    public static RespuestaOrdenDto ToDto(this Orden orden)
    {
        return new RespuestaOrdenDto(
            orden.Id,
            orden.FechaOrden,
            orden.Estado.ToString(),
            (int)orden.Estado,
            orden.Total,
            orden.FechaModificacion,
            orden.MesaId,
            orden.EmpleadoId
            );
    }

    public static IEnumerable<RespuestaOrdenDto> ToDto(this IEnumerable<Orden>? ordenes)
    {
        if (ordenes == null)
        {
            return Enumerable.Empty<RespuestaOrdenDto>();
        }
        
        return ordenes.Select(ToDto);
    }
}