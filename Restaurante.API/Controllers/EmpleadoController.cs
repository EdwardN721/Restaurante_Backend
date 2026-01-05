using Microsoft.AspNetCore.Mvc;
using Restaurante.Business.Dtos.Peticion;
using Restaurante.Business.Dtos.Respuesta;
using Restaurante.Business.Interfaces;
using Restaurante.Model.Exceptions;

namespace Restaurante.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmpleadoController : ControllerBase
{
    private readonly ILogger<EmpleadoController> _logger;
    private readonly IEmpleadoService _service;

    public EmpleadoController(ILogger<EmpleadoController> logger, IEmpleadoService service)
    {
        _logger = logger;
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }
    
    [HttpGet]
    public async Task<IActionResult> ObtenerEmpleados()
    {
        IEnumerable<RespuestaEmpleadoDto> empleados = await _service.ObtenerEmpleados();
        return Ok(empleados); 
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObtenerEmpleadosPorId(Guid id)
    {
        try
        {
            RespuestaEmpleadoDto empleado = await _service.ObtenerEmpleadoPorId(id);
            return Ok(empleado);
        }
        catch (NotFoundException exception)
        {
            _logger.LogError(exception, exception.Message);
            return NotFound(exception.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AgregarEmpleado([FromBody] PeticionEmpleadoDto empleadoDto)
    {
        RespuestaEmpleadoDto empleado = await _service.AgregarEmpleado(empleadoDto);
        return Ok(empleado);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> AcualizarEmpleado(Guid id, [FromBody] PeticionEmpleadoDto empleadoDto)
    {
        await _service.ActualizarEmpleado(id, empleadoDto);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> EliminarEmpleado(Guid id)
    {
        await _service.EliminarEmpleado(id);
        return NoContent();
    }
}