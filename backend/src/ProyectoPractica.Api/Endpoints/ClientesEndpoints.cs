using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProyectoPractica.Api.Contracts;
using ProyectoPractica.Api.Data;

namespace ProyectoPractica.Api.Endpoints;

public static class ClientesEndpoints
{
    public static RouteGroupBuilder MapClientesPotenciales(this IEndpointRouteBuilder app)
    {
        var grupo = app.MapGroup("/api/clientes-potenciales").WithTags("Clientes potenciales");

        grupo.MapGet("/", async (AppDbContext db, CancellationToken ct) =>
                TypedResults.Ok(await db.ClientesPotenciales.AsNoTracking()
                    .OrderBy(c => c.Id)
                    .Select(c => new ClientePotencialDto(c.Id, c.Nombre, c.Producto, c.Email, c.Telefono))
                    .ToListAsync(ct)))
            .WithSummary("Clientes potenciales del inicio");

        grupo.MapDelete("/{id:int}", Borrar)
            .WithSummary("Borra un cliente potencial (la papelera de la tarjeta)");

        return grupo;
    }

    internal static async Task<Results<NoContent, NotFound>> Borrar(int id, AppDbContext db, CancellationToken ct)
    {
        var borrados = await db.ClientesPotenciales.Where(c => c.Id == id).ExecuteDeleteAsync(ct);
        return borrados == 0 ? TypedResults.NotFound() : TypedResults.NoContent();
    }
}
