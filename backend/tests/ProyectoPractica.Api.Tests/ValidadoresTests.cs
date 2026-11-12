using ProyectoPractica.Api.Contracts;
using ProyectoPractica.Api.Data;
using ProyectoPractica.Api.Validation;

namespace ProyectoPractica.Api.Tests;

public class ValidadoresTests
{
    [Theory]
    [InlineData("12345678Z")]   // NIF
    [InlineData("12345678-z")]  // NIF con guion y minúscula
    [InlineData("X1234567L")]   // NIE
    [InlineData("B63249544")]   // CIF con dígito de control
    [InlineData("H59721886")]   // CIF de los mocks originales
    [InlineData("P0800000B")]   // CIF de entidad pública (letra de control)
    public void Documentos_validos(string doc) => Assert.True(ValidadoresEspana.EsDocumentoValido(doc));

    [Theory]
    [InlineData("")]
    [InlineData("12345678A")]   // letra incorrecta
    [InlineData("B63249545")]   // dígito de control incorrecto
    [InlineData("P08000002")]   // entidad pública con número en vez de letra
    [InlineData("1234")]
    public void Documentos_invalidos(string doc) => Assert.False(ValidadoresEspana.EsDocumentoValido(doc));

    [Theory]
    [InlineData("ES9121000418450200051332")]
    [InlineData("ES91 2100 0418 4502 0005 1332")]
    [InlineData("DE89370400440532013000")]
    public void Iban_valido(string iban) => Assert.True(ValidadoresEspana.EsIbanValido(iban));

    [Theory]
    [InlineData("ES9121000418450200051333")]  // control incorrecto
    [InlineData("ES17008100024200012345")]    // longitud española incorrecta
    [InlineData("ES1700810002420001******")]  // IBAN enmascarado de los mocks
    public void Iban_invalido(string iban) => Assert.False(ValidadoresEspana.EsIbanValido(iban));

    [Theory]
    [InlineData("28013", true)]
    [InlineData("08001", true)]
    [InlineData("52006", true)]
    [InlineData("00123", false)]
    [InlineData("53001", false)]
    [InlineData("2801", false)]
    public void Codigo_postal(string cp, bool esperado) =>
        Assert.Equal(esperado, ValidadoresEspana.EsCodigoPostalValido(cp));

    [Theory]
    [InlineData("ana.per@gmail.com", true)]
    [InlineData("ana@", false)]
    [InlineData("ana per@gmail.com", false)]
    [InlineData("ana@gmail", false)]
    public void Email(string email, bool esperado) =>
        Assert.Equal(esperado, ValidadoresEspana.EsEmailValido(email));

    [Fact]
    public void Titular_invalido_devuelve_errores_por_campo_en_camelCase()
    {
        var errores = ValidadoresPoliza.Validar(new TitularDto("12345678A", "", "Calle Mayor 1"));

        Assert.Equal(new[] { "cif", "nombre" }, errores.Keys.Order());
    }

    [Fact]
    public void Contacto_con_correo_secundario_vacio_es_valido()
    {
        var errores = ValidadoresPoliza.Validar(new ContactoDto("ana@gmail.com", "", "+34 600 111 222"));

        Assert.Empty(errores);
    }

    [Fact]
    public void La_semilla_genera_50_polizas_y_todas_pasan_la_validacion()
    {
        var polizas = SemillaDatos.GenerarPolizas(new DateOnly(2026, 11, 12));

        Assert.Equal(SemillaDatos.NumeroPolizas, polizas.Count);
        Assert.Equal(polizas.Count, polizas.Select(p => p.NumPoliza).Distinct().Count());

        foreach (var p in polizas)
        {
            Assert.Empty(ValidadoresPoliza.Validar(p.Titular.ToDto()));
            Assert.Empty(ValidadoresPoliza.Validar(p.Facturacion.ToDto()));
            Assert.Empty(ValidadoresPoliza.Validar(p.Pago.ToDto()));
            Assert.Empty(ValidadoresPoliza.Validar(p.Correspondencia.ToDto()));
            Assert.Empty(ValidadoresPoliza.Validar(p.Contacto.ToDto()));
            Assert.Equal(3, p.Cuotas.Count);
        }
    }
}
