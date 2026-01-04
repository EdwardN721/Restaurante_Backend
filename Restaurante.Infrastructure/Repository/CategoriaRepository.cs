using Restaurante.Model.Models;
using Restaurante.Infrastructure.Context;
using Restaurante.Model.Interface;

namespace Restaurante.Infrastructure.Repository;

public class CategoriaRepository : GenericRepository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(RestauranteDbContext context) : base(context)
    {
    }
}