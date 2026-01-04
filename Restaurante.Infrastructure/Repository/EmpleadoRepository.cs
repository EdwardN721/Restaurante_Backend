using Restaurante.Infrastructure.Context;
using Restaurante.Model.Interface;
using Restaurante.Model.Models;

namespace Restaurante.Infrastructure.Repository;

public class EmpleadoRepository : GenericRepository<Empleado>, IEmpleadoRepository
{
    public EmpleadoRepository(RestauranteDbContext context) : base(context)
    {
    }
}