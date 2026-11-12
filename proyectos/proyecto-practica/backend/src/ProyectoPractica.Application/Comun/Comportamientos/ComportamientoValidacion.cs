using System.Text.Json;
using FluentValidation;
using ProyectoPractica.Application.Abstracciones.Mensajeria;
using ProyectoPractica.Application.Comun.Excepciones;

namespace ProyectoPractica.Application.Comun.Comportamientos;

/// <summary>
/// Ejecuta los validadores de la petición antes del manejador. Si hay errores, el caso de uso
/// no llega a ejecutarse y se lanza <see cref="ExcepcionValidacion"/> (la API la traduce a 400).
/// </summary>
internal sealed class ComportamientoValidacion<TPeticion, TRespuesta>(IEnumerable<IValidator<TPeticion>> validadores)
    : IComportamientoPeticion<TPeticion, TRespuesta>
    where TPeticion : IPeticion<TRespuesta>
{
    public async Task<TRespuesta> ManejarAsync(TPeticion peticion, SiguientePaso<TRespuesta> siguiente, CancellationToken cancellationToken)
    {
        if (!validadores.Any()) return await siguiente();

        var contexto = new ValidationContext<TPeticion>(peticion);
        var resultados = await Task.WhenAll(validadores.Select(v => v.ValidateAsync(contexto, cancellationToken)));

        var errores = resultados
            .SelectMany(r => r.Errors)
            .Where(e => e is not null)
            .GroupBy(e => JsonNamingPolicy.CamelCase.ConvertName(e.PropertyName))
            .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).Distinct().ToArray());

        if (errores.Count > 0) throw new ExcepcionValidacion(errores);

        return await siguiente();
    }
}
