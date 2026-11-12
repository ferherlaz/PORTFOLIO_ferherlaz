using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProyectoPractica.Api.Contracts;
using ProyectoPractica.Api.Data;
using ProyectoPractica.Api.Domain;

namespace ProyectoPractica.Api.Endpoints;

/// <summary>Parámetros de consulta del listado. Todos opcionales.</summary>
public record RenovacionesQuery(
    string? Q,
    string? Estado,
    DateOnly? Desde,
    DateOnly? Hasta,
    decimal? ImporteMin,
    decimal? ImporteMax,
    string? Orden,
    int? Pagina,
    int? Tamano);

public static class RenovacionesEndpoints
{
    public const int TamanoPorDefecto = 10;
    public const int TamanoMaximo = 100;

    private static readonly string[] OrdenesValidos =
        ["vencimiento_asc", "vencimiento_desc", "importe_asc", "importe_desc"];

    public static RouteGroupBuilder MapRenovaciones(this IEndpointRouteBuilder app)
    {
        var grupo = app.MapGroup("/api/renovaciones").WithTags("Renovaciones");

        grupo.MapGet("/", Listar)
            .WithSummary("Listado con filtros, orden y paginación en servidor")
            .WithDescription(
                "estado: Pendiente | Pagada | Vencido. desde/hasta: rango de fecha de vencimiento (yyyy-MM-dd). " +
                "orden: vencimiento_asc (por defecto) | vencimiento_desc | importe_asc | importe_desc. " +
                $"tamano: 1-{TamanoMaximo} (por defecto {TamanoPorDefecto}).");

        grupo.MapGet("/proximas", Proximas)
            .WithSummary("Próximas renovaciones a partir de hoy, por fecha de vencimiento");

        grupo.MapGet("/{id:int}", Obtener)
            .WithSummary("Una renovación por id");

        return grupo;
    }

    internal static async Task<Results<Ok<PaginaDto<RenovacionDto>>, ValidationProblem>> Listar(
        [AsParameters] RenovacionesQuery query, AppDbContext db, CancellationToken ct)
    {
        var errores = new Dictionary<string, string[]>();
        EstadoPoliza? estado = null;

        if (!string.IsNullOrWhiteSpace(query.Estado))
        {
            if (Enum.TryParse<EstadoPoliza>(query.Estado, ignoreCase: true, out var e) && Enum.IsDefined(e))
                estado = e;
            else
                errores["estado"] = ["Valores admitidos: Pendiente, Pagada, Vencido."];
        }

        if (query.Desde is { } d && query.Hasta is { } h && d > h)
            errores["hasta"] = ["La fecha 'hasta' no puede ser anterior a 'desde'."];

        if (query.ImporteMin is { } min && query.ImporteMax is { } max && min > max)
            errores["importeMax"] = ["El importe máximo no puede ser menor que el mínimo."];

        var orden = string.IsNullOrWhiteSpace(query.Orden) ? "vencimiento_asc" : query.Orden.Trim().ToLowerInvariant();
        if (!OrdenesValidos.Contains(orden))
            errores["orden"] = [$"Valores admitidos: {string.Join(", ", OrdenesValidos)}."];

        var pagina = query.Pagina ?? 1;
        var tamano = query.Tamano ?? TamanoPorDefecto;
        if (pagina < 1) errores["pagina"] = ["La página empieza en 1."];
        if (tamano is < 1 or > TamanoMaximo) errores["tamano"] = [$"El tamaño debe estar entre 1 y {TamanoMaximo}."];

        if (errores.Count > 0) return TypedResults.ValidationProblem(errores);

        var consulta = db.Polizas.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.Q))
        {
            var texto = query.Q.Trim().ToLower();
            consulta = consulta.Where(p =>
                p.NombreRiesgo.ToLower().Contains(texto) || p.NumPoliza.ToString().Contains(texto));
        }
        if (estado is not null) consulta = consulta.Where(p => p.Estado == estado);
        if (query.Desde is { } desde) consulta = consulta.Where(p => p.FechaVencimiento >= desde);
        if (query.Hasta is { } hasta) consulta = consulta.Where(p => p.FechaVencimiento <= hasta);
        if (query.ImporteMin is { } importeMin) consulta = consulta.Where(p => p.Importe >= importeMin);
        if (query.ImporteMax is { } importeMax) consulta = consulta.Where(p => p.Importe <= importeMax);

        consulta = orden switch
        {
            "vencimiento_desc" => consulta.OrderByDescending(p => p.FechaVencimiento).ThenBy(p => p.Id),
            "importe_asc" => consulta.OrderBy(p => p.Importe).ThenBy(p => p.Id),
            "importe_desc" => consulta.OrderByDescending(p => p.Importe).ThenBy(p => p.Id),
            _ => consulta.OrderBy(p => p.FechaVencimiento).ThenBy(p => p.Id)
        };

        var total = await consulta.CountAsync(ct);
        var items = await consulta
            .Skip((pagina - 1) * tamano)
            .Take(tamano)
            .Select(p => new RenovacionDto(p.Id, p.NumPoliza, p.NombreRiesgo, p.FechaContrato,
                p.FechaVencimiento, p.Importe, p.Estado))
            .ToListAsync(ct);

        var totalPaginas = (int)Math.Ceiling(total / (double)tamano);
        return TypedResults.Ok(new PaginaDto<RenovacionDto>(items, total, pagina, tamano, totalPaginas));
    }

    internal static async Task<Results<Ok<List<RenovacionDto>>, ValidationProblem>> Proximas(
        AppDbContext db, TimeProvider reloj, CancellationToken ct, int limite = 3)
    {
        if (limite is < 1 or > 20)
            return TypedResults.ValidationProblem(new Dictionary<string, string[]>
            {
                ["limite"] = ["El límite debe estar entre 1 y 20."]
            });

        var hoy = DateOnly.FromDateTime(reloj.GetLocalNow().Date);
        var items = await db.Polizas.AsNoTracking()
            .Where(p => p.FechaVencimiento >= hoy)
            .OrderBy(p => p.FechaVencimiento).ThenBy(p => p.Id)
            .Take(limite)
            .Select(p => new RenovacionDto(p.Id, p.NumPoliza, p.NombreRiesgo, p.FechaContrato,
                p.FechaVencimiento, p.Importe, p.Estado))
            .ToListAsync(ct);

        return TypedResults.Ok(items);
    }

    internal static async Task<Results<Ok<RenovacionDto>, NotFound>> Obtener(
        int id, AppDbContext db, CancellationToken ct)
    {
        var poliza = await db.Polizas.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, ct);
        return poliza is null ? TypedResults.NotFound() : TypedResults.Ok(poliza.ToDto());
    }
}
