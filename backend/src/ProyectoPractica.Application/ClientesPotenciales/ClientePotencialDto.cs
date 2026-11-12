using ProyectoPractica.Domain.ClientesPotenciales;

namespace ProyectoPractica.Application.ClientesPotenciales;

public sealed record ClientePotencialDto(int Id, string Nombre, ProductoOfertado Producto, string Email, string Telefono);
