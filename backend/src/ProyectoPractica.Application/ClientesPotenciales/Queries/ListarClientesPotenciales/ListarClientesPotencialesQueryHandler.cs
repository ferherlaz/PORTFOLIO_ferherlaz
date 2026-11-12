using ProyectoPractica.Application.Abstracciones.Lectura;
using ProyectoPractica.Application.Abstracciones.Mensajeria;

namespace ProyectoPractica.Application.ClientesPotenciales.Queries.ListarClientesPotenciales;

public sealed class ListarClientesPotencialesQueryHandler(IClientesPotencialesLectura lectura)
    : IQueryHandler<ListarClientesPotencialesQuery, IReadOnlyList<ClientePotencialDto>>
{
    public Task<IReadOnlyList<ClientePotencialDto>> ManejarAsync(ListarClientesPotencialesQuery query, CancellationToken cancellationToken) =>
        lectura.ListarAsync(cancellationToken);
}
