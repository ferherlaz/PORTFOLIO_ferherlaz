using ProyectoPractica.Domain.Comun;
using ProyectoPractica.Domain.Polizas;

namespace ProyectoPractica.Domain.Tests.Polizas;

public class PolizaTests
{
    private static readonly DateOnly Contrato = new(2026, 1, 15);

    internal static Poliza NuevaPoliza(DateOnly? vencimiento = null, decimal importe = 1200m) => new(
        numPoliza: 75_846_843,
        nombreRiesgo: "  Seat León  ",
        fechaContrato: Contrato,
        fechaVencimiento: vencimiento ?? Contrato.AddYears(1),
        importe: importe,
        estado: EstadoPoliza.Pendiente,
        titular: Titular.Crear("12345678Z", "José Martínez", "Calle Mayor 1, Madrid"),
        facturacion: Facturacion.Crear("Calle Mayor", "1", "Madrid", "28013"),
        pago: Pago.Crear("José Martínez", "12345678Z", "Al contado", "ES9121000418450200051332", "CaixaBank"),
        correspondencia: Correspondencia.Crear("José Martínez", "Madrid", "28013", "Calle Mayor", "1", "Bajo"),
        contacto: Contacto.Crear("jose@gmail.com", "", "+34 600 111 222"));

    [Fact]
    public void Crear_una_poliza_recorta_el_nombre_del_riesgo()
    {
        var poliza = NuevaPoliza();

        Assert.Equal("Seat León", poliza.NombreRiesgo);
        Assert.Empty(poliza.Cuotas);
    }

    [Fact]
    public void No_se_puede_crear_con_vencimiento_anterior_al_contrato()
    {
        var error = Assert.Throws<ExcepcionDominio>(() => NuevaPoliza(vencimiento: Contrato.AddDays(-1)));

        Assert.Contains("vencimiento", error.Message);
    }

    [Fact]
    public void No_se_puede_crear_con_importe_negativo() =>
        Assert.Throws<ExcepcionDominio>(() => NuevaPoliza(importe: -1));

    [Fact]
    public void Actualizar_titular_sustituye_el_bloque_completo()
    {
        var poliza = NuevaPoliza();
        var nuevo = Titular.Crear(" b63249544 ", " Grup Sisquella ", "Calle Casanova 128");

        poliza.ActualizarTitular(nuevo);

        Assert.Same(nuevo, poliza.Titular);
        Assert.Equal("B63249544", poliza.Titular.Cif);
        Assert.Equal("Grup Sisquella", poliza.Titular.Nombre);
    }

    [Fact]
    public void Un_bloque_no_puede_quedar_vacio()
    {
        var poliza = NuevaPoliza();

        Assert.Throws<ExcepcionDominio>(() => poliza.ActualizarPago(null!));
    }

    [Fact]
    public void Emitir_cuota_la_anade_a_la_poliza()
    {
        var poliza = NuevaPoliza();

        poliza.EmitirCuota(Contrato.AddDays(-5), Contrato, Contrato.AddMonths(3).AddDays(-1), 300m, EstadoPoliza.Pagada);

        var cuota = Assert.Single(poliza.Cuotas);
        Assert.Equal(300m, cuota.Importe);
    }

    [Fact]
    public void Una_cuota_no_puede_acabar_antes_de_empezar()
    {
        var poliza = NuevaPoliza();

        Assert.Throws<ExcepcionDominio>(() =>
            poliza.EmitirCuota(Contrato, Contrato, Contrato.AddDays(-1), 100m, EstadoPoliza.Pagada));
    }
}
