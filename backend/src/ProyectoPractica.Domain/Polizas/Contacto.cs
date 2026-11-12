using ProyectoPractica.Domain.Comun;

namespace ProyectoPractica.Domain.Polizas;

/// <summary>Datos de contacto.</summary>
public sealed record Contacto
{
    private Contacto(string correoPrincipal, string correoSecundario, string telefono)
    {
        CorreoPrincipal = correoPrincipal;
        CorreoSecundario = correoSecundario;
        Telefono = telefono;
    }

    public string CorreoPrincipal { get; }
    public string CorreoSecundario { get; }
    public string Telefono { get; }

    public static Contacto Crear(string? correoPrincipal, string? correoSecundario, string? telefono) =>
        new(Texto.Correo(correoPrincipal), Texto.Correo(correoSecundario), Texto.Limpio(telefono));
}
