using ProyectoPractica.Application.Polizas.Dtos;
using ProyectoPractica.Application.Renovaciones;
using ProyectoPractica.Domain.Polizas;

namespace ProyectoPractica.Application.Polizas;

/// <summary>Conversión de entidades y value objects del dominio a DTOs de salida.</summary>
public static class MapeoPolizas
{
    public const int CuotasEnDetalle = 3;

    public static RenovacionDto ADto(this Poliza p) =>
        new(p.Id, p.NumPoliza, p.NombreRiesgo, p.FechaContrato, p.FechaVencimiento, p.Importe, p.Estado);

    public static TitularDto ADto(this Titular t) => new(t.Cif, t.Nombre, t.DirFiscal);

    public static FacturacionDto ADto(this Facturacion f) => new(f.DirFacturacion, f.Numero, f.Ciudad, f.Cp);

    public static PagoDto ADto(this Pago p) => new(p.Nombre, p.Cif, p.TipoCobro, p.Iban, p.Banco);

    public static CorrespondenciaDto ADto(this Correspondencia c) =>
        new(c.Datos, c.Poblacion, c.Cp, c.Direccion, c.Numero, c.Aclarador);

    public static ContactoDto ADto(this Contacto c) => new(c.CorreoPrincipal, c.CorreoSecundario, c.Telefono);

    public static CuotaDto ADto(this Cuota c) =>
        new(c.Id, c.FechaEmision, c.PeriodoDesde, c.PeriodoHasta, c.Importe, c.Estado);

    public static PolizaDetalleDto ADetalleDto(this Poliza p) => new(
        p.ADto(),
        p.Titular.ADto(),
        p.Facturacion.ADto(),
        p.Pago.ADto(),
        p.Correspondencia.ADto(),
        p.Contacto.ADto(),
        p.Cuotas.OrderByDescending(c => c.FechaEmision).Take(CuotasEnDetalle).Select(c => c.ADto()).ToList());
}
