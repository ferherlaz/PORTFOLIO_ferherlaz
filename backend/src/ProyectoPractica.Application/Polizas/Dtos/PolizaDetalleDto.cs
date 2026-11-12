using ProyectoPractica.Application.Renovaciones;

namespace ProyectoPractica.Application.Polizas.Dtos;

/// <summary>Detalle completo de una póliza: datos generales, los cinco bloques editables y las últimas cuotas.</summary>
public sealed record PolizaDetalleDto(
    RenovacionDto Renovacion,
    TitularDto Titular,
    FacturacionDto Facturacion,
    PagoDto Pago,
    CorrespondenciaDto Correspondencia,
    ContactoDto Contacto,
    IReadOnlyList<CuotaDto> UltimasCuotas);
