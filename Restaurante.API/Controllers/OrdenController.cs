using Microsoft.AspNetCore.Mvc;
using Restaurante.Business.Dtos.Peticion;
using Restaurante.Business.Dtos.Respuesta;
using Restaurante.Business.Interfaces;
using Restaurante.Model.Exceptions;

namespace Restaurante.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdenController : ControllerBase
{
    private readonly IOrdenService _service;
    private readonly ILogger<OrdenController> _logger;

    public OrdenController(IOrdenService service, ILogger<OrdenController> logger)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<IActionResult> ObtenerOrdenes()
    {
        IEnumerable<RespuestaOrdenDto> ordenes = await _service.ObtenerOrdenes();
        return Ok(ordenes); 
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObtenerOrdenPorId(int id)
    {
        try
        {
            RespuestaOrdenDto orden = await _service.ObtenerOrdenPorId(id);
            return Ok(orden);
        }
        catch (NotFoundException exception)
        {
            _logger.LogError(exception, exception.Message);
            return NotFound(exception.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AgregarOrden([FromBody] PeticionOrdenDto ordenDto)
    {
        RespuestaOrdenDto orden = await _service.CrearOrden(ordenDto);
        return Ok(orden);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> AcualizarOrden(int id, [FromBody] PeticionOrdenDto ordenDto)
    {
        await _service.ActualizarOrden(id, ordenDto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> EliminarOrden(int id)
    {
        await _service.EliminarOrden(id);
        return NoContent();
    }
}