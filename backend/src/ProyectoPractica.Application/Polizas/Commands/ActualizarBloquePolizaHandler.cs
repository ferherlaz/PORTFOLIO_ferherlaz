using ProyectoPractica.Application.Abstracciones.Mensajeria;
using ProyectoPractica.Application.Comun.Excepciones;
using ProyectoPractica.Domain.Polizas;
using ProyectoPractica.Domain.Repositorios;

namespace ProyectoPractica.Application.Polizas.Commands;

/// <summary>
/// Flujo común a los cinco comandos de actualización (patrón template method):
/// cargar el agregado, aplicar el cambio en el dominio, confirmar y devolver el bloque guardado.
/// Cada manejador concreto solo decide qué bloque cambia.
/// </summary>
public abstract class ActualizarBloquePolizaHandler<TCommand, TRespuesta>(IPolizaRepository polizas, IUnitOfWork unidadDeTrabajo)
    : ICommandHandler<TCommand, TRespuesta>
    where TCommand : IComandoBloquePoliza<TRespuesta>
{
    public async Task<TRespuesta> ManejarAsync(TCommand command, CancellationToken cancellationToken)
    {
        var poliza = await polizas.ObtenerPorIdAsync(command.PolizaId, cancellationToken)
            ?? throw new ExcepcionNoEncontrado("ninguna póliza", command.PolizaId);

        var respuesta = Aplicar(poliza, command);
        await unidadDeTrabajo.GuardarCambiosAsync(cancellationToken);
        return respuesta;
    }

    /// <summary>Aplica el cambio sobre la póliza y devuelve el bloque tal y como queda (normalizado).</summary>
    protected abstract TRespuesta Aplicar(Poliza poliza, TCommand command);
}
