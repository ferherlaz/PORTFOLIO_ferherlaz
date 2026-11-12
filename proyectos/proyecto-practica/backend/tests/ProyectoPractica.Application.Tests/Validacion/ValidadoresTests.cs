using ProyectoPractica.Application.Polizas.Commands.ActualizarContacto;
using ProyectoPractica.Application.Polizas.Commands.ActualizarFacturacion;
using ProyectoPractica.Application.Polizas.Commands.ActualizarTitular;
using ProyectoPractica.Application.Renovaciones.Queries.ListarRenovaciones;
using ProyectoPractica.Application.Renovaciones.Queries.ObtenerProximasRenovaciones;

namespace ProyectoPractica.Application.Tests.Validacion;

public class ValidadoresTests
{
    private static string[] CamposConError<T>(FluentValidation.IValidator<T> validador, T instancia) =>
        validador.Validate(instancia).Errors.Select(e => e.PropertyName).Distinct().Order().ToArray();

    [Fact]
    public void Titular_con_letra_incorrecta_y_sin_nombre()
    {
        var campos = CamposConError(
            new ActualizarTitularCommandValidator(),
            new ActualizarTitularCommand(1, "12345678A", "", "Calle Mayor 1"));

        Assert.Equal(new[] { "Cif", "Nombre" }, campos);
    }

    [Fact]
    public void Un_campo_obligatorio_vacio_solo_muestra_el_mensaje_de_obligatorio()
    {
        var errores = new ActualizarTitularCommandValidator()
            .Validate(new ActualizarTitularCommand(1, "", "José", "Calle Mayor 1")).Errors;

        var error = Assert.Single(errores);
        Assert.Equal("El CIF/NIF es obligatorio.", error.ErrorMessage);
    }

    [Fact]
    public void Facturacion_con_codigo_postal_de_provincia_inexistente()
    {
        var campos = CamposConError(
            new ActualizarFacturacionCommandValidator(),
            new ActualizarFacturacionCommand(1, "Calle Mayor", "1", "Madrid", "53001"));

        Assert.Equal(new[] { "Cp" }, campos);
    }

    [Fact]
    public void Contacto_con_correo_secundario_vacio_es_valido() =>
        Assert.True(new ActualizarContactoCommandValidator()
            .Validate(new ActualizarContactoCommand(1, "ana@gmail.com", "", "+34 600 111 222")).IsValid);

    [Fact]
    public void Listado_sin_parametros_es_valido() =>
        Assert.True(new ListarRenovacionesQueryValidator().Validate(new ListarRenovacionesQuery()).IsValid);

    [Fact]
    public void Listado_con_parametros_incoherentes()
    {
        var consulta = new ListarRenovacionesQuery(
            Estado: "Raro",
            Desde: new DateOnly(2027, 1, 1),
            Hasta: new DateOnly(2026, 1, 1),
            ImporteMin: 500,
            ImporteMax: 100,
            Orden: "precio",
            Pagina: 0,
            Tamano: 500);

        var campos = CamposConError(new ListarRenovacionesQueryValidator(), consulta);

        Assert.Equal(new[] { "Estado", "Hasta", "ImporteMax", "Orden", "Pagina", "Tamano" }, campos);
    }

    [Theory]
    [InlineData(0, false)]
    [InlineData(3, true)]
    [InlineData(21, false)]
    public void Limite_de_proximas_renovaciones(int limite, bool esValido) =>
        Assert.Equal(esValido, new ObtenerProximasRenovacionesQueryValidator()
            .Validate(new ObtenerProximasRenovacionesQuery(limite)).IsValid);
}
