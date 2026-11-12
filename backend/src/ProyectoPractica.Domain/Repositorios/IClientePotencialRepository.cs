using ProyectoPractica.Domain.ClientesPotenciales;

namespace ProyectoPractica.Domain.Repositorios;

/// <summary>Persistencia de clientes potenciales (lado de escritura).</summary>
public interface IClientePotencialRepository
{
    Task<ClientePotencial?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default);

    void Eliminar(ClientePotencial cliente);
}
