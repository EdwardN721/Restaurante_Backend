using Microsoft.Extensions.Logging;
using Restaurante.Business.Dtos.Peticion;
using Restaurante.Business.Dtos.Respuesta;
using Restaurante.Business.Interfaces;
using Restaurante.Business.Mappers;
using Restaurante.Model.Interface;
using Restaurante.Model.Models;

namespace Restaurante.Business.Services;

public class OrdenService : IOrdenService
{
    private readonly IOrdenRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<OrdenService> _logger;

    public OrdenService(IOrdenRepository repository, IUnitOfWork unitOfWork, ILogger<OrdenService> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger;
    }


    public async Task<IEnumerable<RespuestaOrdenDto>> ObtenerOrdenes()
    {
        IEnumerable<Orden> ordenes = (await _repository.ObtenerTodosAsync()).ToList();
        
        _logger.LogInformation("Total de ordenes: {Ordenes}.",ordenes.Count());
        return ordenes.ToDto();
    }

    public async Task<RespuestaOrdenDto> ObtenerOrdenPorId(int id)
    {
        Orden? orden = await _repository.ObtenerPorIdAsync(id);
        if (orden == null)
        {
            _logger.LogWarning("No existe la orden con el Id {id}", id);
            throw new KeyNotFoundException($"No se encontro la orden con el Id: {id}");
        }
        
        _logger.LogInformation("Orden con Id: {orden.Id}.",orden.Id);
        return orden.ToDto();
    }

    public async Task<RespuestaOrdenDto> CrearOrden(PeticionOrdenDto ordenDto)
    {
        Orden orden = ordenDto.ToEntity();
        await _repository.CrearAsync(orden);
        await _unitOfWork.SaveChangesAsync(CancellationToken.None);
        
        _logger.LogInformation("Orden agregada con exito: {Id}.",orden.Id);
        return orden.ToDto();
    }

    public async Task ActualizarOrden(int id, PeticionOrdenDto ordenDto)
    {
        Orden? orden = await _repository.ObtenerPorIdAsync(id);
        if (orden == null)
        {
            _logger.LogWarning("No existe la orden con el Id {id}", id);
            throw new KeyNotFoundException($"No se encontro la orden con el Id: {id}");
        }
        
        orden.UpdateFromDto(ordenDto);
        await _unitOfWork.SaveChangesAsync(CancellationToken.None);
        
        _logger.LogInformation("Orden actualizado con exito: {Id}.",orden.Id);
    }

    public async Task EliminarOrden(int id)
    {
        Orden? orden = await _repository.ObtenerPorIdAsync(id);
        if (orden == null)
        {
            _logger.LogWarning("No existe la orden con el Id {id}", id);
            throw new KeyNotFoundException($"No se encontro la orden con el Id: {id}");
        }
        
        _repository.EliminarAsync(orden);
        await _unitOfWork.SaveChangesAsync(CancellationToken.None);
        
        _logger.LogWarning("Orden eliminado con exito: {Id}.",orden.Id);
    }
}