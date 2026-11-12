namespace ProyectoPractica.Application.Renovaciones;

/// <summary>Traduce el parámetro "orden" de la URL (p. ej. "importe_desc") a <see cref="OrdenRenovaciones"/>.</summary>
public static class ParametroOrden
{
    public const string PorDefecto = "vencimiento_asc";

    private static readonly Dictionary<string, OrdenRenovaciones> Valores = new(StringComparer.OrdinalIgnoreCase)
    {
        ["vencimiento_asc"] = OrdenRenovaciones.VencimientoAscendente,
        ["vencimiento_desc"] = OrdenRenovaciones.VencimientoDescendente,
        ["importe_asc"] = OrdenRenovaciones.ImporteAscendente,
        ["importe_desc"] = OrdenRenovaciones.ImporteDescendente,
    };

    public static IEnumerable<string> Admitidos => Valores.Keys;

    public static bool TryParse(string? valor, out OrdenRenovaciones orden) =>
        Valores.TryGetValue(string.IsNullOrWhiteSpace(valor) ? PorDefecto : valor.Trim(), out orden);
}
