using ProyectoPractica.Application.Polizas.Dtos;
using ProyectoPractica.Domain.Polizas;
using ProyectoPractica.Domain.Repositorios;

namespace ProyectoPractica.Application.Polizas.Commands.ActualizarContacto;

public sealed class ActualizarContactoCommandHandler(IPolizaRepository polizas, IUnitOfWork unidadDeTrabajo)
    : ActualizarBloquePolizaHandler<ActualizarContactoCommand, ContactoDto>(polizas, unidadDeTrabajo)
{
    protected override ContactoDto Aplicar(Poliza poliza, ActualizarContactoCommand command)
    {
        poliza.ActualizarContacto(Contacto.Crear(command.CorreoPrincipal, command.CorreoSecundario, command.Telefono));
        return poliza.Contacto.ADto();
    }
}
