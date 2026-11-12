using ProyectoPractica.Application.Polizas.Dtos;
using ProyectoPractica.Domain.Polizas;
using ProyectoPractica.Domain.Repositorios;

namespace ProyectoPractica.Application.Polizas.Commands.ActualizarPago;

public sealed class ActualizarPagoCommandHandler(IPolizaRepository polizas, IUnitOfWork unidadDeTrabajo)
    : ActualizarBloquePolizaHandler<ActualizarPagoCommand, PagoDto>(polizas, unidadDeTrabajo)
{
    protected override PagoDto Aplicar(Poliza poliza, ActualizarPagoCommand command)
    {
        poliza.ActualizarPago(Pago.Crear(command.Nombre, command.Cif, command.TipoCobro, command.Iban, command.Banco));
        return poliza.Pago.ADto();
    }
}
