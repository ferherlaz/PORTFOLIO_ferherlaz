using Microsoft.AspNetCore.Mvc;
using ProyectoPractica.Api.Contratos.Polizas;
using ProyectoPractica.Application.Abstracciones.Mensajeria;
using ProyectoPractica.Application.Polizas.Dtos;
using ProyectoPractica.Application.Polizas.Queries.ObtenerDetallePoliza;

namespace ProyectoPractica.Api.Controllers;

/// <summary>Detalle de póliza: una lectura completa y un PUT por cada bloque editable.</summary>
[ApiController]
[Route("api/polizas/{id:int}")]
[Produces("application/json")]
[Tags("Detalle de póliza")]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
public sealed class PolizasController(IMediador mediador) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Detalle completo: renovación, cinco bloques editables y últimas cuotas")]
    [ProducesResponseType<PolizaDetalleDto>(StatusCodes.Status200OK)]
    public Task<PolizaDetalleDto> Obtener(int id, CancellationToken cancellationToken) =>
        mediador.EnviarAsync(new ObtenerDetallePolizaQuery(id), cancellationToken);

    [HttpPut("titular")]
    [EndpointSummary("Guarda el bloque Titular")]
    [ProducesResponseType<TitularDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public Task<TitularDto> ActualizarTitular(int id, ActualizarTitularRequest peticion, CancellationToken cancellationToken) =>
        mediador.EnviarAsync(peticion.ACommand(id), cancellationToken);

    [HttpPut("facturacion")]
    [EndpointSummary("Guarda el bloque Dirección de facturación")]
    [ProducesResponseType<FacturacionDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public Task<FacturacionDto> ActualizarFacturacion(int id, ActualizarFacturacionRequest peticion, CancellationToken cancellationToken) =>
        mediador.EnviarAsync(peticion.ACommand(id), cancellationToken);

    [HttpPut("pago")]
    [EndpointSummary("Guarda el bloque Método de pago")]
    [ProducesResponseType<PagoDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public Task<PagoDto> ActualizarPago(int id, ActualizarPagoRequest peticion, CancellationToken cancellationToken) =>
        mediador.EnviarAsync(peticion.ACommand(id), cancellationToken);

    [HttpPut("correspondencia")]
    [EndpointSummary("Guarda el bloque Dirección de correspondencia")]
    [ProducesResponseType<CorrespondenciaDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public Task<CorrespondenciaDto> ActualizarCorrespondencia(int id, ActualizarCorrespondenciaRequest peticion, CancellationToken cancellationToken) =>
        mediador.EnviarAsync(peticion.ACommand(id), cancellationToken);

    [HttpPut("contacto")]
    [EndpointSummary("Guarda el bloque Contacto")]
    [ProducesResponseType<ContactoDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public Task<ContactoDto> ActualizarContacto(int id, ActualizarContactoRequest peticion, CancellationToken cancellationToken) =>
        mediador.EnviarAsync(peticion.ACommand(id), cancellationToken);
}
