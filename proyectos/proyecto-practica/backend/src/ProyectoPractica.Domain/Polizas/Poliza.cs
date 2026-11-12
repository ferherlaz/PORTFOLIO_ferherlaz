using ProyectoPractica.Domain.Comun;

namespace ProyectoPractica.Domain.Polizas;

/// <summary>
/// Raíz de agregado "Póliza" (en el frontend, "renovación"). Los cinco bloques del detalle son
/// value objects: se sustituyen enteros a través de los métodos Actualizar*, nunca campo a campo.
/// </summary>
public sealed class Poliza : Entidad
{
    private readonly List<Cuota> _cuotas = [];

    private Poliza()
    {
        // Constructor para EF Core.
        NombreRiesgo = null!;
        Titular = null!;
        Facturacion = null!;
        Pago = null!;
        Correspondencia = null!;
        Contacto = null!;
    }

    public Poliza(
        long numPoliza,
        string nombreRiesgo,
        DateOnly fechaContrato,
        DateOnly fechaVencimiento,
        decimal importe,
        EstadoPoliza estado,
        Titular titular,
        Facturacion facturacion,
        Pago pago,
        Correspondencia correspondencia,
        Contacto contacto)
    {
        if (numPoliza <= 0)
            throw new ExcepcionDominio("El número de póliza debe ser positivo.");
        if (string.IsNullOrWhiteSpace(nombreRiesgo))
            throw new ExcepcionDominio("El nombre del riesgo es obligatorio.");
        if (fechaVencimiento < fechaContrato)
            throw new ExcepcionDominio("La fecha de vencimiento no puede ser anterior a la de contrato.");
        if (importe < 0)
            throw new ExcepcionDominio("El importe no puede ser negativo.");

        NumPoliza = numPoliza;
        NombreRiesgo = nombreRiesgo.Trim();
        FechaContrato = fechaContrato;
        FechaVencimiento = fechaVencimiento;
        Importe = importe;
        Estado = estado;
        Titular = Requerido(titular);
        Facturacion = Requerido(facturacion);
        Pago = Requerido(pago);
        Correspondencia = Requerido(correspondencia);
        Contacto = Requerido(contacto);
    }

    public long NumPoliza { get; private set; }
    public string NombreRiesgo { get; private set; }
    public DateOnly FechaContrato { get; private set; }
    public DateOnly FechaVencimiento { get; private set; }
    public decimal Importe { get; private set; }
    public EstadoPoliza Estado { get; private set; }

    public Titular Titular { get; private set; }
    public Facturacion Facturacion { get; private set; }
    public Pago Pago { get; private set; }
    public Correspondencia Correspondencia { get; private set; }
    public Contacto Contacto { get; private set; }

    public IReadOnlyCollection<Cuota> Cuotas => _cuotas.AsReadOnly();

    public void ActualizarTitular(Titular titular) => Titular = Requerido(titular);

    public void ActualizarFacturacion(Facturacion facturacion) => Facturacion = Requerido(facturacion);

    public void ActualizarPago(Pago pago) => Pago = Requerido(pago);

    public void ActualizarCorrespondencia(Correspondencia correspondencia) =>
        Correspondencia = Requerido(correspondencia);

    public void ActualizarContacto(Contacto contacto) => Contacto = Requerido(contacto);

    public Cuota EmitirCuota(DateOnly fechaEmision, DateOnly periodoDesde, DateOnly periodoHasta, decimal importe, EstadoPoliza estado)
    {
        var cuota = new Cuota(fechaEmision, periodoDesde, periodoHasta, importe, estado);
        _cuotas.Add(cuota);
        return cuota;
    }

    private static T Requerido<T>(T? valor) where T : class =>
        valor ?? throw new ExcepcionDominio($"El bloque {typeof(T).Name} es obligatorio.");
}
