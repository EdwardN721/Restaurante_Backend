using Microsoft.AspNetCore.Mvc;
using Restaurante.Business.Dtos.Peticion;
using Restaurante.Business.Dtos.Respuesta;
using Restaurante.Business.Interfaces;
using Restaurante.Model.Exceptions;

namespace Restaurante.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductoController : ControllerBase
{
    private readonly ILogger<ProductoController> _logger;
    private readonly IProductoService _service;

    public ProductoController(ILogger<ProductoController> logger, IProductoService service)
    {
        _logger = logger;
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerProductos()
    {
        IEnumerable<RespuestaProductoDto> productos = await _service.ObtenerProductos();
        return Ok(productos); 
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObtenerProductosPorId(Guid id)
    {
        try
        {
            RespuestaProductoDto producto = await _service.ObtenerProductoPorId(id);
            return Ok(producto);
        }
        catch (NotFoundException exception)
        {
            _logger.LogError(exception, exception.Message);
            return NotFound(exception.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AgregarProducto([FromBody] PeticionProductoDto producto)
    {
        RespuestaProductoDto productoDto = await _service.CrearProducto(producto);
        return Ok(productoDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> AcualizarProducto(Guid id, [FromBody] PeticionProductoDto producto)
    {
        await _service.ActualizarProducto(id, producto);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> EliminarProducto(Guid id)
    {
        await _service.EliminarProducto(id);
        return NoContent();
    }
}