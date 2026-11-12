using ProyectoPractica.Application.Polizas.Dtos;
using ProyectoPractica.Domain.Polizas;
using ProyectoPractica.Domain.Repositorios;

namespace ProyectoPractica.Application.Polizas.Commands.ActualizarCorrespondencia;

public sealed class ActualizarCorrespondenciaCommandHandler(IPolizaRepository polizas, IUnitOfWork unidadDeTrabajo)
    : ActualizarBloquePolizaHandler<ActualizarCorrespondenciaCommand, CorrespondenciaDto>(polizas, unidadDeTrabajo)
{
    protected override CorrespondenciaDto Aplicar(Poliza poliza, ActualizarCorrespondenciaCommand command)
    {
        poliza.ActualizarCorrespondencia(Correspondencia.Crear(
            command.Datos, command.Poblacion, command.Cp, command.Direccion, command.Numero, command.Aclarador));
        return poliza.Correspondencia.ADto();
    }
}
