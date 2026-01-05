using Restaurante.Business.Dtos.Peticion;
using Restaurante.Business.Dtos.Respuesta;

namespace Restaurante.Business.Interfaces;

public interface IOrdenService
{
    Task<IEnumerable<RespuestaOrdenDto>> ObtenerOrdenes();
    Task<RespuestaOrdenDto> ObtenerOrdenPorId(int id);
    Task<RespuestaOrdenDto> CrearOrden(PeticionOrdenDto ordenDto);
    Task ActualizarOrden(int id, PeticionOrdenDto ordenDto);
    Task EliminarOrden(int id);
}