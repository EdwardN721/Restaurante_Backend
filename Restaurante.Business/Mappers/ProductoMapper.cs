using Restaurante.Business.Dtos.Peticion;
using Restaurante.Business.Dtos.Respuesta;
using Restaurante.Model.Models;

namespace Restaurante.Business.Mappers;

public static class ProductoMapper
{
    public static Producto ToEntity(this PeticionProductoDto dto)
    {
        return new Producto
        {
            Nombre = dto.Nombre,
            Precio = dto.Precio,
            Activo = dto.Activo,
            FechaRegistro = DateTime.UtcNow,
            FechaModificacion = null,
            CategoriaId = dto.CategoriaId
        };
    }

    public static void UpdateFromDto(this Producto producto, PeticionProductoDto dto)
    {
        producto.Nombre = dto.Nombre;
        producto.Precio = dto.Precio;
        producto.Activo = dto.Activo;
        producto.FechaModificacion = DateTime.UtcNow;
        producto.CategoriaId = dto.CategoriaId;
    }

    public static RespuestaProductoDto ToDto(this Producto producto)
    {
        return new RespuestaProductoDto(
            producto.Id,
            producto.Nombre,
            producto.Precio,
            producto.Activo,
            producto.FechaRegistro,
            producto.FechaModificacion,
            producto.CategoriaId
            );
    }

    public static IEnumerable<RespuestaProductoDto> ToDto(this IEnumerable<Producto>? productos)
    {
        if (productos == null)
        {
            return Enumerable.Empty<RespuestaProductoDto>();
        }
        
        return productos.Select(ToDto);
    }
}