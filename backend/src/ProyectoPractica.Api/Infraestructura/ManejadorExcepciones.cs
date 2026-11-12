using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProyectoPractica.Application.Comun.Excepciones;
using ProyectoPractica.Domain.Comun;

namespace ProyectoPractica.Api.Infraestructura;

/// <summary>
/// Traduce las excepciones de las capas interiores a respuestas HTTP con formato ProblemDetails (RFC 9457).
/// Así los controladores no necesitan try/catch y las capas interiores no conocen HTTP.
/// </summary>
internal sealed class ManejadorExcepciones(IProblemDetailsService problemDetails, ILogger<ManejadorExcepciones> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext contexto, Exception excepcion, CancellationToken cancellationToken)
    {
        ProblemDetails? problema = excepcion switch
        {
            ExcepcionValidacion validacion => new HttpValidationProblemDetails(
                validacion.Errores.ToDictionary(e => e.Key, e => e.Value))
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Uno o más campos no son válidos.",
            },
            ExcepcionNoEncontrado noEncontrado => new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Recurso no encontrado.",
                Detail = noEncontrado.Message,
            },
            ExcepcionDominio dominio => new ProblemDetails
            {
                Status = StatusCodes.Status422UnprocessableEntity,
                Title = "La operación no cumple las reglas del negocio.",
                Detail = dominio.Message,
            },
            _ => null,
        };

        // Cualquier otra excepción es un error inesperado: la gestiona el manejador por defecto (500).
        if (problema is null)
        {
            logger.LogError(excepcion, "Error no controlado en {Ruta}", contexto.Request.Path);
            return false;
        }

        contexto.Response.StatusCode = problema.Status!.Value;
        return await problemDetails.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = contexto,
            ProblemDetails = problema,
            Exception = excepcion,
        });
    }
}
