using ProyectoPractica.Application.Polizas.Dtos;
using ProyectoPractica.Domain.Polizas;
using ProyectoPractica.Domain.Repositorios;

namespace ProyectoPractica.Application.Polizas.Commands.ActualizarFacturacion;

public sealed class ActualizarFacturacionCommandHandler(IPolizaRepository polizas, IUnitOfWork unidadDeTrabajo)
    : ActualizarBloquePolizaHandler<ActualizarFacturacionCommand, FacturacionDto>(polizas, unidadDeTrabajo)
{
    protected override FacturacionDto Aplicar(Poliza poliza, ActualizarFacturacionCommand command)
    {
        poliza.ActualizarFacturacion(Facturacion.Crear(command.DirFacturacion, command.Numero, command.Ciudad, command.Cp));
        return poliza.Facturacion.ADto();
    }
}
