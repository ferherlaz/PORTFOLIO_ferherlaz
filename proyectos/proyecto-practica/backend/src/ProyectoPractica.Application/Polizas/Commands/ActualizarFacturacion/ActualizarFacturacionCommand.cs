using ProyectoPractica.Application.Polizas.Dtos;

namespace ProyectoPractica.Application.Polizas.Commands.ActualizarFacturacion;

public sealed record ActualizarFacturacionCommand(
    int PolizaId,
    string? DirFacturacion,
    string? Numero,
    string? Ciudad,
    string? Cp) : IComandoBloquePoliza<FacturacionDto>;
