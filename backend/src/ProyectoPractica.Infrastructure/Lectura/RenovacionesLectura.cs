using Microsoft.EntityFrameworkCore;
using ProyectoPractica.Application.Abstracciones.Lectura;
using ProyectoPractica.Application.Comun.Modelos;
using ProyectoPractica.Application.Renovaciones;
using ProyectoPractica.Domain.Polizas;
using ProyectoPractica.Infrastructure.Persistencia;

namespace ProyectoPractica.Infrastructure.Lectura;

/// <summary>Consultas de solo lectura sobre pólizas: sin seguimiento de cambios y proyectando directamente a DTO.</summary>
internal sealed class RenovacionesLectura(AppDbContext contexto) : IRenovacionesLectura
{
    public async Task<PaginaDto<RenovacionDto>> ListarAsync(FiltroRenovaciones filtro, CancellationToken cancellationToken)
    {
        var consulta = Filtrar(contexto.Polizas.AsNoTracking(), filtro);
        var total = await consulta.CountAsync(cancellationToken);

        var items = await Ordenar(consulta, filtro.Orden)
            .Skip((filtro.Pagina - 1) * filtro.Tamano)
            .Take(filtro.Tamano)
            .Select(AProyeccion)
            .ToListAsync(cancellationToken);

        return new PaginaDto<RenovacionDto>(items, total, filtro.Pagina, filtro.Tamano);
    }

    public async Task<IReadOnlyList<RenovacionDto>> ProximasAsync(DateOnly desde, int limite, CancellationToken cancellationToken) =>
        await contexto.Polizas.AsNoTracking()
            .Where(p => p.FechaVencimiento >= desde)
            .OrderBy(p => p.FechaVencimiento).ThenBy(p => p.Id)
            .Take(limite)
            .Select(AProyeccion)
            .ToListAsync(cancellationToken);

    public Task<RenovacionDto?> ObtenerAsync(int id, CancellationToken cancellationToken) =>
        contexto.Polizas.AsNoTracking()
            .Where(p => p.Id == id)
            .Select(AProyeccion)
            .FirstOrDefaultAsync(cancellationToken);

    private static readonly System.Linq.Expressions.Expression<Func<Poliza, RenovacionDto>> AProyeccion = p =>
        new RenovacionDto(p.Id, p.NumPoliza, p.NombreRiesgo, p.FechaContrato, p.FechaVencimiento, p.Importe, p.Estado);

    private static IQueryable<Poliza> Filtrar(IQueryable<Poliza> consulta, FiltroRenovaciones filtro)
    {
        if (filtro.Texto is { } texto)
        {
            var minusculas = texto.ToLower();
            consulta = consulta.Where(p =>
                p.NombreRiesgo.ToLower().Contains(minusculas) || p.NumPoliza.ToString().Contains(minusculas));
        }

        if (filtro.Estado is { } estado) consulta = consulta.Where(p => p.Estado == estado);
        if (filtro.Desde is { } desde) consulta = consulta.Where(p => p.FechaVencimiento >= desde);
        if (filtro.Hasta is { } hasta) consulta = consulta.Where(p => p.FechaVencimiento <= hasta);
        if (filtro.ImporteMin is { } minimo) consulta = consulta.Where(p => p.Importe >= minimo);
        if (filtro.ImporteMax is { } maximo) consulta = consulta.Where(p => p.Importe <= maximo);

        return consulta;
    }

    private static IQueryable<Poliza> Ordenar(IQueryable<Poliza> consulta, OrdenRenovaciones orden) => orden switch
    {
        OrdenRenovaciones.VencimientoDescendente => consulta.OrderByDescending(p => p.FechaVencimiento).ThenBy(p => p.Id),
        OrdenRenovaciones.ImporteAscendente => consulta.OrderBy(p => p.Importe).ThenBy(p => p.Id),
        OrdenRenovaciones.ImporteDescendente => consulta.OrderByDescending(p => p.Importe).ThenBy(p => p.Id),
        _ => consulta.OrderBy(p => p.FechaVencimiento).ThenBy(p => p.Id),
    };
}
