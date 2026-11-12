using ProyectoPractica.Domain.Polizas;

namespace ProyectoPractica.Application.Polizas.Dtos;

public sealed record CuotaDto(
    int Id,
    DateOnly FechaEmision,
    DateOnly PeriodoDesde,
    DateOnly PeriodoHasta,
    decimal Importe,
    EstadoPoliza Estado);
