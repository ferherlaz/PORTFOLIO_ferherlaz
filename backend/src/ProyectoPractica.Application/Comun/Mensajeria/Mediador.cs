using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using ProyectoPractica.Application.Abstracciones.Mensajeria;

namespace ProyectoPractica.Application.Comun.Mensajeria;

/// <summary>
/// Mediador ligero (sin dependencias de terceros): resuelve el manejador de cada petición desde el
/// contenedor de dependencias y lo envuelve con los comportamientos registrados (validación, etc.).
/// </summary>
internal sealed class Mediador(IServiceProvider proveedor) : IMediador
{
    private static readonly ConcurrentDictionary<Type, object> Envoltorios = new();

    public Task<TRespuesta> EnviarAsync<TRespuesta>(IPeticion<TRespuesta> peticion, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(peticion);

        // El tipo concreto solo se conoce en tiempo de ejecución: se crea (y cachea) un envoltorio genérico cerrado.
        var envoltorio = (EnvoltorioPeticion<TRespuesta>)Envoltorios.GetOrAdd(
            peticion.GetType(),
            tipo => Activator.CreateInstance(typeof(EnvoltorioPeticion<,>).MakeGenericType(tipo, typeof(TRespuesta)))!);

        return envoltorio.ManejarAsync(peticion, proveedor, cancellationToken);
    }
}

internal abstract class EnvoltorioPeticion<TRespuesta>
{
    public abstract Task<TRespuesta> ManejarAsync(IPeticion<TRespuesta> peticion, IServiceProvider proveedor, CancellationToken cancellationToken);
}

internal sealed class EnvoltorioPeticion<TPeticion, TRespuesta> : EnvoltorioPeticion<TRespuesta>
    where TPeticion : IPeticion<TRespuesta>
{
    public override Task<TRespuesta> ManejarAsync(IPeticion<TRespuesta> peticion, IServiceProvider proveedor, CancellationToken cancellationToken)
    {
        var tipada = (TPeticion)peticion;
        var manejador = proveedor.GetRequiredService<IManejadorPeticion<TPeticion, TRespuesta>>();

        SiguientePaso<TRespuesta> paso = () => manejador.ManejarAsync(tipada, cancellationToken);

        // El primer comportamiento registrado es el más externo.
        foreach (var comportamiento in proveedor.GetServices<IComportamientoPeticion<TPeticion, TRespuesta>>().Reverse())
        {
            var siguiente = paso;
            paso = () => comportamiento.ManejarAsync(tipada, siguiente, cancellationToken);
        }

        return paso();
    }
}
