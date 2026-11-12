using Microsoft.EntityFrameworkCore;
using ProyectoPractica.Api.Domain;

namespace ProyectoPractica.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Poliza> Polizas => Set<Poliza>();
    public DbSet<Cuota> Cuotas => Set<Cuota>();
    public DbSet<ClientePotencial> ClientesPotenciales => Set<ClientePotencial>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Poliza>(poliza =>
        {
            poliza.HasIndex(p => p.NumPoliza).IsUnique();
            poliza.HasIndex(p => p.FechaVencimiento);
            poliza.Property(p => p.NombreRiesgo).HasMaxLength(120);
            poliza.Property(p => p.Estado).HasConversion<string>().HasMaxLength(20);

            poliza.OwnsOne(p => p.Titular);
            poliza.OwnsOne(p => p.Facturacion);
            poliza.OwnsOne(p => p.Pago);
            poliza.OwnsOne(p => p.Correspondencia);
            poliza.OwnsOne(p => p.Contacto);

            poliza.HasMany(p => p.Cuotas).WithOne().HasForeignKey(c => c.PolizaId);
        });

        modelBuilder.Entity<Cuota>()
            .Property(c => c.Estado).HasConversion<string>().HasMaxLength(20);

        modelBuilder.Entity<ClientePotencial>()
            .Property(c => c.Producto).HasConversion<string>().HasMaxLength(20);

        // SQLite no sabe ordenar ni comparar decimal: se guarda como REAL.
        if (Database.IsSqlite())
        {
            modelBuilder.Entity<Poliza>().Property(p => p.Importe).HasConversion<double>();
            modelBuilder.Entity<Cuota>().Property(c => c.Importe).HasConversion<double>();
        }
        else
        {
            modelBuilder.Entity<Poliza>().Property(p => p.Importe).HasPrecision(12, 2);
            modelBuilder.Entity<Cuota>().Property(c => c.Importe).HasPrecision(12, 2);
        }
    }
}
