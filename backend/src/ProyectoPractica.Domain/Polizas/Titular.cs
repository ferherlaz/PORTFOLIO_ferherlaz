using ProyectoPractica.Domain.Comun;

namespace ProyectoPractica.Domain.Polizas;

/// <summary>Titular de la póliza.</summary>
public sealed record Titular
{
    private Titular(string cif, string nombre, string dirFiscal)
    {
        Cif = cif;
        Nombre = nombre;
        DirFiscal = dirFiscal;
    }

    public string Cif { get; }
    public string Nombre { get; }
    public string DirFiscal { get; }

    public static Titular Crear(string? cif, string? nombre, string? dirFiscal) =>
        new(Texto.Codigo(cif), Texto.Limpio(nombre), Texto.Limpio(dirFiscal));
}
