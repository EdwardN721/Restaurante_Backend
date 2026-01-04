namespace Restaurante.Business.Dtos.Peticion;

public record PeticionCategoriaDto(
    string Nombre,
    string? Descripcion,
    bool Activo
    );