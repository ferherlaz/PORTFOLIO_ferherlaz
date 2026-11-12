using ProyectoPractica.Application.Abstracciones.Lectura;
using ProyectoPractica.Application.Abstracciones.Mensajeria;
using ProyectoPractica.Application.Comun.Excepciones;

namespace ProyectoPractica.Application.Renovaciones.Queries.ObtenerRenovacion;

public sealed class ObtenerRenovacionQueryHandler(IRenovacionesLectura lectura)
    : IQueryHandler<ObtenerRenovacionQuery, RenovacionDto>
{
    public async Task<RenovacionDto> ManejarAsync(ObtenerRenovacionQuery query, CancellationToken cancellationToken) =>
        await lectura.ObtenerAsync(query.Id, cancellationToken)
        ?? throw new ExcepcionNoEncontrado("ninguna renovación", query.Id);
}
