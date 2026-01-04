namespace Restaurante.Business.Dtos.Peticion;

public record PeticionProductoDto(
        string Nombre,
        decimal Precio,
        bool Activo,
        int CategoriaId
    );