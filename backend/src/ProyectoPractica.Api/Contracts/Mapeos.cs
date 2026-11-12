using ProyectoPractica.Api.Domain;

namespace ProyectoPractica.Api.Contracts;

public static class Mapeos
{
    public static RenovacionDto ToDto(this Poliza p) =>
        new(p.Id, p.NumPoliza, p.NombreRiesgo, p.FechaContrato, p.FechaVencimiento, p.Importe, p.Estado);

    public static TitularDto ToDto(this Titular t) => new(t.Cif, t.Nombre, t.DirFiscal);

    public static FacturacionDto ToDto(this Facturacion f) =>
        new(f.DirFacturacion, f.Numero, f.Ciudad, f.Cp);

    public static PagoDto ToDto(this Pago p) => new(p.Nombre, p.Cif, p.TipoCobro, p.Iban, p.Banco);

    public static CorrespondenciaDto ToDto(this Correspondencia c) =>
        new(c.Datos, c.Poblacion, c.Cp, c.Direccion, c.Numero, c.Aclarador);

    public static ContactoDto ToDto(this Contacto c) =>
        new(c.CorreoPrincipal, c.CorreoSecundario, c.Telefono);

    public static CuotaDto ToDto(this Cuota c) =>
        new(c.Id, c.FechaEmision, c.PeriodoDesde, c.PeriodoHasta, c.Importe, c.Estado);

    public static ClientePotencialDto ToDto(this ClientePotencial c) =>
        new(c.Id, c.Nombre, c.Producto, c.Email, c.Telefono);

    public static PolizaDetalleDto ToDetalleDto(this Poliza p) => new(
        p.ToDto(),
        p.Titular.ToDto(),
        p.Facturacion.ToDto(),
        p.Pago.ToDto(),
        p.Correspondencia.ToDto(),
        p.Contacto.ToDto(),
        p.Cuotas.OrderByDescending(c => c.FechaEmision).Take(3).Select(c => c.ToDto()).ToList());

    private static string Limpio(string? valor) => (valor ?? "").Trim();

    public static void Aplicar(this Titular destino, TitularDto dto)
    {
        destino.Cif = Validation.ValidadoresEspana.Normalizar(dto.Cif);
        destino.Nombre = Limpio(dto.Nombre);
        destino.DirFiscal = Limpio(dto.DirFiscal);
    }

    public static void Aplicar(this Facturacion destino, FacturacionDto dto)
    {
        destino.DirFacturacion = Limpio(dto.DirFacturacion);
        destino.Numero = Limpio(dto.Numero);
        destino.Ciudad = Limpio(dto.Ciudad);
        destino.Cp = Limpio(dto.Cp);
    }

    public static void Aplicar(this Pago destino, PagoDto dto)
    {
        destino.Nombre = Limpio(dto.Nombre);
        destino.Cif = Validation.ValidadoresEspana.Normalizar(dto.Cif);
        destino.TipoCobro = Limpio(dto.TipoCobro);
        destino.Iban = Validation.ValidadoresEspana.Normalizar(dto.Iban);
        destino.Banco = Limpio(dto.Banco);
    }

    public static void Aplicar(this Correspondencia destino, CorrespondenciaDto dto)
    {
        destino.Datos = Limpio(dto.Datos);
        destino.Poblacion = Limpio(dto.Poblacion);
        destino.Cp = Limpio(dto.Cp);
        destino.Direccion = Limpio(dto.Direccion);
        destino.Numero = Limpio(dto.Numero);
        destino.Aclarador = Limpio(dto.Aclarador);
    }

    public static void Aplicar(this Contacto destino, ContactoDto dto)
    {
        destino.CorreoPrincipal = Limpio(dto.CorreoPrincipal).ToLowerInvariant();
        destino.CorreoSecundario = Limpio(dto.CorreoSecundario).ToLowerInvariant();
        destino.Telefono = Limpio(dto.Telefono);
    }
}
