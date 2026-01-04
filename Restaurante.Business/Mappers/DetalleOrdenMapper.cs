using Restaurante.Business.Dtos.Peticion;
using Restaurante.Business.Dtos.Respuesta;
using Restaurante.Model.Models;

namespace Restaurante.Business.Mappers;

public static class DetalleOrdenMapper
{
    public static DetalleOrden ToEntity(this PeticionDetalleOrdenDto dto)
    {
        return new DetalleOrden
        {
            Cantidad = dto.Cantidad,
            PrecioUnitario = dto.PrecioUnitario,
            OrdenId = dto.OrdenId,
            ProductoId = dto.ProductoId,
        };
    }

    public static void UpdateFromDto(this DetalleOrden detalleOrden, PeticionDetalleOrdenDto dto)
    {
        detalleOrden.Cantidad = dto.Cantidad;
        detalleOrden.PrecioUnitario = dto.PrecioUnitario;
        detalleOrden.OrdenId = dto.OrdenId;
        detalleOrden.ProductoId = dto.ProductoId;
    }

    public static RespuestaDetalleOrdenDto ToDto(this DetalleOrden detalleOrden)
    {
        return new RespuestaDetalleOrdenDto(
            detalleOrden.Id,
            detalleOrden.Cantidad,
            detalleOrden.PrecioUnitario,
            detalleOrden.OrdenId,
            detalleOrden.ProductoId
            );
    }

    public static IEnumerable<RespuestaDetalleOrdenDto> ToDto(this IEnumerable<DetalleOrden>? detalleOrdens)
    {
        if (detalleOrdens == null) {return Enumerable.Empty<RespuestaDetalleOrdenDto>();}
        return detalleOrdens.Select(ToDto);
    }
}