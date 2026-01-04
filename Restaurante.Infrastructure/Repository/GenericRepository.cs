using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Restaurante.Infrastructure.Context;
using Restaurante.Model.Interface;

namespace Restaurante.Infrastructure.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly RestauranteDbContext Context;
    protected readonly DbSet<T> DbSet;

    public GenericRepository(RestauranteDbContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }

    public async Task<T?> ObtenerPorIdAsync(object id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> ObtenerTodosAsync()
    {
        return await DbSet.ToListAsync();
    }

    public async Task<IEnumerable<T>> ObtenerAsync(Expression<Func<T, bool>> predicate)
    {
        return await DbSet
            .Where(predicate)
            .ToListAsync();
    }

    public async Task<T> CrearAsync(T entity)
    {
        await DbSet.AddAsync(entity);
        return entity;
    }

    public void EliminarAsync(T entity)
    {
        DbSet.Remove(entity);
    }
}