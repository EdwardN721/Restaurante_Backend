using Microsoft.AspNetCore.Mvc;
using Restaurante.Business.Dtos.Peticion;
using Restaurante.Business.Dtos.Respuesta;
using Restaurante.Business.Interfaces;
using Restaurante.Model.Exceptions;

namespace Restaurante.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaService _service;
    private readonly ILogger<CategoriaController> _logger;

    public CategoriaController(ICategoriaService service, ILogger<CategoriaController> logger)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerCategorias()
    {
        IEnumerable<RespuestaCategoriaDto> categorias = await _service.ObtenerCategorias();
        
        _logger.LogInformation("Obtener categorias exitosamente.");
        return Ok(categorias);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObtenerCategoriaPorId(int id)
    {
        try
        {
            RespuestaCategoriaDto categoria = await _service.ObtenerCategoriaPorId(id);
            
            _logger.LogInformation("Obtener categoria Id {Id} exitosamente.", id);
            return Ok(categoria);
        }
        catch (NotFoundException ex)
        {
            _logger.LogError(ex, ex.Message);
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CrearCategoria([FromBody] PeticionCategoriaDto categoria)
    {
        _logger.LogInformation("Creando categoria.");
        RespuestaCategoriaDto categoriaNueva = await _service.AgregarCategoria(categoria);
        
        return Ok(categoriaNueva);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> ActualizarCategoria(int id, [FromBody] PeticionCategoriaDto categoria)
    {
        _logger.LogInformation("Actualizando categoria.");
        await _service.ActualizarCategoria(id, categoria);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> EliminarCategoria(int id)
    {
        _logger.LogWarning("Eliminando categoria.");
        await _service.EliminarCategoria(id);
        return NoContent();
    }
}