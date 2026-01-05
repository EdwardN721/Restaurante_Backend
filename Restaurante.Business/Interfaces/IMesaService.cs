using Restaurante.Business.Dtos.Peticion;
using Restaurante.Business.Dtos.Respuesta;
using Restaurante.Model.Models;

namespace Restaurante.Business.Interfaces;

public interface IMesaService
{
    Task<IEnumerable<RespuestaMesaDto>> ObtenerMesas();
    Task<RespuestaMesaDto> ObtenerMesaPorId(int id);
    Task<RespuestaMesaDto> IngresarMesa(PeticionMesaDto mesa);
    Task ActualizarMesa(int id, PeticionMesaDto mesa);
    Task EliminarMesa(int id);
}