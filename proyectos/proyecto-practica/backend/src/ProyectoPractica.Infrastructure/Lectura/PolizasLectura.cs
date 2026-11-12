using Microsoft.EntityFrameworkCore;
using ProyectoPractica.Application.Abstracciones.Lectura;
using ProyectoPractica.Application.Polizas;
using ProyectoPractica.Application.Polizas.Dtos;
using ProyectoPractica.Infrastructure.Persistencia;

namespace ProyectoPractica.Infrastructure.Lectura;

internal sealed class PolizasLectura(AppDbContext contexto) : IPolizasLectura
{
    public async Task<PolizaDetalleDto?> ObtenerDetalleAsync(int id, CancellationToken cancellationToken)
    {
        var poliza = await contexto.Polizas.AsNoTracking()
            .Include(p => p.Cuotas)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        return poliza?.ADetalleDto();
    }
}
