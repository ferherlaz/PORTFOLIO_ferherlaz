using ProyectoPractica.Application.Polizas.Commands.ActualizarPago;

namespace ProyectoPractica.Api.Contratos.Polizas;

/// <summary>Cuerpo de PUT /api/polizas/{id}/pago.</summary>
public sealed record ActualizarPagoRequest(
    string? Nombre,
    string? Cif,
    string? TipoCobro,
    string? Iban,
    string? Banco)
{
    public ActualizarPagoCommand ACommand(int polizaId) => new(polizaId, Nombre, Cif, TipoCobro, Iban, Banco);
}
