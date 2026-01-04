using Restaurante.Business.Dtos.Peticion;
using Restaurante.Business.Dtos.Respuesta;
using Restaurante.Model.Models;

namespace Restaurante.Business.Mappers;

public static class MesaMapper
{
    public static Mesa ToEntity(this PeticionMesaDto dto)
    {
        return new Mesa
        {
            NumeroMesa = dto.NumeroMesa,
            Capacidad = dto.Capacidad,
            Ubicacion = dto.Ubicacion,
            Activo = dto.Activo,
            FechaRegistro = DateTime.UtcNow,
            FechaModificacion = null
        };
    }

    public static void UpdateFromDto(this Mesa mesa, PeticionMesaDto dto)
    {
        mesa.NumeroMesa = dto.NumeroMesa;
        mesa.Capacidad = dto.Capacidad;
        mesa.Ubicacion = dto.Ubicacion;
        mesa.Activo = dto.Activo;
        mesa.FechaModificacion = DateTime.UtcNow;
    }

    public static RespuestaMesaDto ToDto(this Mesa mesa)
    {
        return new RespuestaMesaDto(
            mesa.Id,
            mesa.NumeroMesa,
            mesa.Capacidad,
            mesa.Ubicacion,
            mesa.Activo,
            mesa.FechaRegistro,
            mesa.FechaModificacion);
    }

    public static IEnumerable<RespuestaMesaDto> ToDto(this IEnumerable<Mesa>? mesas)
    {
        if (mesas == null)
        {
            return Enumerable.Empty<RespuestaMesaDto>();
        }

        return mesas.Select(ToDto);
    }
}