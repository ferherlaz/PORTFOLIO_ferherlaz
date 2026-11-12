using ProyectoPractica.Domain.Comun;

namespace ProyectoPractica.Domain.Polizas;

/// <summary>Dirección de correspondencia.</summary>
public sealed record Correspondencia
{
    private Correspondencia(string datos, string poblacion, string cp, string direccion, string numero, string aclarador)
    {
        Datos = datos;
        Poblacion = poblacion;
        Cp = cp;
        Direccion = direccion;
        Numero = numero;
        Aclarador = aclarador;
    }

    public string Datos { get; }
    public string Poblacion { get; }
    public string Cp { get; }
    public string Direccion { get; }
    public string Numero { get; }
    public string Aclarador { get; }

    public static Correspondencia Crear(
        string? datos, string? poblacion, string? cp, string? direccion, string? numero, string? aclarador) =>
        new(Texto.Limpio(datos), Texto.Limpio(poblacion), Texto.Limpio(cp),
            Texto.Limpio(direccion), Texto.Limpio(numero), Texto.Limpio(aclarador));
}
