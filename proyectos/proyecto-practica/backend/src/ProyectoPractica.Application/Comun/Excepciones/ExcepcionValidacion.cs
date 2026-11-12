namespace ProyectoPractica.Application.Comun.Excepciones;

/// <summary>Datos de entrada no válidos. Errores agrupados por campo (en camelCase, como en el JSON).</summary>
public sealed class ExcepcionValidacion(IReadOnlyDictionary<string, string[]> errores)
    : Exception("Uno o más campos no son válidos.")
{
    public IReadOnlyDictionary<string, string[]> Errores { get; } = errores;
}
