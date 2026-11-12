using ProyectoPractica.Application.ClientesPotenciales;

namespace ProyectoPractica.Application.Abstracciones.Lectura;

/// <summary>Lado de lectura (CQRS) de los clientes potenciales.</summary>
public interface IClientesPotencialesLectura
{
    Task<IReadOnlyList<ClientePotencialDto>> ListarAsync(CancellationToken cancellationToken);
}
