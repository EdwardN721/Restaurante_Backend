using Restaurante.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Restaurante.Infrastructure.Context;

public class RestauranteDbContext : DbContext
{
    public RestauranteDbContext(DbContextOptions options) : base(options)
    { }
    
    DbSet<Categoria> Categorias { get; set; }
    DbSet<DetalleOrden> DetalleOrdenes { get; set; }
    DbSet<Empleado> Empleados { get; set; }
    DbSet<Mesa> Mesas { get; set; }
    DbSet<Producto> Productos { get; set; }
    DbSet<Orden> Ordenes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.ToTable("Categorias");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("CategoriaID");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("GETDATE()");
        });

        modelBuilder.Entity<DetalleOrden>(entity =>
        {
            entity.ToTable("DetalleOrden");
            entity.HasKey(d => d.Id);
            
            entity.Property(d => d.Id)
                .HasColumnName("DetalleID");
            entity.Property(d => d.ProductoId)
                .HasColumnName("ProductoID");
            entity.Property(d => d.OrdenId)
                .HasColumnName("OrdenID");
            entity.Property(d => d.SubTotal)
                .HasPrecision(18, 2);
            entity.Property(d => d.PrecioUnitario)
                .HasPrecision(18, 2);
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.ToTable("Empleados");
            entity.HasKey(e => e.Id);
            
            entity.Property(d => d.Id)
                .HasColumnName("EmpleadoID")
                .HasDefaultValueSql("NEWSEQUENTIALID()");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("GETDATE()");
        });

        modelBuilder.Entity<Mesa>(entity =>
            {
                entity.ToTable("Mesas");
                entity.HasKey(m => m.Id);
                
                entity.Property(d => d.Id)
                    .HasColumnName("MesaID");
                entity.Property(e => e.FechaRegistro)
                    .HasDefaultValueSql("GETDATE()");
            });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.ToTable("Productos");
            entity.HasKey(p => p.Id);
            
            entity.Property(d => d.Id)
                .HasColumnName("ProductoID")
                .HasDefaultValueSql("NEWSEQUENTIALID()");
            entity.Property(p => p.FechaRegistro)
                .HasDefaultValueSql("GETDATE()");
            entity.Property(p => p.Precio)
                .HasPrecision(18, 2);
        });

        modelBuilder.Entity<Orden>(entity =>
        {
            entity.ToTable("Ordenes");
            entity.HasKey(o => o.Id);
            
            entity.Property(d => d.Id)
                .HasColumnName("OrdenID");
            entity.Property(o => o.Total)
                .HasPrecision(18, 2);
            entity.Property(o => o.Estado)
                .HasConversion<string>()
                .HasMaxLength(20);
        });
    }
}
