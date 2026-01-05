using Restaurante.Business.Dtos.Peticion;
using Restaurante.Business.Dtos.Respuesta;

namespace Restaurante.Business.Interfaces;

public interface IEmpleadoService
{
    Task<IEnumerable<RespuestaEmpleadoDto>> ObtenerEmpleados();
    Task<RespuestaEmpleadoDto> ObtenerEmpleadoPorId(Guid id);
    Task<RespuestaEmpleadoDto> AgregarEmpleado(PeticionEmpleadoDto empleadoDto);
    Task ActualizarEmpleado(Guid id, PeticionEmpleadoDto empleadoDto);
    Task EliminarEmpleado(Guid id);
}