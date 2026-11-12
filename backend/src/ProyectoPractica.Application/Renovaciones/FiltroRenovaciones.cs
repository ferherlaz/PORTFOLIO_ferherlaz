using ProyectoPractica.Domain.Polizas;

namespace ProyectoPractica.Application.Renovaciones;

/// <summary>Criterios ya validados y tipados con los que se consulta el listado de renovaciones.</summary>
public sealed record FiltroRenovaciones(
    string? Texto,
    EstadoPoliza? Estado,
    DateOnly? Desde,
    DateOnly? Hasta,
    decimal? ImporteMin,
    decimal? ImporteMax,
    OrdenRenovaciones Orden,
    int Pagina,
    int Tamano);
