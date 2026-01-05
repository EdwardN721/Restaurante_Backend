using Microsoft.Extensions.Logging;
using Restaurante.Business.Dtos.Peticion;
using Restaurante.Business.Dtos.Respuesta;
using Restaurante.Business.Interfaces;
using Restaurante.Business.Mappers;
using Restaurante.Model.Interface;
using Restaurante.Model.Models;

namespace Restaurante.Business.Services;

public class EmpleadoService : IEmpleadoService
{
    private readonly IEmpleadoRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<EmpleadoService> _logger;

    public EmpleadoService(IEmpleadoRepository repository, IUnitOfWork unitOfWork, ILogger<EmpleadoService> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _unitOfWork = unitOfWork;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<RespuestaEmpleadoDto>> ObtenerEmpleados()
    {
        IEnumerable<Empleado> empleados = await _repository.ObtenerTodosAsync();
        return empleados.ToDto();
    }

    public async Task<RespuestaEmpleadoDto> ObtenerEmpleadoPorId(Guid id)
    {
        Empleado? empleado = await _repository.ObtenerPorIdAsync(id);
        if (empleado == null)
        {
            _logger.LogWarning("Empleado con el Id: {Id} no existe.", id);
            throw new KeyNotFoundException($"Empleado {id} no existe");
        }
        
        return empleado.ToDto(); 
    }

    public async Task<RespuestaEmpleadoDto> AgregarEmpleado(PeticionEmpleadoDto empleadoDto)
    {
        Empleado empleado =  empleadoDto.ToEntity();
        await _repository.CrearAsync(empleado);
        await _unitOfWork.SaveChangesAsync(CancellationToken.None);
        
        _logger.LogInformation("Empleado {Nombre} agregado exitosamente.", empleado.Nombre);
        return empleado.ToDto();
    }

    public async Task ActualizarEmpleado(Guid id, PeticionEmpleadoDto empleado)
    {
        Empleado? empleadoEncontrado = await _repository.ObtenerPorIdAsync(id);
        if (empleadoEncontrado == null)
        {
            _logger.LogWarning("Empleado con el Id: {Id} no existe.", id);
            throw new KeyNotFoundException($"Empleado {id} no existe");
        }
        
        empleadoEncontrado.UpdateFromDto(empleado);
        await _unitOfWork.SaveChangesAsync(CancellationToken.None);
        
        _logger.LogInformation("Empleado {Nombre} modificado exitosamente.", empleado.Nombre);
    }

    public async Task EliminarEmpleado(Guid id)
    {
        Empleado? empleadoEncontrado = await _repository.ObtenerPorIdAsync(id);
        if (empleadoEncontrado == null)
        {
            _logger.LogWarning("Empleado con el Id: {Id} no existe.", id);
            throw new KeyNotFoundException($"Empleado {id} no existe");
        }
        
        _repository.EliminarAsync(empleadoEncontrado);
        await _unitOfWork.SaveChangesAsync(CancellationToken.None);
        
        _logger.LogWarning("Empleado eliminado exitosamente.");
    }
}