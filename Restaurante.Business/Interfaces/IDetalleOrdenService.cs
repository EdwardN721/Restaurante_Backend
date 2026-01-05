using Restaurante.Business.Dtos.Peticion;
using Restaurante.Business.Dtos.Respuesta;

namespace Restaurante.Business.Interfaces;

public interface IDetalleOrdenService
{
    Task<IEnumerable<RespuestaDetalleOrdenDto>> ObtenerDetalleOrdenes();
    Task<RespuestaDetalleOrdenDto> ObtenerDetalleOrdenPorId(int id);
    Task<RespuestaDetalleOrdenDto> AgregarDetalleOrden(PeticionDetalleOrdenDto detalleOrdenDto);
    Task AcualizarDetalleOrden(int id, PeticionDetalleOrdenDto ordenDto);
    Task EliminarDetalleOrden(int id);
}