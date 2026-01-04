using System.Linq.Expressions;

namespace Restaurante.Model.Interface;

public interface IGenericRepository<T> where T : class
{
    Task<T?> ObtenerPorIdAsync(object id);
    Task<IEnumerable<T>> ObtenerTodosAsync();
    Task<IEnumerable<T>> ObtenerAsync(Expression<Func<T, bool>> predicate);
    Task<T> CrearAsync(T entity);
    void EliminarAsync(T entity);
}