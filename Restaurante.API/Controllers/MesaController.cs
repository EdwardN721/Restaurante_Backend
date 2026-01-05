using Microsoft.AspNetCore.Mvc;
using Restaurante.Business.Dtos.Peticion;
using Restaurante.Business.Dtos.Respuesta;
using Restaurante.Business.Interfaces;
using Restaurante.Model.Exceptions;

namespace Restaurante.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MesaController : ControllerBase
{
    private readonly ILogger<MesaController> _logger;
    private readonly IMesaService _service;

    public MesaController(ILogger<MesaController> logger, IMesaService service)
    {
        _logger = logger;
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }
    
    [HttpGet]
    public async Task<IActionResult> ObtenerMesas()
    {
        IEnumerable<RespuestaMesaDto> mesas = await _service.ObtenerMesas();
        return Ok(mesas); 
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObtenerMesaPorId(int id)
    {
        try
        {
            RespuestaMesaDto mesa = await _service.ObtenerMesaPorId(id);
            return Ok(mesa);
        }
        catch (NotFoundException exception)
        {
            _logger.LogError(exception, exception.Message);
            return NotFound(exception.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AgregarMesa([FromBody] PeticionMesaDto mesaDto)
    {
        RespuestaMesaDto mesa = await _service.IngresarMesa(mesaDto);
        return Ok(mesa);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> AcualizarMesa(int id, [FromBody] PeticionMesaDto mesaDto)
    {
        await _service.ActualizarMesa(id, mesaDto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> EliminarMesa(int id)
    {
        await _service.EliminarMesa(id);
        return NoContent();
    }
}