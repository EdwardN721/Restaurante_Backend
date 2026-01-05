using Microsoft.Extensions.Logging;
using Restaurante.Business.Dtos.Peticion;
using Restaurante.Business.Dtos.Respuesta;
using Restaurante.Business.Interfaces;
using Restaurante.Business.Mappers;
using Restaurante.Model.Interface;
using Restaurante.Model.Models;

namespace Restaurante.Business.Services;

public class MesaService : IMesaService
{
    private readonly IMesaRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<MesaService> _logger;

    public MesaService(IMesaRepository repository, IUnitOfWork unitOfWork, ILogger<MesaService> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger;
    }

    public async Task<IEnumerable<RespuestaMesaDto>> ObtenerMesas()
    {
        IEnumerable<Mesa> mesas = (await _repository.ObtenerTodosAsync()).ToList();
        
        _logger.LogInformation($"Total de mesas: {mesas.Count()}");
        return mesas.ToDto();
    }

    public async Task<RespuestaMesaDto> ObtenerMesaPorId(int id)
    {
        Mesa? mesa = await _repository.ObtenerPorIdAsync(id);
        if (mesa == null)
        {
            _logger.LogError("No se encontro el mesa {Id}", id);
            throw new KeyNotFoundException($"No se encontró la mesa con el Id {id}.");
        }
        
        return mesa.ToDto(); 
    }

    public async Task<RespuestaMesaDto> IngresarMesa(PeticionMesaDto mesa)
    {
        Mesa mesaNueva = mesa.ToEntity();
        await _repository.CrearAsync(mesaNueva);
        await _unitOfWork.SaveChangesAsync(CancellationToken.None);
        
        _logger.LogInformation("Mesa agregada con exito.");
        return mesaNueva.ToDto();
    }

    public async Task ActualizarMesa(int id, PeticionMesaDto mesa)
    {
        Mesa? mesaEntonctrada = await _repository.ObtenerPorIdAsync(id);
        if (mesaEntonctrada == null)
        {
            _logger.LogError("No se encontro el mesa {Id}", id);
            throw new KeyNotFoundException($"No se encontró la mesa con el Id {id}.");
        }
        
        mesaEntonctrada.UpdateFromDto(mesa);
        await _unitOfWork.SaveChangesAsync(CancellationToken.None);
        
        _logger.LogInformation("Mesa actualizada con exito.");
    }

    public async Task EliminarMesa(int id)
    {
        Mesa? mesa = await _repository.ObtenerPorIdAsync(id);
        if (mesa == null)
        {
            _logger.LogError("No se encontro el mesa {Id}", id);
            throw new KeyNotFoundException($"No se encontró la mesa con el Id {id}.");
        }
        
        _repository.EliminarAsync(mesa);
        await _unitOfWork.SaveChangesAsync(CancellationToken.None);
        
        _logger.LogInformation("Mesa eliminada con exito.");
    }
}