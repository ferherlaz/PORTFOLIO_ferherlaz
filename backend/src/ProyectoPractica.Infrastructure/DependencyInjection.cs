using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProyectoPractica.Application.Abstracciones.Lectura;
using ProyectoPractica.Domain.Repositorios;
using ProyectoPractica.Infrastructure.Lectura;
using ProyectoPractica.Infrastructure.Persistencia;
using ProyectoPractica.Infrastructure.Repositorios;

namespace ProyectoPractica.Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// Registra la persistencia: PostgreSQL si existe la cadena "Postgres"; si no, SQLite (sin configuración).
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuracion)
    {
        var postgres = configuracion.GetConnectionString("Postgres");
        var sqlite = configuracion.GetConnectionString("Sqlite") ?? "Data Source=proyecto-practica.db";

        services.AddDbContext<AppDbContext>(opciones =>
        {
            if (!string.IsNullOrWhiteSpace(postgres))
                opciones.UseNpgsql(postgres);
            else
                opciones.UseSqlite(sqlite);
        });

        services.TryAddSingleton(TimeProvider.System);

        // Escritura (comandos): repositorios + unidad de trabajo.
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AppDbContext>());
        services.AddScoped<IPolizaRepository, PolizaRepository>();
        services.AddScoped<IClientePotencialRepository, ClientePotencialRepository>();

        // Lectura (queries): consultas proyectadas a DTO.
        services.AddScoped<IRenovacionesLectura, RenovacionesLectura>();
        services.AddScoped<IPolizasLectura, PolizasLectura>();
        services.AddScoped<IClientesPotencialesLectura, ClientesPotencialesLectura>();

        return services;
    }
}
