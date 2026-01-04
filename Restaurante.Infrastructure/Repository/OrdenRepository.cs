using Restaurante.Model.Models;
using Restaurante.Model.Interface;
using Restaurante.Infrastructure.Context;

namespace Restaurante.Infrastructure.Repository;

public class OrdenRepository : GenericRepository<Orden>, IOrdenRepository
{
    public OrdenRepository(RestauranteDbContext context) : base(context)
    {
    }
}