using ProyectoPractica.Domain.Reglas;

namespace ProyectoPractica.Domain.Tests.Reglas;

public class ReglasEspanaTests
{
    [Theory]
    [InlineData("12345678Z")]   // NIF
    [InlineData("12345678-z")]  // NIF con guion y minúscula
    [InlineData("X1234567L")]   // NIE
    [InlineData("B63249544")]   // CIF con dígito de control
    [InlineData("H59721886")]   // CIF de los mocks originales
    [InlineData("P0800000B")]   // CIF de entidad pública (letra de control)
    public void Documentos_validos(string documento) => Assert.True(ReglasEspana.EsDocumentoValido(documento));

    [Theory]
    [InlineData("")]
    [InlineData("12345678A")]   // letra incorrecta
    [InlineData("B63249545")]   // dígito de control incorrecto
    [InlineData("P08000002")]   // entidad pública con número en vez de letra
    [InlineData("1234")]
    public void Documentos_invalidos(string documento) => Assert.False(ReglasEspana.EsDocumentoValido(documento));

    [Theory]
    [InlineData("ES9121000418450200051332")]
    [InlineData("ES91 2100 0418 4502 0005 1332")]
    [InlineData("DE89370400440532013000")]
    public void Iban_valido(string iban) => Assert.True(ReglasEspana.EsIbanValido(iban));

    [Theory]
    [InlineData("ES9121000418450200051333")]  // control incorrecto
    [InlineData("ES17008100024200012345")]    // longitud española incorrecta
    [InlineData("ES1700810002420001******")]  // IBAN enmascarado de los mocks
    public void Iban_invalido(string iban) => Assert.False(ReglasEspana.EsIbanValido(iban));

    [Fact]
    public void Los_digitos_de_control_calculados_generan_un_iban_valido()
    {
        const string bban = "21000418450200051332";
        var iban = "ES" + ReglasEspana.DigitosControlIban("ES", bban) + bban;

        Assert.Equal("ES9121000418450200051332", iban);
    }

    [Theory]
    [InlineData("28013", true)]
    [InlineData("08001", true)]
    [InlineData("52006", true)]
    [InlineData("00123", false)]
    [InlineData("53001", false)]
    [InlineData("2801", false)]
    public void Codigo_postal(string cp, bool esperado) => Assert.Equal(esperado, ReglasEspana.EsCodigoPostalValido(cp));

    [Theory]
    [InlineData("ana.per@gmail.com", true)]
    [InlineData("ana@", false)]
    [InlineData("ana per@gmail.com", false)]
    [InlineData("ana@gmail", false)]
    public void Email(string email, bool esperado) => Assert.Equal(esperado, ReglasEspana.EsEmailValido(email));

    [Theory]
    [InlineData("+34 600 111 222", true)]
    [InlineData("600111222", true)]
    [InlineData("abc", false)]
    [InlineData("12", false)]
    public void Telefono(string telefono, bool esperado) => Assert.Equal(esperado, ReglasEspana.EsTelefonoValido(telefono));
}
