using ProyectoPractica.Application.Polizas.Dtos;

namespace ProyectoPractica.Application.Polizas.Commands.ActualizarCorrespondencia;

public sealed record ActualizarCorrespondenciaCommand(
    int PolizaId,
    string? Datos,
    string? Poblacion,
    string? Cp,
    string? Direccion,
    string? Numero,
    string? Aclarador) : IComandoBloquePoliza<CorrespondenciaDto>;
