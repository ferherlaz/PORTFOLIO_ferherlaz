using ProyectoPractica.Application.Abstracciones.Lectura;
using ProyectoPractica.Application.Abstracciones.Mensajeria;

namespace ProyectoPractica.Application.Renovaciones.Queries.ObtenerProximasRenovaciones;

public sealed class ObtenerProximasRenovacionesQueryHandler(IRenovacionesLectura lectura, TimeProvider reloj)
    : IQueryHandler<ObtenerProximasRenovacionesQuery, IReadOnlyList<RenovacionDto>>
{
    public Task<IReadOnlyList<RenovacionDto>> ManejarAsync(ObtenerProximasRenovacionesQuery query, CancellationToken cancellationToken)
    {
        var hoy = DateOnly.FromDateTime(reloj.GetLocalNow().Date);
        return lectura.ProximasAsync(hoy, query.Limite, cancellationToken);
    }
}
