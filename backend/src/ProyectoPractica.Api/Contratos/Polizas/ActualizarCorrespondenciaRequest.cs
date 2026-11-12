using ProyectoPractica.Application.Polizas.Commands.ActualizarCorrespondencia;

namespace ProyectoPractica.Api.Contratos.Polizas;

/// <summary>Cuerpo de PUT /api/polizas/{id}/correspondencia.</summary>
public sealed record ActualizarCorrespondenciaRequest(
    string? Datos,
    string? Poblacion,
    string? Cp,
    string? Direccion,
    string? Numero,
    string? Aclarador)
{
    public ActualizarCorrespondenciaCommand ACommand(int polizaId) => new(polizaId, Datos, Poblacion, Cp, Direccion, Numero, Aclarador);
}
