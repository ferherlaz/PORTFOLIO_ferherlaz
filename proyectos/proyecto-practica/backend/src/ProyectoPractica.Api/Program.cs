using ProyectoPractica.Api.Infraestructura;
using ProyectoPractica.Application;
using ProyectoPractica.Infrastructure;
using ProyectoPractica.Infrastructure.Persistencia;

var builder = WebApplication.CreateBuilder(args);

// Render (y otros PaaS) indican el puerto en la variable PORT.
if (Environment.GetEnvironmentVariable("PORT") is { Length: > 0 } puerto)
{
    builder.WebHost.UseUrls($"http://0.0.0.0:{puerto}");
}

// Raíz de composición: cada capa registra sus propias dependencias.
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPresentacion(builder.Configuration);

var app = builder.Build();

app.UsePresentacion();

await app.Services.InicializarBaseDatosAsync();
await app.RunAsync();

/// <summary>Visible para las pruebas de integración (WebApplicationFactory).</summary>
public partial class Program;
