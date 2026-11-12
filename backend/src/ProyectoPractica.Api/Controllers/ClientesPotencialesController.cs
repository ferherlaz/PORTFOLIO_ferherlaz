using Microsoft.AspNetCore.Mvc;
using ProyectoPractica.Application.Abstracciones.Mensajeria;
using ProyectoPractica.Application.ClientesPotenciales;
using ProyectoPractica.Application.ClientesPotenciales.Commands.EliminarClientePotencial;
using ProyectoPractica.Application.ClientesPotenciales.Queries.ListarClientesPotenciales;

namespace ProyectoPractica.Api.Controllers;

[ApiController]
[Route("api/clientes-potenciales")]
[Produces("application/json")]
[Tags("Clientes potenciales")]
public sealed class ClientesPotencialesController(IMediador mediador) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Clientes potenciales del inicio")]
    [ProducesResponseType<IReadOnlyList<ClientePotencialDto>>(StatusCodes.Status200OK)]
    public Task<IReadOnlyList<ClientePotencialDto>> Listar(CancellationToken cancellationToken) =>
        mediador.EnviarAsync(new ListarClientesPotencialesQuery(), cancellationToken);

    [HttpDelete("{id:int}")]
    [EndpointSummary("Borra un cliente potencial (la papelera de la tarjeta)")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Eliminar(int id, CancellationToken cancellationToken)
    {
        await mediador.EnviarAsync(new EliminarClientePotencialCommand(id), cancellationToken);
        return NoContent();
    }
}
