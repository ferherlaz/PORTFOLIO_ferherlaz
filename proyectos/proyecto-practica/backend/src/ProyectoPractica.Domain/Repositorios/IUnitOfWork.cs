namespace ProyectoPractica.Domain.Repositorios;

/// <summary>Confirma en una sola transacción los cambios hechos a través de los repositorios.</summary>
public interface IUnitOfWork
{
    Task<int> GuardarCambiosAsync(CancellationToken cancellationToken = default);
}
