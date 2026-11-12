using ProyectoPractica.Domain.Polizas;

namespace ProyectoPractica.Domain.Tests.Polizas;

public class ValueObjectsTests
{
    [Fact]
    public void Pago_normaliza_documento_e_iban()
    {
        var pago = Pago.Crear(" Ana ", "b63249544", "Al contado", "es91 2100 0418 4502 0005 1332", " CaixaBank ");

        Assert.Equal("Ana", pago.Nombre);
        Assert.Equal("B63249544", pago.Cif);
        Assert.Equal("ES9121000418450200051332", pago.Iban);
        Assert.Equal("CaixaBank", pago.Banco);
    }

    [Fact]
    public void Contacto_guarda_los_correos_en_minusculas()
    {
        var contacto = Contacto.Crear(" Ana.Per@Gmail.com ", null, " +34 600 111 222 ");

        Assert.Equal("ana.per@gmail.com", contacto.CorreoPrincipal);
        Assert.Equal(string.Empty, contacto.CorreoSecundario);
        Assert.Equal("+34 600 111 222", contacto.Telefono);
    }

    [Fact]
    public void Dos_value_objects_con_los_mismos_datos_son_iguales() =>
        Assert.Equal(
            Facturacion.Crear("Calle Mayor", "1", "Madrid", "28013"),
            Facturacion.Crear(" Calle Mayor ", "1", "Madrid", "28013"));
}
