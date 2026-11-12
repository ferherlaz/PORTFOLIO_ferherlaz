namespace ProyectoPractica.Domain.Comun;

/// <summary>Normalización de textos de entrada antes de guardarlos en el dominio.</summary>
public static class Texto
{
    /// <summary>Recorta espacios; null pasa a cadena vacía.</summary>
    public static string Limpio(string? valor) => (valor ?? string.Empty).Trim();

    /// <summary>Documentos e IBAN: sin espacios ni guiones y en mayúsculas.</summary>
    public static string Codigo(string? valor) =>
        (valor ?? string.Empty).Replace(" ", string.Empty).Replace("-", string.Empty).Trim().ToUpperInvariant();

    /// <summary>Correos: recortados y en minúsculas.</summary>
    public static string Correo(string? valor) => Limpio(valor).ToLowerInvariant();
}
