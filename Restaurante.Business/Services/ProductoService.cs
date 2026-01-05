using Microsoft.Extensions.Logging;
using Restaurante.Business.Dtos.Peticion;
using Restaurante.Business.Dtos.Respuesta;
using Restaurante.Business.Interfaces;
using Restaurante.Business.Mappers;
using Restaurante.Model.Interface;
using Restaurante.Model.Models;

namespace Restaurante.Business.Services;

public class ProductoService : IProductoService 
{
    private readonly IProductoRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ProductoService> _logger;

    public ProductoService(IProductoRepository repository, IUnitOfWork unitOfWork, ILogger<ProductoService> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _unitOfWork = unitOfWork  ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<RespuestaProductoDto>> ObtenerProductos()
    {
        IEnumerable<Producto> productos = (await _repository.ObtenerTodosAsync()).ToList();
        
        _logger.LogInformation($"Total de productos: {productos.Count()}");
        return productos.ToDto();
    }

    public async Task<RespuestaProductoDto> ObtenerProductoPorId(Guid id)
    {
        Producto? producto = await _repository.ObtenerPorIdAsync(id);
        if (producto == null)
        {
            _logger.LogWarning("Producto con el {Id} no encontrado", id);
            throw new KeyNotFoundException($"Producto no encontrado con el Id {id}");
        } 
        
        _logger.LogInformation("Producto encotrado con el id: {Id}", id);
        return producto.ToDto();
    }

    public async Task<RespuestaProductoDto> CrearProducto(PeticionProductoDto producto)
    {
        Producto productoNuevo = producto.ToEntity();
        await _repository.CrearAsync(productoNuevo);
        await _unitOfWork.SaveChangesAsync(CancellationToken.None);

        _logger.LogInformation("Producto creado exitosamente con el Id {Id}", productoNuevo.Id);
        return productoNuevo.ToDto();
    }

    public async Task ActualizarProducto(Guid id, PeticionProductoDto producto)
    {
        Producto? productoEncontrado = await _repository.ObtenerPorIdAsync(id);
        if (productoEncontrado == null)
        {
            _logger.LogWarning("Producto con el {Id} no encontrado", id);
            throw new KeyNotFoundException($"Producto no encontrado con el Id {id}");
        } 
        
        productoEncontrado.UpdateFromDto(producto);
        await _unitOfWork.SaveChangesAsync(CancellationToken.None);
        
        _logger.LogInformation("Producto actualizado con el id: {Id}", id);
    }

    public async Task EliminarProducto(Guid id)
    {
        Producto? productoEncontrado = await _repository.ObtenerPorIdAsync(id);
        if (productoEncontrado == null)
        {
            _logger.LogWarning("Producto con el {Id} no encontrado", id);
            throw new KeyNotFoundException($"Producto no encontrado con el Id {id}");
        } 
        
        _repository.EliminarAsync(productoEncontrado);
        await _unitOfWork.SaveChangesAsync(CancellationToken.None);
        
        _logger.LogWarning("Producto eliminado con el id: {Id}", id);
    }
}