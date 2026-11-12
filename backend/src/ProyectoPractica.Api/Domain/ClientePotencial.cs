namespace ProyectoPractica.Api.Domain;

public enum ProductoOfertado
{
    Moto,
    Casa
}

public class ClientePotencial
{
    public int Id { get; set; }
    public required string Nombre { get; set; }
    public ProductoOfertado Producto { get; set; }
    public required string Email { get; set; }
    public required string Telefono { get; set; }
}
