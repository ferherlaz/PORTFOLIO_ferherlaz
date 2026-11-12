using ProyectoPractica.Application.Polizas.Dtos;

namespace ProyectoPractica.Application.Polizas.Commands.ActualizarContacto;

public sealed record ActualizarContactoCommand(
    int PolizaId,
    string? CorreoPrincipal,
    string? CorreoSecundario,
    string? Telefono) : IComandoBloquePoliza<ContactoDto>;
