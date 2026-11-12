using ProyectoPractica.Domain.Polizas;

namespace ProyectoPractica.Application.Renovaciones;

public sealed record RenovacionDto(
    int Id,
    long NumPoliza,
    string NombreRiesgo,
    DateOnly FechaContrato,
    DateOnly FechaVencimiento,
    decimal Importe,
    EstadoPoliza Estado);
