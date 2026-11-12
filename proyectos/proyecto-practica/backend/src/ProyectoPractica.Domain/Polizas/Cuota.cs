using ProyectoPractica.Domain.Comun;

namespace ProyectoPractica.Domain.Polizas;

/// <summary>Recibo emitido para un periodo de la póliza. Solo se crea desde la propia póliza.</summary>
public sealed class Cuota : Entidad
{
    private Cuota()
    {
    }

    internal Cuota(DateOnly fechaEmision, DateOnly periodoDesde, DateOnly periodoHasta, decimal importe, EstadoPoliza estado)
    {
        if (periodoHasta < periodoDesde)
            throw new ExcepcionDominio("El periodo de la cuota termina antes de empezar.");
        if (importe < 0)
            throw new ExcepcionDominio("El importe de la cuota no puede ser negativo.");

        FechaEmision = fechaEmision;
        PeriodoDesde = periodoDesde;
        PeriodoHasta = periodoHasta;
        Importe = importe;
        Estado = estado;
    }

    public int PolizaId { get; private set; }
    public DateOnly FechaEmision { get; private set; }
    public DateOnly PeriodoDesde { get; private set; }
    public DateOnly PeriodoHasta { get; private set; }
    public decimal Importe { get; private set; }
    public EstadoPoliza Estado { get; private set; }
}
