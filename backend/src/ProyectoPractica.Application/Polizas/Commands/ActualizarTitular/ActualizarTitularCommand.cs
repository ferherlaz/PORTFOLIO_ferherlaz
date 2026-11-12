using ProyectoPractica.Application.Polizas.Dtos;

namespace ProyectoPractica.Application.Polizas.Commands.ActualizarTitular;

public sealed record ActualizarTitularCommand(
    int PolizaId,
    string? Cif,
    string? Nombre,
    string? DirFiscal) : IComandoBloquePoliza<TitularDto>;
