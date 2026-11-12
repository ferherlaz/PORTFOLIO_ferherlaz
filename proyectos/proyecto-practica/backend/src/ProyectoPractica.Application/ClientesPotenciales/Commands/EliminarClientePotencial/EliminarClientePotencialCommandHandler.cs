using ProyectoPractica.Application.Abstracciones.Mensajeria;
using ProyectoPractica.Application.Comun.Excepciones;
using ProyectoPractica.Domain.Repositorios;

namespace ProyectoPractica.Application.ClientesPotenciales.Commands.EliminarClientePotencial;

public sealed class EliminarClientePotencialCommandHandler(IClientePotencialRepository clientes, IUnitOfWork unidadDeTrabajo)
    : ICommandHandler<EliminarClientePotencialCommand, Unidad>
{
    public async Task<Unidad> ManejarAsync(EliminarClientePotencialCommand command, CancellationToken cancellationToken)
    {
        var cliente = await clientes.ObtenerPorIdAsync(command.Id, cancellationToken)
            ?? throw new ExcepcionNoEncontrado("ningún cliente potencial", command.Id);

        clientes.Eliminar(cliente);
        await unidadDeTrabajo.GuardarCambiosAsync(cancellationToken);
        return Unidad.Valor;
    }
}
