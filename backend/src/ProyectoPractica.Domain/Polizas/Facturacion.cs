using ProyectoPractica.Domain.Comun;

namespace ProyectoPractica.Domain.Polizas;

/// <summary>Dirección de facturación.</summary>
public sealed record Facturacion
{
    private Facturacion(string dirFacturacion, string numero, string ciudad, string cp)
    {
        DirFacturacion = dirFacturacion;
        Numero = numero;
        Ciudad = ciudad;
        Cp = cp;
    }

    public string DirFacturacion { get; }
    public string Numero { get; }
    public string Ciudad { get; }
    public string Cp { get; }

    public static Facturacion Crear(string? dirFacturacion, string? numero, string? ciudad, string? cp) =>
        new(Texto.Limpio(dirFacturacion), Texto.Limpio(numero), Texto.Limpio(ciudad), Texto.Limpio(cp));
}
