using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using ProyectoPractica.Api.Data;
using ProyectoPractica.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Render (y otros PaaS) indican el puerto en la variable PORT.
if (Environment.GetEnvironmentVariable("PORT") is { Length: > 0 } puerto)
{
    builder.WebHost.UseUrls($"http://0.0.0.0:{puerto}");
}

// Base de datos: PostgreSQL si hay cadena "Postgres"; si no, SQLite (cero configuración).
var postgres = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (!string.IsNullOrWhiteSpace(postgres))
        options.UseNpgsql(postgres);
    else
        options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite") ?? "Data Source=proyecto-practica.db");
});

builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, _, _) =>
    {
        document.Info.Title = "Proyecto Práctica · API de pólizas";
        document.Info.Version = "v1";
        document.Info.Description =
            "API REST en .NET 10 para el área personal de seguros del portfolio: listado de renovaciones " +
            "con filtros y paginación en servidor, detalle de póliza editable por bloques con validación " +
            "(NIF/CIF/NIE, IBAN, código postal, email) y clientes potenciales.";
        return Task.CompletedTask;
    });
});

builder.Services.ConfigureHttpJsonOptions(options =>
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var origenes = builder.Configuration.GetSection("Cors:Origins").Get<string[]>() ?? [];
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy
    .WithOrigins(origenes)
    .WithMethods("GET", "PUT", "DELETE")
    .WithHeaders("Content-Type")));

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();
app.UseCors();

// Swagger público a propósito: es un proyecto de portfolio para que se pueda probar.
app.MapOpenApi();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "Proyecto Práctica v1");
    options.DocumentTitle = "Proyecto Práctica · API";
});

app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();
app.MapGet("/health", () => Results.Ok(new { estado = "ok" })).WithTags("Sistema");

app.MapRenovaciones();
app.MapPolizas();
app.MapClientesPotenciales();

// Crea el esquema y carga la semilla si la base de datos está vacía.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var reloj = scope.ServiceProvider.GetRequiredService<TimeProvider>();
    await db.Database.EnsureCreatedAsync();
    await SemillaDatos.AsegurarDatosAsync(db, DateOnly.FromDateTime(reloj.GetLocalNow().Date));
}

app.Run();

public partial class Program;
