using Microsoft.EntityFrameworkCore;
using ProyectoPractica.Domain.ClientesPotenciales;
using ProyectoPractica.Domain.Polizas;
using ProyectoPractica.Domain.Repositorios;

namespace ProyectoPractica.Infrastructure.Persistencia;

/// <summary>Contexto de EF Core. También es la unidad de trabajo que confirma los cambios de los repositorios.</summary>
public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Poliza> Polizas => Set<Poliza>();
    public DbSet<Cuota> Cuotas => Set<Cuota>();
    public DbSet<ClientePotencial> ClientesPotenciales => Set<ClientePotencial>();

    public Task<int> GuardarCambiosAsync(CancellationToken cancellationToken = default) =>
        SaveChangesAsync(cancellationToken);

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        // SQLite no sabe comparar ni ordenar decimal: se guarda como REAL. PostgreSQL usa numeric(12,2).
        if (Database.IsSqlite())
            configurationBuilder.Properties<decimal>().HaveConversion<double>();
        else
            configurationBuilder.Properties<decimal>().HavePrecision(12, 2);
    }
}
