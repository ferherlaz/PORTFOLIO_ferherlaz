using ProyectoPractica.Domain.Comun;

namespace ProyectoPractica.Domain.Polizas;

/// <summary>Método de pago (pagador y cuenta de cargo).</summary>
public sealed record Pago
{
    private Pago(string nombre, string cif, string tipoCobro, string iban, string banco)
    {
        Nombre = nombre;
        Cif = cif;
        TipoCobro = tipoCobro;
        Iban = iban;
        Banco = banco;
    }

    public string Nombre { get; }
    public string Cif { get; }
    public string TipoCobro { get; }
    public string Iban { get; }
    public string Banco { get; }

    public static Pago Crear(string? nombre, string? cif, string? tipoCobro, string? iban, string? banco) =>
        new(Texto.Limpio(nombre), Texto.Codigo(cif), Texto.Limpio(tipoCobro), Texto.Codigo(iban), Texto.Limpio(banco));
}
