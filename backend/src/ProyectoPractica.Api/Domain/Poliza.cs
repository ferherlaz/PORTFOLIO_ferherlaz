namespace ProyectoPractica.Api.Domain;

public enum EstadoPoliza
{
    Pendiente,
    Pagada,
    Vencido
}

/// <summary>
/// Póliza (en el frontend, "renovación"). Los cinco bloques del detalle se guardan
/// como tipos propios (owned types) en la misma tabla: siempre existen y se editan por separado.
/// </summary>
public class Poliza
{
    public int Id { get; set; }
    public long NumPoliza { get; set; }
    public required string NombreRiesgo { get; set; }
    public DateOnly FechaContrato { get; set; }
    public DateOnly FechaVencimiento { get; set; }
    public decimal Importe { get; set; }
    public EstadoPoliza Estado { get; set; }

    public Titular Titular { get; set; } = new();
    public Facturacion Facturacion { get; set; } = new();
    public Pago Pago { get; set; } = new();
    public Correspondencia Correspondencia { get; set; } = new();
    public Contacto Contacto { get; set; } = new();

    public List<Cuota> Cuotas { get; set; } = [];
}

public class Titular
{
    public string Cif { get; set; } = "";
    public string Nombre { get; set; } = "";
    public string DirFiscal { get; set; } = "";
}

public class Facturacion
{
    public string DirFacturacion { get; set; } = "";
    public string Numero { get; set; } = "";
    public string Ciudad { get; set; } = "";
    public string Cp { get; set; } = "";
}

public class Pago
{
    public string Nombre { get; set; } = "";
    public string Cif { get; set; } = "";
    public string TipoCobro { get; set; } = "";
    public string Iban { get; set; } = "";
    public string Banco { get; set; } = "";
}

public class Correspondencia
{
    public string Datos { get; set; } = "";
    public string Poblacion { get; set; } = "";
    public string Cp { get; set; } = "";
    public string Direccion { get; set; } = "";
    public string Numero { get; set; } = "";
    public string Aclarador { get; set; } = "";
}

public class Contacto
{
    public string CorreoPrincipal { get; set; } = "";
    public string CorreoSecundario { get; set; } = "";
    public string Telefono { get; set; } = "";
}

public class Cuota
{
    public int Id { get; set; }
    public int PolizaId { get; set; }
    public DateOnly FechaEmision { get; set; }
    public DateOnly PeriodoDesde { get; set; }
    public DateOnly PeriodoHasta { get; set; }
    public decimal Importe { get; set; }
    public EstadoPoliza Estado { get; set; }
}
