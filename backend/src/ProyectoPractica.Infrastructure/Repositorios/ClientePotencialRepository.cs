using Microsoft.EntityFrameworkCore;
using ProyectoPractica.Domain.ClientesPotenciales;
using ProyectoPractica.Domain.Repositorios;
using ProyectoPractica.Infrastructure.Persistencia;

namespace ProyectoPractica.Infrastructure.Repositorios;

internal sealed class ClientePotencialRepository(AppDbContext contexto) : IClientePotencialRepository
{
    public Task<ClientePotencial?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default) =>
        contexto.ClientesPotenciales.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    public void Eliminar(ClientePotencial cliente) => contexto.ClientesPotenciales.Remove(cliente);
}
