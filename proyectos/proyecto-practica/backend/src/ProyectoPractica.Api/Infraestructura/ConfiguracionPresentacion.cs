using System.Text.Json.Serialization;
using ProyectoPractica.Infrastructure.Persistencia;

namespace ProyectoPractica.Api.Infraestructura;

/// <summary>Servicios y pipeline HTTP de la API (controladores, errores, CORS, OpenAPI y salud).</summary>
public static class ConfiguracionPresentacion
{
    private const string Titulo = "Proyecto Práctica · API de pólizas";

    public static IServiceCollection AddPresentacion(this IServiceCollection services, IConfiguration configuracion)
    {
        services
            .AddControllers()
            .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        services.AddProblemDetails();
        services.AddExceptionHandler<ManejadorExcepciones>();

        var origenes = configuracion.GetSection("Cors:Origins").Get<string[]>() ?? [];
        services.AddCors(o => o.AddDefaultPolicy(politica => politica
            .WithOrigins(origenes)
            .WithMethods("GET", "PUT", "DELETE")
            .WithHeaders("Content-Type")));

        services.AddOpenApi(o => o.AddDocumentTransformer((documento, _, _) =>
        {
            documento.Info.Title = Titulo;
            documento.Info.Version = "v1";
            documento.Info.Description =
                "API REST en .NET 10 con Clean Architecture y CQRS para el área personal de seguros del portfolio: " +
                "renovaciones con filtros y paginación en servidor, detalle de póliza editable por bloques con " +
                "validación (NIF/CIF/NIE, IBAN, código postal, email) y clientes potenciales.";
            return Task.CompletedTask;
        }));

        services.AddHealthChecks().AddDbContextCheck<AppDbContext>("base-de-datos");

        return services;
    }

    public static WebApplication UsePresentacion(this WebApplication app)
    {
        app.UseExceptionHandler();
        app.UseStatusCodePages();
        app.UseCors();

        // Swagger público a propósito: es un proyecto de portfolio para que se pueda probar.
        app.MapOpenApi();
        app.UseSwaggerUI(o =>
        {
            o.SwaggerEndpoint("/openapi/v1.json", "Proyecto Práctica v1");
            o.DocumentTitle = Titulo;
        });

        app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();
        app.MapHealthChecks("/health");
        app.MapControllers();

        return app;
    }
}
