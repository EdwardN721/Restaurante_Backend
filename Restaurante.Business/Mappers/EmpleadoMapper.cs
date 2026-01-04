using Restaurante.Business.Dtos.Peticion;
using Restaurante.Business.Dtos.Respuesta;
using Restaurante.Model.Models;

namespace Restaurante.Business.Mappers;

public static class EmpleadoMapper
{
    public static Empleado ToEntity(this PeticionEmpleadoDto dto)
    {
        return new Empleado
        {
            Nombre = dto.Nombre,
            PrimerApellido = dto.PrimerApellido,
            SegundoApellido = dto.SegundoApellido,
            Rol = dto.Rol,
            Activo = dto.Activo,
            FechaRegistro = DateTime.UtcNow,
            FechaModificacion = null
        };
    }

    public static void UpdateFromDto(this Empleado empleado, PeticionEmpleadoDto dto)
    {
        empleado.Nombre = dto.Nombre;
        empleado.PrimerApellido = dto.PrimerApellido;
        empleado.SegundoApellido = dto.SegundoApellido;
        empleado.Rol = dto.Rol;
        empleado.Activo = dto.Activo;
        empleado.FechaModificacion = DateTime.UtcNow;
    }

    public static RespuestaEmpleadoDto ToDto(this Empleado empleado)
    {
        return new RespuestaEmpleadoDto(
            empleado.Id,
            empleado.Nombre,
            empleado.PrimerApellido,
            empleado.SegundoApellido,
            empleado.Rol,
            empleado.Activo,
            empleado.FechaRegistro,
            empleado.FechaModificacion
        );
    }

    public static IEnumerable<RespuestaEmpleadoDto> ToDto(this IEnumerable<Empleado>? empleados)
    {
        if (empleados == null)
        {
            return Enumerable.Empty<RespuestaEmpleadoDto>();
        }
        return empleados.Select(ToDto);
    }
}