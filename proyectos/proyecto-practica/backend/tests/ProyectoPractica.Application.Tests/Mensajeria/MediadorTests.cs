using Microsoft.Extensions.DependencyInjection;
using ProyectoPractica.Application.Abstracciones.Mensajeria;
using ProyectoPractica.Application.ClientesPotenciales.Commands.EliminarClientePotencial;
using ProyectoPractica.Application.Comun.Excepciones;
using ProyectoPractica.Application.Polizas.Commands.ActualizarPago;
using ProyectoPractica.Application.Polizas.Commands.ActualizarTitular;
using ProyectoPractica.Application.Tests.Dobles;
using ProyectoPractica.Domain.ClientesPotenciales;
using ProyectoPractica.Domain.Polizas;
using ProyectoPractica.Domain.Repositorios;

namespace ProyectoPractica.Application.Tests.Mensajeria;

/// <summary>Prueba el flujo completo mediador → validación → manejador → repositorio, con dobles en memoria.</summary>
public class MediadorTests
{
    private readonly Poliza _poliza = PolizasDePrueba.Nueva();
    private readonly UnitOfWorkFalso _unidadDeTrabajo = new();
    private readonly ClientePotencialRepositoryFalso _clientes = new(
        new ClientePotencial("Ana Perffeti", ProductoOfertado.Casa, "ana.per@gmail.com", "+34 258 632 444"));

    private IMediador CrearMediador()
    {
        var servicios = new ServiceCollection()
            .AddApplication()
            .AddSingleton<IPolizaRepository>(new PolizaRepositoryFalso(_poliza))
            .AddSingleton<IClientePotencialRepository>(_clientes)
            .AddSingleton<IUnitOfWork>(_unidadDeTrabajo)
            .BuildServiceProvider();

        return servicios.CreateScope().ServiceProvider.GetRequiredService<IMediador>();
    }

    [Fact]
    public async Task Un_comando_valido_actualiza_la_poliza_y_guarda_una_vez()
    {
        var respuesta = await CrearMediador().EnviarAsync(
            new ActualizarTitularCommand(1, " b63249544 ", " Grup Sisquella ", "Calle Casanova 128"));

        Assert.Equal("B63249544", respuesta.Cif);
        Assert.Equal("Grup Sisquella", _poliza.Titular.Nombre);
        Assert.Equal(1, _unidadDeTrabajo.VecesGuardado);
    }

    [Fact]
    public async Task Un_comando_invalido_no_llega_al_manejador_y_devuelve_errores_por_campo_en_camelCase()
    {
        var pagoAntes = _poliza.Pago;

        var error = await Assert.ThrowsAsync<ExcepcionValidacion>(() => CrearMediador().EnviarAsync(
            new ActualizarPagoCommand(1, "Ana", "B63249544", "Al contado", "ES0000000000000000000000", "")));

        Assert.Equal(new[] { "iban" }, error.Errores.Keys);
        Assert.Same(pagoAntes, _poliza.Pago);
        Assert.Equal(0, _unidadDeTrabajo.VecesGuardado);
    }

    [Fact]
    public async Task Actualizar_una_poliza_inexistente_lanza_no_encontrado()
    {
        await Assert.ThrowsAsync<ExcepcionNoEncontrado>(() => CrearMediador().EnviarAsync(
            new ActualizarTitularCommand(99, "12345678Z", "José", "Calle Mayor 1")));

        Assert.Equal(0, _unidadDeTrabajo.VecesGuardado);
    }

    [Fact]
    public async Task Eliminar_cliente_potencial_lo_quita_del_repositorio()
    {
        var resultado = await CrearMediador().EnviarAsync(new EliminarClientePotencialCommand(1));

        Assert.Equal(Unidad.Valor, resultado);
        Assert.Empty(_clientes.Restantes);
        Assert.Equal(1, _unidadDeTrabajo.VecesGuardado);
    }

    [Fact]
    public async Task Eliminar_un_cliente_inexistente_lanza_no_encontrado() =>
        await Assert.ThrowsAsync<ExcepcionNoEncontrado>(() =>
            CrearMediador().EnviarAsync(new EliminarClientePotencialCommand(42)));
}
