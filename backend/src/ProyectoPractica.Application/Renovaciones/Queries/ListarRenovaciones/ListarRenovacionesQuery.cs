using ProyectoPractica.Application.Abstracciones.Mensajeria;
using ProyectoPractica.Application.Comun.Modelos;

namespace ProyectoPractica.Application.Renovaciones.Queries.ListarRenovaciones;

/// <summary>
/// Listado de renovaciones con filtros, orden y paginación en servidor.
/// Los parámetros llegan tal cual de la URL; el validador comprueba que se puedan interpretar.
/// </summary>
public sealed record ListarRenovacionesQuery(
    string? Q = null,
    string? Estado = null,
    DateOnly? Desde = null,
    DateOnly? Hasta = null,
    decimal? ImporteMin = null,
    decimal? ImporteMax = null,
    string? Orden = null,
    int? Pagina = null,
    int? Tamano = null) : IQuery<PaginaDto<RenovacionDto>>
{
    public const int TamanoPorDefecto = 10;
    public const int TamanoMaximo = 100;
}
