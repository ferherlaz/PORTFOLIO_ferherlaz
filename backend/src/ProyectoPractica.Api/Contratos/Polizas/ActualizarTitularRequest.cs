using ProyectoPractica.Application.Polizas.Commands.ActualizarTitular;

namespace ProyectoPractica.Api.Contratos.Polizas;

/// <summary>Cuerpo de PUT /api/polizas/{id}/titular.</summary>
public sealed record ActualizarTitularRequest(
    string? Cif,
    string? Nombre,
    string? DirFiscal)
{
    public ActualizarTitularCommand ACommand(int polizaId) => new(polizaId, Cif, Nombre, DirFiscal);
}
