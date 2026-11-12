using Microsoft.EntityFrameworkCore;
using ProyectoPractica.Application.Abstracciones.Lectura;
using ProyectoPractica.Application.ClientesPotenciales;
using ProyectoPractica.Infrastructure.Persistencia;

namespace ProyectoPractica.Infrastructure.Lectura;

internal sealed class ClientesPotencialesLectura(AppDbContext contexto) : IClientesPotencialesLectura
{
    public async Task<IReadOnlyList<ClientePotencialDto>> ListarAsync(CancellationToken cancellationToken) =>
        await contexto.ClientesPotenciales.AsNoTracking()
            .OrderBy(c => c.Id)
            .Select(c => new ClientePotencialDto(c.Id, c.Nombre, c.Producto, c.Email, c.Telefono))
            .ToListAsync(cancellationToken);
}
