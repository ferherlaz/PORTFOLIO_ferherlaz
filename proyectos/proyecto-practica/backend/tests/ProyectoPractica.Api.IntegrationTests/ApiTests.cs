using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoPractica.Application.ClientesPotenciales;
using ProyectoPractica.Application.Polizas.Dtos;
using ProyectoPractica.Application.Renovaciones;
using ProyectoPractica.Domain.Polizas;

namespace ProyectoPractica.Api.IntegrationTests;

/// <summary>Contrato HTTP que consume el frontend Angular: rutas, códigos de estado y forma del JSON.</summary>
public class ApiTests(ApiFactory factory) : IClassFixture<ApiFactory>
{
    private static readonly JsonSerializerOptions Json = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter() },
    };

    private readonly HttpClient _cliente = factory.CreateClient();

    [Fact]
    public async Task Listado_pagina_en_servidor_y_devuelve_el_json_esperado()
    {
        using var documento = JsonDocument.Parse(await _cliente.GetStringAsync("/api/renovaciones?pagina=2&tamano=10"));
        var raiz = documento.RootElement;

        Assert.Equal(50, raiz.GetProperty("total").GetInt32());
        Assert.Equal(2, raiz.GetProperty("pagina").GetInt32());
        Assert.Equal(10, raiz.GetProperty("tamano").GetInt32());
        Assert.Equal(5, raiz.GetProperty("totalPaginas").GetInt32());
        Assert.Equal(10, raiz.GetProperty("items").GetArrayLength());

        var primera = raiz.GetProperty("items")[0];
        Assert.Equal(JsonValueKind.String, primera.GetProperty("estado").ValueKind);
        Assert.Matches(@"^\d{4}-\d{2}-\d{2}$", primera.GetProperty("fechaVencimiento").GetString()!);
    }

    [Fact]
    public async Task Listado_filtra_por_estado_y_ordena_por_importe()
    {
        using var documento = JsonDocument.Parse(
            await _cliente.GetStringAsync("/api/renovaciones?estado=pagada&orden=importe_desc&tamano=100"));
        var items = documento.RootElement.GetProperty("items").Deserialize<List<RenovacionDto>>(Json)!;

        Assert.NotEmpty(items);
        Assert.All(items, r => Assert.Equal(EstadoPoliza.Pagada, r.Estado));
        Assert.Equal(items.OrderByDescending(r => r.Importe).Select(r => r.Id), items.Select(r => r.Id));
    }

    [Fact]
    public async Task Listado_con_parametros_invalidos_devuelve_problem_details_con_cada_campo()
    {
        var respuesta = await _cliente.GetAsync("/api/renovaciones?orden=precio&pagina=0&estado=Raro");

        Assert.Equal(HttpStatusCode.BadRequest, respuesta.StatusCode);
        Assert.Equal("application/problem+json", respuesta.Content.Headers.ContentType?.MediaType);
        var problema = await respuesta.Content.ReadFromJsonAsync<HttpValidationProblemDetails>();
        Assert.Equal(new[] { "estado", "orden", "pagina" }, problema!.Errors.Keys.Order());
    }

    [Fact]
    public async Task Todas_las_polizas_tienen_detalle_completo()
    {
        for (var id = 1; id <= 50; id++)
        {
            var detalle = await _cliente.GetFromJsonAsync<PolizaDetalleDto>($"/api/polizas/{id}", Json);
            Assert.NotNull(detalle);
            Assert.False(string.IsNullOrEmpty(detalle.Titular.Cif));
            Assert.False(string.IsNullOrEmpty(detalle.Pago.Iban));
            Assert.Equal(3, detalle.UltimasCuotas.Count);
        }
    }

    [Fact]
    public async Task Guardar_titular_persiste_y_devuelve_el_bloque_normalizado()
    {
        var respuesta = await _cliente.PutAsJsonAsync(
            "/api/polizas/7/titular",
            new { cif = "12345678z", nombre = "  José Martínez  ", dirFiscal = "Calle Casanova 128, Barcelona" });

        respuesta.EnsureSuccessStatusCode();
        var guardado = await respuesta.Content.ReadFromJsonAsync<TitularDto>(Json);
        Assert.Equal("12345678Z", guardado!.Cif);

        var detalle = await _cliente.GetFromJsonAsync<PolizaDetalleDto>("/api/polizas/7", Json);
        Assert.Equal("12345678Z", detalle!.Titular.Cif);
        Assert.Equal("José Martínez", detalle.Titular.Nombre);
    }

    [Fact]
    public async Task Guardar_pago_con_iban_invalido_no_guarda_y_devuelve_el_error_del_campo()
    {
        var antes = await _cliente.GetFromJsonAsync<PolizaDetalleDto>("/api/polizas/3", Json);
        var cuerpo = antes!.Pago with { Iban = "ES0000000000000000000000" };

        var respuesta = await _cliente.PutAsJsonAsync("/api/polizas/3/pago", cuerpo);

        Assert.Equal(HttpStatusCode.BadRequest, respuesta.StatusCode);
        var problema = await respuesta.Content.ReadFromJsonAsync<HttpValidationProblemDetails>();
        Assert.Equal(new[] { "iban" }, problema!.Errors.Keys);
        Assert.Equal("El IBAN no es válido (revisa los dígitos de control).", problema.Errors["iban"].Single());

        var despues = await _cliente.GetFromJsonAsync<PolizaDetalleDto>("/api/polizas/3", Json);
        Assert.Equal(antes.Pago.Iban, despues!.Pago.Iban);
    }

    [Theory]
    [InlineData("/api/polizas/999")]
    [InlineData("/api/renovaciones/999")]
    public async Task Un_recurso_inexistente_devuelve_404_con_problem_details(string ruta)
    {
        var respuesta = await _cliente.GetAsync(ruta);

        Assert.Equal(HttpStatusCode.NotFound, respuesta.StatusCode);
        var problema = await respuesta.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.Equal(404, problema!.Status);
    }

    [Fact]
    public async Task Guardar_en_una_poliza_inexistente_devuelve_404()
    {
        var respuesta = await _cliente.PutAsJsonAsync(
            "/api/polizas/999/contacto",
            new { correoPrincipal = "ana@gmail.com", correoSecundario = "", telefono = "+34 600 111 222" });

        Assert.Equal(HttpStatusCode.NotFound, respuesta.StatusCode);
    }

    [Fact]
    public async Task Proximas_renovaciones_vienen_ordenadas_desde_hoy()
    {
        var proximas = await _cliente.GetFromJsonAsync<List<RenovacionDto>>("/api/renovaciones/proximas?limite=3", Json);
        var hoy = DateOnly.FromDateTime(DateTime.Today);

        Assert.NotNull(proximas);
        Assert.Equal(3, proximas.Count);
        Assert.All(proximas, r => Assert.True(r.FechaVencimiento >= hoy));
        Assert.Equal(proximas.OrderBy(r => r.FechaVencimiento).Select(r => r.Id), proximas.Select(r => r.Id));
    }

    [Fact]
    public async Task Borrar_cliente_potencial_es_un_delete_real()
    {
        var antes = await _cliente.GetFromJsonAsync<List<ClientePotencialDto>>("/api/clientes-potenciales", Json);
        var id = antes!.Last().Id;

        Assert.Equal(HttpStatusCode.NoContent, (await _cliente.DeleteAsync($"/api/clientes-potenciales/{id}")).StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, (await _cliente.DeleteAsync($"/api/clientes-potenciales/{id}")).StatusCode);

        var despues = await _cliente.GetFromJsonAsync<List<ClientePotencialDto>>("/api/clientes-potenciales", Json);
        Assert.DoesNotContain(despues!, c => c.Id == id);
    }

    [Fact]
    public async Task Health_check_comprueba_la_base_de_datos()
    {
        var respuesta = await _cliente.GetAsync("/health");

        Assert.Equal(HttpStatusCode.OK, respuesta.StatusCode);
        Assert.Equal("Healthy", await respuesta.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task Cors_permite_el_dominio_del_portfolio()
    {
        var peticion = new HttpRequestMessage(HttpMethod.Get, "/api/renovaciones/proximas");
        peticion.Headers.Add("Origin", "https://ferherlazporfolio.com");

        var respuesta = await _cliente.SendAsync(peticion);

        Assert.Equal("https://ferherlazporfolio.com", respuesta.Headers.GetValues("Access-Control-Allow-Origin").Single());
    }
}
