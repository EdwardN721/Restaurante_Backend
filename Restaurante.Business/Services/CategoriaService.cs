using Restaurante.Model.Models;
using Restaurante.Model.Interface;
using Microsoft.Extensions.Logging;
using Restaurante.Business.Mappers;
using Restaurante.Model.Exceptions;
using Restaurante.Business.Interfaces;
using Restaurante.Business.Dtos.Peticion;
using Restaurante.Business.Dtos.Respuesta;

namespace Restaurante.Business.Services;

public class CategoriaService : ICategoriaService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICategoriaRepository _repository;
    private readonly ILogger<CategoriaService> _logger;

    public CategoriaService(IUnitOfWork unitOfWork, ICategoriaRepository repository, ILogger<CategoriaService> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger;
    }

    public async Task<IEnumerable<RespuestaCategoriaDto>> ObtenerCategorias()
    {
        IEnumerable<Categoria> categorias = (await _repository.ObtenerTodosAsync()).ToList();

        _logger.LogInformation("Obtener Categorias: {Contador}", categorias.Count());
        return categorias.ToDto();
    }

    public async Task<RespuestaCategoriaDto> ObtenerCategoriaPorId(int id)
    {
        Categoria? categoria = await _repository.ObtenerPorIdAsync(id);

        if (categoria == null)
        {
            _logger.LogWarning("Categoria {Id} no encontrada.", id);
            throw new NotFoundException($"Categoria con Id: {id} no encontrada.");
        }
        
        return categoria.ToDto();
    }

    public async Task<RespuestaCategoriaDto> AgregarCategoria(PeticionCategoriaDto categoria)
    {
        Categoria nuevaCategoria = categoria.ToEntity();
        await _unitOfWork.Categorias.CrearAsync(nuevaCategoria);
        await _unitOfWork.SaveChangesAsync(CancellationToken.None);
        
        _logger.LogInformation("Categoria creada exitosamente. ID: {Id}, Nombre: {Nombre}", 
            nuevaCategoria.Id, nuevaCategoria.Nombre);
        return nuevaCategoria.ToDto();
    }

    public async Task ActualizarCategoria(int id, PeticionCategoriaDto dto)
    {
        Categoria? categoriaExistente = await _repository.ObtenerPorIdAsync(id);

        if (categoriaExistente == null)
        {
            _logger.LogWarning("Categoria {id} no encontrada.", id);
            throw new NotFoundException($"Categoria con Id: {id} no encontrada.");
        }
        
        categoriaExistente.UpdateFromDto(dto);

        _logger.LogInformation($"Actualizando Categoria: {categoriaExistente.Id}");
        await _unitOfWork.SaveChangesAsync(CancellationToken.None);
    }

    public async Task EliminarCategoria(int id)
    {
        _logger.LogWarning("Eliminando Categoria con el Id: {id}", id);
        Categoria? categoriaExistente = await _repository.ObtenerPorIdAsync(id);

        if (categoriaExistente == null)
        {
            _logger.LogWarning("Categoria {id} no encontrada.", id);
            throw new NotFoundException($"Categoria con Id: {id} no encontrada.");
        }
        
        
        _unitOfWork.Categorias.EliminarAsync(categoriaExistente);
        await _unitOfWork.SaveChangesAsync(CancellationToken.None);
        _logger.LogInformation("Categoria eliminada exitosamente.");
    }
}