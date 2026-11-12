using ProyectoPractica.Application.Polizas.Commands.ActualizarContacto;

namespace ProyectoPractica.Api.Contratos.Polizas;

/// <summary>Cuerpo de PUT /api/polizas/{id}/contacto.</summary>
public sealed record ActualizarContactoRequest(
    string? CorreoPrincipal,
    string? CorreoSecundario,
    string? Telefono)
{
    public ActualizarContactoCommand ACommand(int polizaId) => new(polizaId, CorreoPrincipal, CorreoSecundario, Telefono);
}
