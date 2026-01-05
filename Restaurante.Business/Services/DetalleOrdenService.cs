using Restaurante.Model.Interface;
using Microsoft.Extensions.Logging;
using Restaurante.Business.Interfaces;
using Restaurante.Business.Dtos.Peticion;
using Restaurante.Business.Dtos.Respuesta;
using Restaurante.Business.Mappers;
using Restaurante.Model.Exceptions;
using Restaurante.Model.Models;

namespace Restaurante.Business.Services;

public class DetalleOrdenService : IDetalleOrdenService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDetalleOrdenRepository _repository;
    private readonly ILogger<DetalleOrdenService> _logger;

    public DetalleOrdenService(IUnitOfWork unitOfWork, IDetalleOrdenRepository repository, ILogger<DetalleOrdenService> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger;
    }


    public async Task<IEnumerable<RespuestaDetalleOrdenDto>> ObtenerDetalleOrdenes()
    {
        IEnumerable<DetalleOrden> detalleOrdenes = (await _repository.ObtenerTodosAsync()).ToList();
        
        _logger.LogInformation("Total de Detalle ordenes: {Ordenes}.", detalleOrdenes.Count());
        return detalleOrdenes.ToDto();
    }

    public async Task<RespuestaDetalleOrdenDto> ObtenerDetalleOrdenPorId(int id)
    {
        DetalleOrden? detalleOrden = await _repository.ObtenerPorIdAsync(id);
        if (detalleOrden == null)
        {
            _logger.LogWarning("No existe el detalle orden {id}", id);
            throw new NotFoundException($"No se encontro el detalle orden con el Id: {id}");
        } 
        
        return detalleOrden.ToDto();
    }

    public async Task<RespuestaDetalleOrdenDto> AgregarDetalleOrden(PeticionDetalleOrdenDto detalleOrdenDto)
    {
        DetalleOrden detalleOrdenNuevo = detalleOrdenDto.ToEntity();
        
        await _repository.CrearAsync(detalleOrdenNuevo);
        await _unitOfWork.SaveChangesAsync(CancellationToken.None);
        
        _logger.LogInformation("Detalle Orden creado con éxito - {Id}.", detalleOrdenNuevo.Id);
        return detalleOrdenNuevo.ToDto();
    }

    public async Task AcualizarDetalleOrden(int id, PeticionDetalleOrdenDto ordenDto)
    {
        DetalleOrden? detalleOrden = await _repository.ObtenerPorIdAsync(id);
        if (detalleOrden == null)
        {
            _logger.LogWarning("No existe el detalle orden {id}", id);
            throw new NotFoundException($"No se encontro el detalle orden con el Id: {id}");
        }
        
        detalleOrden.UpdateFromDto(ordenDto);
        await _unitOfWork.SaveChangesAsync(CancellationToken.None);
        
        _logger.LogInformation("Detalle Orden modificado - {Id} con éxito.", id);
    }

    public async Task EliminarDetalleOrden(int id)
    {
        DetalleOrden? detalleOrden = await _repository.ObtenerPorIdAsync(id);
        if (detalleOrden == null)
        {
            _logger.LogWarning("No existe el detalle orden {id}", id);
            throw new NotFoundException($"No se encontro el detalle orden con el Id: {id}");
        }
        
        _repository.EliminarAsync(detalleOrden);
        await _unitOfWork.SaveChangesAsync(CancellationToken.None);
        
        _logger.LogWarning("Detalle Orden eliminado - {Id}.", id);
    }
}