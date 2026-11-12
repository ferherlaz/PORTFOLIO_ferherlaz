using ProyectoPractica.Application.Polizas.Dtos;
using ProyectoPractica.Domain.Polizas;
using ProyectoPractica.Domain.Repositorios;

namespace ProyectoPractica.Application.Polizas.Commands.ActualizarTitular;

public sealed class ActualizarTitularCommandHandler(IPolizaRepository polizas, IUnitOfWork unidadDeTrabajo)
    : ActualizarBloquePolizaHandler<ActualizarTitularCommand, TitularDto>(polizas, unidadDeTrabajo)
{
    protected override TitularDto Aplicar(Poliza poliza, ActualizarTitularCommand command)
    {
        poliza.ActualizarTitular(Titular.Crear(command.Cif, command.Nombre, command.DirFiscal));
        return poliza.Titular.ADto();
    }
}
