using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ProyectoPractica.Api.IntegrationTests;

/// <summary>Arranca la API real contra un fichero SQLite temporal, distinto para cada clase de pruebas.</summary>
public sealed class ApiFactory : WebApplicationFactory<Program>
{
    private readonly string _rutaBaseDatos = Path.Combine(Path.GetTempPath(), $"pp-tests-{Guid.NewGuid():N}.db");

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("ConnectionStrings:Sqlite", $"Data Source={_rutaBaseDatos};Pooling=False");
        builder.UseSetting("ConnectionStrings:Postgres", string.Empty);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (File.Exists(_rutaBaseDatos)) File.Delete(_rutaBaseDatos);
    }
}
