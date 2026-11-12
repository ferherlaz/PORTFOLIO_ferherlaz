using ProyectoPractica.Application.Polizas.Dtos;

namespace ProyectoPractica.Application.Polizas.Commands.ActualizarPago;

public sealed record ActualizarPagoCommand(
    int PolizaId,
    string? Nombre,
    string? Cif,
    string? TipoCobro,
    string? Iban,
    string? Banco) : IComandoBloquePoliza<PagoDto>;
