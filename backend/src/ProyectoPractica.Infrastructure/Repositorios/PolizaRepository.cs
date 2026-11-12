using Microsoft.EntityFrameworkCore;
using ProyectoPractica.Domain.Polizas;
using ProyectoPractica.Domain.Repositorios;
using ProyectoPractica.Infrastructure.Persistencia;

namespace ProyectoPractica.Infrastructure.Repositorios;

internal sealed class PolizaRepository(AppDbContext contexto) : IPolizaRepository
{
    public Task<Poliza?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default) =>
        contexto.Polizas.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
}
