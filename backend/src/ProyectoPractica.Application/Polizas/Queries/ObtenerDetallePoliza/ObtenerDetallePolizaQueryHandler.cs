using ProyectoPractica.Application.Abstracciones.Lectura;
using ProyectoPractica.Application.Abstracciones.Mensajeria;
using ProyectoPractica.Application.Comun.Excepciones;
using ProyectoPractica.Application.Polizas.Dtos;

namespace ProyectoPractica.Application.Polizas.Queries.ObtenerDetallePoliza;

public sealed class ObtenerDetallePolizaQueryHandler(IPolizasLectura lectura)
    : IQueryHandler<ObtenerDetallePolizaQuery, PolizaDetalleDto>
{
    public async Task<PolizaDetalleDto> ManejarAsync(ObtenerDetallePolizaQuery query, CancellationToken cancellationToken) =>
        await lectura.ObtenerDetalleAsync(query.Id, cancellationToken)
        ?? throw new ExcepcionNoEncontrado("ninguna póliza", query.Id);
}
