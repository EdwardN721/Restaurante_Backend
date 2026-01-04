using Restaurante.Infrastructure.Context;
using Restaurante.Model.Interface;
using Restaurante.Model.Models;

namespace Restaurante.Infrastructure.Repository;

public class MesaRepository : GenericRepository<Mesa>, IMesaRepository
{
    public MesaRepository(RestauranteDbContext context) : base(context)
    {
    }
}