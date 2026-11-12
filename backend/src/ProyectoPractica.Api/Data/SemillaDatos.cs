using Microsoft.EntityFrameworkCore;
using ProyectoPractica.Api.Domain;
using ProyectoPractica.Api.Validation;

namespace ProyectoPractica.Api.Data;

/// <summary>
/// Genera 50 pólizas con todos los bloques de detalle rellenos y datos válidos
/// (NIF/CIF con letra correcta, IBAN con dígitos de control, CP reales).
/// Es determinista: con la misma fecha de referencia, siempre produce lo mismo.
/// Las fechas se calculan respecto a hoy para que "Próximas renovaciones" siempre tenga datos.
/// </summary>
public static class SemillaDatos
{
    public const int NumeroPolizas = 50;

    private static readonly string[] Riesgos =
    [
        "Hyundai Ioniq", "Seat León", "Toyota Corolla", "Volkswagen Golf", "Renault Clio",
        "Kia Niro", "Tesla Model 3", "Peugeot 3008", "Yamaha MT-07", "Honda PCX 125",
        "Vivienda C/ Mayor 12", "Local comercial Gran Vía", "Nave industrial Polígono Sur",
        "Ático Avenida Diagonal", "Chalet Urbanización Los Pinos"
    ];

    private static readonly string[] Nombres =
        ["José", "María", "Carmen", "Antonio", "Lucía", "Javier", "Ana", "David", "Laura", "Pablo", "Marta", "Sergio"];

    private static readonly string[] Apellidos =
        ["Martínez", "García", "López", "Sánchez", "Fernández", "Ruiz", "Moreno", "Navarro", "Romero", "Torres", "Gil", "Vidal"];

    private static readonly string[] Empresas =
    [
        "Grup Sisquella Serveis Professionals", "Transportes Hermanos Gil", "Talleres del Norte",
        "Clínica Dental Sonrisa", "Construcciones Levante", "Asesoría Fiscal Puerta del Sol"
    ];

    private static readonly (string Ciudad, string PrefijoCp)[] Ciudades =
    [
        ("Madrid", "28"), ("Barcelona", "08"), ("Valencia", "46"), ("Sevilla", "41"), ("Bilbao", "48"),
        ("Zaragoza", "50"), ("Málaga", "29"), ("Sabadell", "08"), ("Valladolid", "47"), ("A Coruña", "15")
    ];

    private static readonly string[] Calles =
    [
        "Calle Mayor", "Avenida de la Constitución", "Calle Casanova", "Paseo de Gracia",
        "Calle Lluís Companys", "Plaza de la Iglesia", "Calle Alcalá", "Ronda de Sant Pere"
    ];

    private static readonly (string Codigo, string Nombre)[] Bancos =
    [
        ("0049", "Banco Santander"), ("2100", "CaixaBank"), ("0182", "BBVA"),
        ("0081", "Banco Sabadell"), ("0128", "Bankinter")
    ];

    private static readonly string[] TiposCobro =
        ["Domiciliado estándar (7 días)", "Al contado", "A plazos", "Domiciliado trimestral"];

    private static readonly string[] Aclaradores = ["Bajo", "1º Izquierda", "2º Derecha", "Ático", "Local"];

    private static readonly string[] Dominios = ["gmail.com", "hotmail.com", "yahoo.es", "outlook.com"];

    public static async Task AsegurarDatosAsync(AppDbContext db, DateOnly hoy, CancellationToken ct = default)
    {
        if (await db.Polizas.AnyAsync(ct)) return;

        db.Polizas.AddRange(GenerarPolizas(hoy));
        db.ClientesPotenciales.AddRange(GenerarClientes());
        await db.SaveChangesAsync(ct);
    }

