using Restaurante.Model.Interface;
using Restaurante.Infrastructure.Context;
using Microsoft.EntityFrameworkCore.Storage;
using Restaurante.Infrastructure.Repository;

namespace Restaurante.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly RestauranteDbContext _context;
    private IDbContextTransaction? _currentTransaction;
    
    private ICategoriaRepository? _categorias;
    private IProductoRepository? _productos;
    private IMesaRepository? _mesas;
    private IEmpleadoRepository? _empleados;
    private IOrdenRepository? _ordenes;
    private IDetalleOrdenRepository? _detalleOrdenes;
    
    public UnitOfWork(RestauranteDbContext context)
    {
        _context = context;
    }

    // Lazy Loading: Solo se instancia si alguien lo pide
    public ICategoriaRepository Categorias
    {
        get
        {
            return _categorias ??= new CategoriaRepository(_context);
        }
    }

    public IProductoRepository Productos
    {
        get
        {
            return _productos ??= new ProductoRepository(_context);
        }
    }

    public IMesaRepository Mesas
    {
        get
        {
            return _mesas ??= new MesaRepository(_context);
        }
    }

    public IEmpleadoRepository Empleados
    {
        get
        {
            return _empleados ??= new EmpleadoRepository(_context);
        }
    }

    public IOrdenRepository Ordenes
    {
        get
        {
            return _ordenes ??= new OrdenRepository(_context);
        }
    }

    public IDetalleOrdenRepository DetalleOrdenes
    {
        get
        {
            return _detalleOrdenes ??= new DetalleOrdenRepository(_context);
        }
    }

    public void Dispose()
    {
        _currentTransaction?.Dispose();
        GC.SuppressFinalize(this);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken  = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync()
    {
        if (_currentTransaction != null)
        {
            return;
        }
        _currentTransaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await SaveChangesAsync();
            if (_currentTransaction != null)
            {
                await _currentTransaction.CommitAsync();
            }
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync()
    {
        try
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync();
            }
        }
        finally
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }
}