using Restaurante.Model.Models;
using Restaurante.Model.Interface;
using Restaurante.Infrastructure.Context;

namespace Restaurante.Infrastructure.Repository;

public class DetalleOrdenRepository : GenericRepository<DetalleOrden>, IDetalleOrdenRepository
{
    public DetalleOrdenRepository(RestauranteDbContext context) : base(context)
    {
    }
}