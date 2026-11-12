using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using ProyectoPractica.Api.Contracts;

namespace ProyectoPractica.Api.Tests;

/// <summary>Arranca la API real contra un fichero SQLite temporal por cada clase de test.</summary>
public sealed class ApiFactory : WebApplicationFactory<Program>
{
    private readonly string _rutaDb = Path.Combine(Path.GetTempPath(), $"pp-tests-{Guid.NewGuid():N}.db");

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("ConnectionStrings:Sqlite", $"Data Source={_rutaDb};Pooling=False");
        builder.UseSetting("ConnectionStrings:Postgres", "");
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (File.Exists(_rutaDb)) File.Delete(_rutaDb);
    }
}

public class ApiTests(ApiFactory factory) : IClassFixture<ApiFactory>
{
    private static readonly JsonSerializerOptions Json = new(JsonSerializerDefaults.Web)
    {
        Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
    };

    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Listado_pagina_en_servidor()
    {
        var pagina = await _client.GetFromJsonAsync<PaginaDto<RenovacionDto>>(
            "/api/renovaciones?pagina=2&tamano=10", Json);

        Assert.NotNull(pagina);
        Assert.Equal(50, pagina.Total);
        Assert.Equal(5, pagina.TotalPaginas);
        Assert.Equal(2, pagina.Pagina);
        Assert.Equal(10, pagina.Items.Count);
    }

    [Fact]
    public async Task Listado_filtra_por_estado_y_ordena_por_importe()
    {
        var pagina = await _client.GetFromJsonAsync<PaginaDto<RenovacionDto>>(
            "/api/renovaciones?estado=pagada&orden=importe_desc&tamano=100", Json);

        Assert.NotNull(pagina);
        Assert.NotEmpty(pagina.Items);
        Assert.All(pagina.Items, r => Assert.Equal(Domain.EstadoPoliza.Pagada, r.Estado));
        Assert.Equal(pagina.Items.OrderByDescending(r => r.Importe).Select(r => r.Id), pagina.Items.Select(r => r.Id));
    }

    [Fact]
    public async Task Listado_con_parametros_invalidos_devuelve_problem_details()
    {
        var respuesta = await _client.GetAsync("/api/renovaciones?orden=precio&pagina=0&estado=Raro");

        Assert.Equal(HttpStatusCode.BadRequest, respuesta.StatusCode);
        var problema = await respuesta.Content.ReadFromJsonAsync<HttpValidationProblemDetails>();
        Assert.NotNull(problema);
        Assert.Contains("orden", problema.Errors.Keys);
        Assert.Contains("pagina", problema.Errors.Keys);
        Assert.Contains("estado", problema.Errors.Keys);
    }

    [Fact]
    public async Task Todas_las_polizas_tienen_detalle_completo()
    {
        for (var id = 1; id <= 50; id++)
        {
            var detalle = await _client.GetFromJsonAsync<PolizaDetalleDto>($"/api/polizas/{id}", Json);
            Assert.NotNull(detalle);
            Assert.False(string.IsNullOrEmpty(detalle.Titular.Cif));
            Assert.False(string.IsNullOrEmpty(detalle.Pago.Iban));
            Assert.Equal(3, detalle.UltimasCuotas.Count);
        }
    }

    [Fact]
    public async Task Guardar_titular_persiste_los_cambios()
    {
        var dto = new TitularDto("12345678z", "  José Martínez  ", "Calle Casanova 128, Barcelona");

        var respuesta = await _client.PutAsJsonAsync("/api/polizas/7/titular", dto);
        respuesta.EnsureSuccessStatusCode();

        var detalle = await _client.GetFromJsonAsync<PolizaDetalleDto>("/api/polizas/7", Json);
        Assert.Equal("12345678Z", detalle!.Titular.Cif);
        Assert.Equal("José Martínez", detalle.Titular.Nombre);
    }

    [Fact]
    public async Task Guardar_pago_con_iban_invalido_no_guarda_y_devuelve_error_del_campo()
    {
        var antes = await _client.GetFromJsonAsync<PolizaDetalleDto>("/api/polizas/3", Json);
        var dto = antes!.Pago with { Iban = "ES0000000000000000000000" };

        var respuesta = await _client.PutAsJsonAsync("/api/polizas/3/pago", dto);

        Assert.Equal(HttpStatusCode.BadRequest, respuesta.StatusCode);
        var problema = await respuesta.Content.ReadFromJsonAsync<HttpValidationProblemDetails>();
        Assert.Equal(new[] { "iban" }, problema!.Errors.Keys);

        var despues = await _client.GetFromJsonAsync<PolizaDetalleDto>("/api/polizas/3", Json);
        Assert.Equal(antes.Pago.Iban, despues!.Pago.Iban);
    }

    [Fact]
    public async Task Poliza_inexistente_devuelve_404()
    {
        var respuesta = await _client.GetAsync("/api/polizas/999");
        Assert.Equal(HttpStatusCode.NotFound, respuesta.StatusCode);
    }

    [Fact]
    public async Task Proximas_renovaciones_vienen_ordenadas_desde_hoy()
    {
        var proximas = await _client.GetFromJsonAsync<List<RenovacionDto>>("/api/renovaciones/proximas?limite=3", Json);
        var hoy = DateOnly.FromDateTime(DateTime.Today);

        Assert.NotNull(proximas);
        Assert.Equal(3, proximas.Count);
        Assert.All(proximas, r => Assert.True(r.FechaVencimiento >= hoy));
        Assert.Equal(proximas.OrderBy(r => r.FechaVencimiento).Select(r => r.Id), proximas.Select(r => r.Id));
    }

    [Fact]
    public async Task Borrar_cliente_potencial_es_un_delete_real()
    {
        var antes = await _client.GetFromJsonAsync<List<ClientePotencialDto>>("/api/clientes-potenciales", Json);
        var id = antes!.Last().Id;

        Assert.Equal(HttpStatusCode.NoContent, (await _client.DeleteAsync($"/api/clientes-potenciales/{id}")).StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, (await _client.DeleteAsync($"/api/clientes-potenciales/{id}")).StatusCode);

        var despues = await _client.GetFromJsonAsync<List<ClientePotencialDto>>("/api/clientes-potenciales", Json);
        Assert.DoesNotContain(despues!, c => c.Id == id);
    }
}
