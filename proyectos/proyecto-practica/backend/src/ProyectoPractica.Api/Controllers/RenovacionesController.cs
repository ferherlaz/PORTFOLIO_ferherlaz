using Microsoft.AspNetCore.Mvc;
using ProyectoPractica.Application.Abstracciones.Mensajeria;
using ProyectoPractica.Application.Comun.Modelos;
using ProyectoPractica.Application.Renovaciones;
using ProyectoPractica.Application.Renovaciones.Queries.ListarRenovaciones;
using ProyectoPractica.Application.Renovaciones.Queries.ObtenerProximasRenovaciones;
using ProyectoPractica.Application.Renovaciones.Queries.ObtenerRenovacion;

namespace ProyectoPractica.Api.Controllers;

[ApiController]
[Route("api/renovaciones")]
[Produces("application/json")]
[Tags("Renovaciones")]
public sealed class RenovacionesController(IMediador mediador) : ControllerBase
{
    /// <summary>Listado con filtros, orden y paginación en servidor.</summary>
    /// <remarks>
    /// estado: Pendiente | Pagada | Vencido. desde/hasta: rango de vencimiento (yyyy-MM-dd).
    /// orden: vencimiento_asc (por defecto) | vencimiento_desc | importe_asc | importe_desc. tamano: 1-100.
    /// </remarks>
    [HttpGet]
    [EndpointSummary("Listado de renovaciones con filtros, orden y paginación en servidor")]
    [ProducesResponseType<PaginaDto<RenovacionDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public Task<PaginaDto<RenovacionDto>> Listar([FromQuery] ListarRenovacionesQuery query, CancellationToken cancellationToken) =>
        mediador.EnviarAsync(query, cancellationToken);

    [HttpGet("proximas")]
    [EndpointSummary("Próximas renovaciones a partir de hoy")]
    [ProducesResponseType<IReadOnlyList<RenovacionDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public Task<IReadOnlyList<RenovacionDto>> Proximas([FromQuery] int limite = 3, CancellationToken cancellationToken = default) =>
        mediador.EnviarAsync(new ObtenerProximasRenovacionesQuery(limite), cancellationToken);

    [HttpGet("{id:int}")]
    [EndpointSummary("Una renovación por id")]
    [ProducesResponseType<RenovacionDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public Task<RenovacionDto> Obtener(int id, CancellationToken cancellationToken) =>
        mediador.EnviarAsync(new ObtenerRenovacionQuery(id), cancellationToken);
}
