using Microsoft.EntityFrameworkCore;
using Restaurante.Business.Interfaces;
using Restaurante.Business.Services;
using Restaurante.Infrastructure.Context;
using Restaurante.Infrastructure.Persistence;
using Restaurante.Infrastructure.Repository;
using Restaurante.Model.Interface;

namespace Restaurante.API;

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
        
        // Registro de services
        services.AddScoped<ICategoriaService, CategoriaService>();
        services.AddScoped<IDetalleOrdenService, DetalleOrdenService>();
        services.AddScoped<IEmpleadoService, EmpleadoService>();
        services.AddScoped<IMesaService, MesaService>();
        services.AddScoped<IOrdenService, OrdenService>();
        services.AddScoped<IProductoService, ProductoService>();
        
        return services;
    }
}