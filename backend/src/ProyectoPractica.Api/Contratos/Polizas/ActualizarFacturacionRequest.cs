using ProyectoPractica.Application.Polizas.Commands.ActualizarFacturacion;

namespace ProyectoPractica.Api.Contratos.Polizas;

/// <summary>Cuerpo de PUT /api/polizas/{id}/facturacion.</summary>
public sealed record ActualizarFacturacionRequest(
    string? DirFacturacion,
    string? Numero,
    string? Ciudad,
    string? Cp)
{
    public ActualizarFacturacionCommand ACommand(int polizaId) => new(polizaId, DirFacturacion, Numero, Ciudad, Cp);
}
