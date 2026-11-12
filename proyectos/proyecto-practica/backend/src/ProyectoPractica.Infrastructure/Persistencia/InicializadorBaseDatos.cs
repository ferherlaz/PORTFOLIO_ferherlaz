using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProyectoPractica.Infrastructure.Persistencia.Semilla;

namespace ProyectoPractica.Infrastructure.Persistencia;

public static class InicializadorBaseDatos
{
    /// <summary>Crea el esquema si no existe y carga la semilla cuando la base de datos está vacía.</summary>
    public static async Task InicializarBaseDatosAsync(this IServiceProvider servicios, CancellationToken cancellationToken = default)
    {
        await using var ambito = servicios.CreateAsyncScope();
        var contexto = ambito.ServiceProvider.GetRequiredService<AppDbContext>();
        var reloj = ambito.ServiceProvider.GetRequiredService<TimeProvider>();

        await contexto.Database.EnsureCreatedAsync(cancellationToken);
        if (await contexto.Polizas.AnyAsync(cancellationToken)) return;

        var hoy = DateOnly.FromDateTime(reloj.GetLocalNow().Date);
        contexto.Polizas.AddRange(SemillaDatos.GenerarPolizas(hoy));
        contexto.ClientesPotenciales.AddRange(SemillaDatos.GenerarClientes());
        await contexto.SaveChangesAsync(cancellationToken);
    }
}
