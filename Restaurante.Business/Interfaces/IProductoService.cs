using Restaurante.Business.Dtos.Peticion;
using Restaurante.Business.Dtos.Respuesta;
using Restaurante.Model.Models;

namespace Restaurante.Business.Interfaces;

public interface IProductoService
{
    Task<IEnumerable<RespuestaProductoDto>> ObtenerProductos();
    Task<RespuestaProductoDto> ObtenerProductoPorId(Guid id);
    Task<RespuestaProductoDto> CrearProducto(PeticionProductoDto producto);
    Task ActualizarProducto(Guid id, PeticionProductoDto producto);
    Task EliminarProducto(Guid id);
}