using Restaurante.Business.Dtos.Peticion;
using Restaurante.Business.Dtos.Respuesta;

namespace Restaurante.Business.Interfaces;

public interface ICategoriaService
{
    Task<IEnumerable<RespuestaCategoriaDto>> ObtenerCategorias();
    Task<RespuestaCategoriaDto> ObtenerCategoriaPorId(int id);
    Task<RespuestaCategoriaDto> AgregarCategoria(PeticionCategoriaDto categoria);
    Task ActualizarCategoria(int id, PeticionCategoriaDto categoria);
    Task EliminarCategoria(int id);
}