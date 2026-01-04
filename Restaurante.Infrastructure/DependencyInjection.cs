using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurante.Infrastructure.Context;
using Restaurante.Infrastructure.Persistence;
using Restaurante.Infrastructure.Repository;
using Restaurante.Model.Interface;

namespace Restaurante.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RestauranteDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        // Registro de Repositorios y UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        // Registros Especificos
        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<IDetalleOrdenRepository, DetalleOrdenRepository>();
        services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();
        services.AddScoped<IMesaRepository, MesaRepository>();
        services.AddScoped<IOrdenRepository, OrdenRepository>();
        services.AddScoped<IProductoRepository, ProductoRepository>();
        
        return services;
    }
}