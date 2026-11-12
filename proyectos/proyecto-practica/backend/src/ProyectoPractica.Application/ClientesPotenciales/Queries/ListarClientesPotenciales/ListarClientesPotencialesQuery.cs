using ProyectoPractica.Application.Abstracciones.Mensajeria;

namespace ProyectoPractica.Application.ClientesPotenciales.Queries.ListarClientesPotenciales;

public sealed record ListarClientesPotencialesQuery : IQuery<IReadOnlyList<ClientePotencialDto>>;
