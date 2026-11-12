using ProyectoPractica.Api.Domain;

namespace ProyectoPractica.Api.Contracts;

public record RenovacionDto(
    int Id,
    long NumPoliza,
    string NombreRiesgo,
    DateOnly FechaContrato,
    DateOnly FechaVencimiento,
    decimal Importe,
    EstadoPoliza Estado);

public record PaginaDto<T>(
    IReadOnlyList<T> Items,
    int Total,
    int Pagina,
    int Tamano,
    int TotalPaginas);

public record TitularDto(string Cif, string Nombre, string DirFiscal);

public record FacturacionDto(string DirFacturacion, string Numero, string Ciudad, string Cp);

public record PagoDto(string Nombre, string Cif, string TipoCobro, string Iban, string Banco);

public record CorrespondenciaDto(
    string Datos,
    string Poblacion,
    string Cp,
    string Direccion,
    string Numero,
    string Aclarador);

public record ContactoDto(string CorreoPrincipal, string CorreoSecundario, string Telefono);

public record CuotaDto(
    int Id,
    DateOnly FechaEmision,
    DateOnly PeriodoDesde,
    DateOnly PeriodoHasta,
    decimal Importe,
    EstadoPoliza Estado);

public record PolizaDetalleDto(
    RenovacionDto Renovacion,
    TitularDto Titular,
    FacturacionDto Facturacion,
    PagoDto Pago,
    CorrespondenciaDto Correspondencia,
    ContactoDto Contacto,
    IReadOnlyList<CuotaDto> UltimasCuotas);

public record ClientePotencialDto(
    int Id,
    string Nombre,
    ProductoOfertado Producto,
    string Email,
    string Telefono);
