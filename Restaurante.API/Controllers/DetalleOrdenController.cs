using Microsoft.AspNetCore.Mvc;
using Restaurante.Business.Dtos.Peticion;
using Restaurante.Business.Dtos.Respuesta;
using Restaurante.Business.Interfaces;
using Restaurante.Model.Exceptions;

namespace Restaurante.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DetalleOrdenController : ControllerBase
{
    private readonly ILogger<DetalleOrdenController> _logger;
    private readonly IDetalleOrdenService _service;

    public DetalleOrdenController(ILogger<DetalleOrdenController> logger, IDetalleOrdenService service)
    {
        _logger = logger;
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }
    
    [HttpGet]
    public async Task<IActionResult> ObtenerDetalleOrdenes()
    {
        IEnumerable<RespuestaDetalleOrdenDto> detalleOrdenes = await _service.ObtenerDetalleOrdenes();
        return Ok(detalleOrdenes); 
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObtenerDetalleOrdenPorId(int id)
    {
        try
        {
            RespuestaDetalleOrdenDto detalleOrden = await _service.ObtenerDetalleOrdenPorId(id);
            return Ok(detalleOrden);
        }
        catch (NotFoundException exception)
        {
            _logger.LogError(exception, exception.Message);
            return NotFound(exception.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AgregarDetalleOrden([FromBody] PeticionDetalleOrdenDto detalleOrdenDto)
    {
        RespuestaDetalleOrdenDto detalleOrden = await _service.AgregarDetalleOrden(detalleOrdenDto);
        return Ok(detalleOrden);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> AcualizarDetalleOrden(int id, [FromBody] PeticionDetalleOrdenDto detalleOrdenDto)
    {
        await _service.AcualizarDetalleOrden(id, detalleOrdenDto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> EliminarDetalleOrden(int id)
    {
        await _service.EliminarDetalleOrden(id);
        return NoContent();
    }
}