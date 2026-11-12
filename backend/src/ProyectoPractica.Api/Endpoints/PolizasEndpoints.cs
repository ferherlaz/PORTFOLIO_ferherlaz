using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProyectoPractica.Api.Contracts;
using ProyectoPractica.Api.Data;
using ProyectoPractica.Api.Domain;
using ProyectoPractica.Api.Validation;

namespace ProyectoPractica.Api.Endpoints;

public static class PolizasEndpoints
{
    public static RouteGroupBuilder MapPolizas(this IEndpointRouteBuilder app)
    {
        var grupo = app.MapGroup("/api/polizas/{id:int}").WithTags("Detalle de póliza");

        grupo.MapGet("/", Obtener)
            .WithSummary("Detalle completo: renovación, cinco bloques editables y últimas cuotas");

        // Un PUT por tarjeta: cada bloque se guarda y valida por separado, igual que en la pantalla.
        grupo.MapPut("/titular", (int id, TitularDto dto, AppDbContext db, CancellationToken ct) =>
                Guardar(id, db, ValidadoresPoliza.Validar(dto), p => p.Titular.Aplicar(dto), p => p.Titular.ToDto(), ct))
            .WithSummary("Guarda el bloque Titular");

        grupo.MapPut("/facturacion", (int id, FacturacionDto dto, AppDbContext db, CancellationToken ct) =>
                Guardar(id, db, ValidadoresPoliza.Validar(dto), p => p.Facturacion.Aplicar(dto), p => p.Facturacion.ToDto(), ct))
            .WithSummary("Guarda el bloque Dirección de facturación");

        grupo.MapPut("/pago", (int id, PagoDto dto, AppDbContext db, CancellationToken ct) =>
                Guardar(id, db, ValidadoresPoliza.Validar(dto), p => p.Pago.Aplicar(dto), p => p.Pago.ToDto(), ct))
            .WithSummary("Guarda el bloque Método de pago");

        grupo.MapPut("/correspondencia", (int id, CorrespondenciaDto dto, AppDbContext db, CancellationToken ct) =>
                Guardar(id, db, ValidadoresPoliza.Validar(dto), p => p.Correspondencia.Aplicar(dto), p => p.Correspondencia.ToDto(), ct))
            .WithSummary("Guarda el bloque Dirección de correspondencia");

        grupo.MapPut("/contacto", (int id, ContactoDto dto, AppDbContext db, CancellationToken ct) =>
                Guardar(id, db, ValidadoresPoliza.Validar(dto), p => p.Contacto.Aplicar(dto), p => p.Contacto.ToDto(), ct))
            .WithSummary("Guarda el bloque Contacto");

        return grupo;
    }

    internal static async Task<Results<Ok<PolizaDetalleDto>, NotFound>> Obtener(
        int id, AppDbContext db, CancellationToken ct)
    {
        var poliza = await db.Polizas.AsNoTracking()
            .Include(p => p.Cuotas)
            .FirstOrDefaultAsync(p => p.Id == id, ct);

        return poliza is null ? TypedResults.NotFound() : TypedResults.Ok(poliza.ToDetalleDto());
    }

    private static async Task<Results<Ok<TDto>, ValidationProblem, NotFound>> Guardar<TDto>(
        int id,
        AppDbContext db,
        Dictionary<string, string[]> errores,
        Action<Poliza> aplicar,
        Func<Poliza, TDto> respuesta,
        CancellationToken ct)
    {
        // Se valida antes de tocar la base de datos: un 400 nunca deja cambios a medias.
        if (errores.Count > 0) return TypedResults.ValidationProblem(errores);

        var poliza = await db.Polizas.FirstOrDefaultAsync(p => p.Id == id, ct);
        if (poliza is null) return TypedResults.NotFound();

        aplicar(poliza);
        await db.SaveChangesAsync(ct);

        return TypedResults.Ok(respuesta(poliza));
    }
}
