using ProyectoPractica.Domain.Comun;

namespace ProyectoPractica.Domain.ClientesPotenciales;

/// <summary>Persona a la que se le puede ofrecer un seguro.</summary>
public sealed class ClientePotencial : Entidad
{
    private ClientePotencial()
    {
        // Constructor para EF Core.
        Nombre = null!;
        Email = null!;
        Telefono = null!;
    }

    public ClientePotencial(string nombre, ProductoOfertado producto, string email, string telefono)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ExcepcionDominio("El nombre del cliente potencial es obligatorio.");

        Nombre = Texto.Limpio(nombre);
        Producto = producto;
        Email = Texto.Correo(email);
        Telefono = Texto.Limpio(telefono);
    }

    public string Nombre { get; private set; }
    public ProductoOfertado Producto { get; private set; }
    public string Email { get; private set; }
    public string Telefono { get; private set; }
}
