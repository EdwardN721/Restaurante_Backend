namespace Restaurante.Business.Dtos.Peticion;

public record PeticionEmpleadoDto(
    string Nombre,
    string PrimerApellido,
    string SegundoApellido,
    string Rol,
    bool Activo
    );