using ProyectoPractica.Application.Polizas.Commands.ActualizarContacto;
using ProyectoPractica.Application.Polizas.Commands.ActualizarCorrespondencia;
using ProyectoPractica.Application.Polizas.Commands.ActualizarFacturacion;
using ProyectoPractica.Application.Polizas.Commands.ActualizarPago;
using ProyectoPractica.Application.Polizas.Commands.ActualizarTitular;
using ProyectoPractica.Infrastructure.Persistencia.Semilla;

namespace ProyectoPractica.Api.IntegrationTests;

/// <summary>La semilla debe pasar las mismas validaciones que un usuario: si no, "Guardar" fallaría sin tocar nada.</summary>
public class SemillaDatosTests
{
    [Fact]
    public void La_semilla_genera_50_polizas_unicas_y_todos_sus_bloques_son_validos()
    {
        var polizas = SemillaDatos.GenerarPolizas(new DateOnly(2026, 11, 12));

        Assert.Equal(SemillaDatos.NumeroPolizas, polizas.Count);
        Assert.Equal(polizas.Count, polizas.Select(p => p.NumPoliza).Distinct().Count());

        foreach (var p in polizas)
        {
            Assert.True(new ActualizarTitularCommandValidator()
                .Validate(new ActualizarTitularCommand(0, p.Titular.Cif, p.Titular.Nombre, p.Titular.DirFiscal)).IsValid);
            Assert.True(new ActualizarFacturacionCommandValidator()
                .Validate(new ActualizarFacturacionCommand(0, p.Facturacion.DirFacturacion, p.Facturacion.Numero, p.Facturacion.Ciudad, p.Facturacion.Cp)).IsValid);
            Assert.True(new ActualizarPagoCommandValidator()
                .Validate(new ActualizarPagoCommand(0, p.Pago.Nombre, p.Pago.Cif, p.Pago.TipoCobro, p.Pago.Iban, p.Pago.Banco)).IsValid);
            Assert.True(new ActualizarCorrespondenciaCommandValidator()
                .Validate(new ActualizarCorrespondenciaCommand(0, p.Correspondencia.Datos, p.Correspondencia.Poblacion, p.Correspondencia.Cp, p.Correspondencia.Direccion, p.Correspondencia.Numero, p.Correspondencia.Aclarador)).IsValid);
            Assert.True(new ActualizarContactoCommandValidator()
                .Validate(new ActualizarContactoCommand(0, p.Contacto.CorreoPrincipal, p.Contacto.CorreoSecundario, p.Contacto.Telefono)).IsValid);
            Assert.Equal(3, p.Cuotas.Count);
        }
    }
}
