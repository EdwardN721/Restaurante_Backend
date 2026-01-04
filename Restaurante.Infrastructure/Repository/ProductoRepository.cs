using Restaurante.Model.Models;
using Restaurante.Model.Interface;
using Restaurante.Infrastructure.Context;

namespace Restaurante.Infrastructure.Repository;

public class ProductoRepository : GenericRepository<Producto>, IProductoRepository
{
    public ProductoRepository(RestauranteDbContext context) : base(context)
    {
    }
}