using ProyectoPractica.Application.Abstracciones.Lectura;
using ProyectoPractica.Application.Abstracciones.Mensajeria;
using ProyectoPractica.Application.Comun.Modelos;
using ProyectoPractica.Domain.Polizas;

namespace ProyectoPractica.Application.Renovaciones.Queries.ListarRenovaciones;

public sealed class ListarRenovacionesQueryHandler(IRenovacionesLectura lectura)
    : IQueryHandler<ListarRenovacionesQuery, PaginaDto<RenovacionDto>>
{
    public Task<PaginaDto<RenovacionDto>> ManejarAsync(ListarRenovacionesQuery query, CancellationToken cancellationToken)
    {
        // La petición ya está validada: aquí solo se convierte a un filtro tipado.
        ParametroOrden.TryParse(query.Orden, out var orden);

        var filtro = new FiltroRenovaciones(
            Texto: string.IsNullOrWhiteSpace(query.Q) ? null : query.Q.Trim(),
            Estado: string.IsNullOrWhiteSpace(query.Estado) ? null : Enum.Parse<EstadoPoliza>(query.Estado, ignoreCase: true),
            Desde: query.Desde,
            Hasta: query.Hasta,
            ImporteMin: query.ImporteMin,
            ImporteMax: query.ImporteMax,
            Orden: orden,
            Pagina: query.Pagina ?? 1,
            Tamano: query.Tamano ?? ListarRenovacionesQuery.TamanoPorDefecto);

        return lectura.ListarAsync(filtro, cancellationToken);
    }
}
