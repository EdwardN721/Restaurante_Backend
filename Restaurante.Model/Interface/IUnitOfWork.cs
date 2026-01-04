namespace Restaurante.Model.Interface;

public interface IUnitOfWork : IDisposable
{
    ICategoriaRepository Categorias { get; }
    IProductoRepository Productos { get; }
    IDetalleOrdenRepository DetalleOrdenes { get; }
    IOrdenRepository Ordenes { get; }
    IMesaRepository Mesas { get; }
    IEmpleadoRepository Empleados { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}