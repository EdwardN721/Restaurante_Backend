namespace Restaurante.Business.Dtos.Peticion;

public record PeticionMesaDto(
        string NumeroMesa,
        int Capacidad,
        string? Ubicacion,
        bool Activo
    );