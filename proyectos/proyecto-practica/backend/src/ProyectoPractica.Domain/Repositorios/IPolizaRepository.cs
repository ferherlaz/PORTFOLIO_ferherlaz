using ProyectoPractica.Domain.Polizas;

namespace ProyectoPractica.Domain.Repositorios;

/// <summary>Persistencia del agregado Póliza (lado de escritura).</summary>
public interface IPolizaRepository
{
    Task<Poliza?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default);
}