    public static List<Poliza> GenerarPolizas(DateOnly hoy)
    {
        var rnd = new Random(2024);
        var polizas = new List<Poliza>(NumeroPolizas);

        for (var i = 1; i <= NumeroPolizas; i++)
        {
            // Vencimientos repartidos entre hace 3 meses y dentro de 1 año.
            var vencimiento = hoy.AddDays(rnd.Next(-90, 366));
            var contrato = vencimiento.AddYears(-1);
            var importe = Math.Round((decimal)(rnd.NextDouble() * 2400 + 180), 2);
            var estado = vencimiento < hoy
                ? (rnd.Next(4) == 0 ? EstadoPoliza.Vencido : EstadoPoliza.Pagada)
                : (rnd.Next(3) == 0 ? EstadoPoliza.Pendiente : EstadoPoliza.Pagada);

            var nombre = $"{Elegir(rnd, Nombres)} {Elegir(rnd, Apellidos)} {Elegir(rnd, Apellidos)}";
            var (ciudad, prefijo) = Elegir(rnd, Ciudades);
            var cp = prefijo + rnd.Next(1, 999).ToString("000");
            var calle = Elegir(rnd, Calles);
            var numero = rnd.Next(1, 200).ToString();
            var esEmpresa = rnd.Next(3) == 0;
            var pagador = esEmpresa ? Elegir(rnd, Empresas) : nombre;
            var docTitular = GenerarNif(rnd);
            var docPagador = esEmpresa ? GenerarCif(rnd) : docTitular;
            var (codigoBanco, banco) = Elegir(rnd, Bancos);
            var usuario = Quitartildes(nombre.Split(' ')[0] + "." + nombre.Split(' ')[1]).ToLowerInvariant();

            var poliza = new Poliza
            {
                NumPoliza = 75_000_000 + i * 17_389 + rnd.Next(0, 999),
                NombreRiesgo = Elegir(rnd, Riesgos),
                FechaContrato = contrato,
                FechaVencimiento = vencimiento,
                Importe = importe,
                Estado = estado,
                Titular = new Titular { Cif = docTitular, Nombre = nombre, DirFiscal = $"{calle} {numero}, {ciudad}" },
                Facturacion = new Facturacion { DirFacturacion = calle, Numero = numero, Ciudad = ciudad, Cp = cp },
                Pago = new Pago
                {
                    Nombre = pagador,
                    Cif = docPagador,
                    TipoCobro = Elegir(rnd, TiposCobro),
                    Iban = GenerarIban(rnd, codigoBanco),
                    Banco = banco
                },
                Correspondencia = new Correspondencia
                {
                    Datos = pagador,
                    Poblacion = ciudad,
                    Cp = cp,
                    Direccion = calle,
                    Numero = numero,
                    Aclarador = Elegir(rnd, Aclaradores)
                },
                Contacto = new Contacto
                {
                    CorreoPrincipal = $"{usuario}@{Elegir(rnd, Dominios)}",
                    CorreoSecundario = rnd.Next(2) == 0 ? $"{usuario}{rnd.Next(10, 99)}@{Elegir(rnd, Dominios)}" : "",
                    Telefono = $"+34 6{rnd.Next(10, 99)} {rnd.Next(100, 999)} {rnd.Next(100, 999)}"
                }
            };

            // Tres últimas cuotas trimestrales: la más reciente sigue el estado de la póliza.
            for (var c = 0; c < 3; c++)
            {
                var desde = contrato.AddMonths(c * 3);
                poliza.Cuotas.Add(new Cuota
                {
                    FechaEmision = desde.AddDays(-5),
                    PeriodoDesde = desde,
                    PeriodoHasta = desde.AddMonths(3).AddDays(-1),
                    Importe = Math.Round(importe / 4, 2),
                    Estado = c == 2 ? estado : EstadoPoliza.Pagada
                });
            }

            polizas.Add(poliza);
        }

        return polizas;
    }

    public static List<ClientePotencial> GenerarClientes() =>
    [
        new() { Nombre = "José Carlos Rodríguez", Producto = ProductoOfertado.Moto, Email = "jose.rod@gmail.com", Telefono = "+34 258 632 555" },
        new() { Nombre = "Ana Perffeti", Producto = ProductoOfertado.Casa, Email = "ana.per@gmail.com", Telefono = "+34 258 632 444" },
        new() { Nombre = "Marta Navarro Gil", Producto = ProductoOfertado.Casa, Email = "marta.navarro@outlook.com", Telefono = "+34 611 204 389" },
        new() { Nombre = "Sergio Vidal Torres", Producto = ProductoOfertado.Moto, Email = "sergio.vidal@yahoo.es", Telefono = "+34 622 918 070" }
    ];

    private static T Elegir<T>(Random rnd, T[] opciones) => opciones[rnd.Next(opciones.Length)];

    private static string GenerarNif(Random rnd)
    {
        var numero = rnd.Next(10_000_000, 99_999_999);
        return $"{numero:00000000}{ValidadoresEspana.LetraNif(numero)}";
    }

    private static string GenerarCif(Random rnd)
    {
        var letra = "AB"[rnd.Next(2)];
        var digitos = rnd.Next(1_000_000, 9_999_999).ToString("0000000");
        return $"{letra}{digitos}{ValidadoresEspana.DigitoControlCif(digitos)}";
    }

    private static string GenerarIban(Random rnd, string codigoBanco)
    {
        var oficina = rnd.Next(1, 9999).ToString("0000");
        var dc = rnd.Next(0, 99).ToString("00");
        var cuenta = rnd.NextInt64(1_000_000_000, 9_999_999_999).ToString("0000000000");
        var bban = codigoBanco + oficina + dc + cuenta;
        return "ES" + ValidadoresEspana.DigitosControlIban("ES", bban) + bban;
    }

    private static string Quitartildes(string texto) => texto
        .Replace('á', 'a').Replace('é', 'e').Replace('í', 'i').Replace('ó', 'o').Replace('ú', 'u')
        .Replace('Á', 'A').Replace('É', 'E').Replace('Í', 'I').Replace('Ó', 'O').Replace('Ú', 'U');
}
