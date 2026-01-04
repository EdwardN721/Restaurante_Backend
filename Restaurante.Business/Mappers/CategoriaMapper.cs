using Restaurante.Model.Models;
using Restaurante.Business.Dtos.Peticion;
using Restaurante.Business.Dtos.Respuesta;

namespace Restaurante.Business.Mappers;

public static class CategoriaMapper
{
    public static Categoria ToEntity(this PeticionCategoriaDto categoria)
    {
        return new Categoria
        {
            Nombre = categoria.Nombre,
            Descripcion = categoria.Descripcion,
            Activo = categoria.Activo,
            FechaRegistro = DateTime.UtcNow,
            FechaModificacion = null
        };
    }

    public static void UpdateFromDto(this Categoria categoria, PeticionCategoriaDto dto)
    {
        categoria.Nombre = dto.Nombre;
        categoria.Descripcion = dto.Descripcion;
        categoria.Activo = dto.Activo;
        categoria.FechaModificacion = DateTime.UtcNow;
    }

    public static RespuestaCategoriaDto ToDto(this Categoria categoria)
    {
        return new RespuestaCategoriaDto(
            categoria.Id,
            categoria.Nombre,
            categoria.Descripcion,
            categoria.Activo,
            categoria.FechaRegistro,
            categoria.FechaModificacion
            );
    }

    public static IEnumerable<RespuestaCategoriaDto> ToDto(this IEnumerable<Categoria>? categorias)
    {
        if (categorias == null) {return Enumerable.Empty<RespuestaCategoriaDto>();}
        return categorias.Select(ToDto);
    }
}