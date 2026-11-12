using ProyectoPractica.Application.Comun.Modelos;
using ProyectoPractica.Application.Renovaciones;

namespace ProyectoPractica.Application.Abstracciones.Lectura;

/// <summary>
/// Lado de lectura (CQRS) de las renovaciones: consultas optimizadas que devuelven DTOs directamente,
/// sin cargar agregados. La implementa Infrastructure.
/// </summary>
public interface IRenovacionesLectura
{
    Task<PaginaDto<RenovacionDto>> ListarAsync(FiltroRenovaciones filtro, CancellationToken cancellationToken);

    Task<IReadOnlyList<RenovacionDto>> ProximasAsync(DateOnly desde, int limite, CancellationToken cancellationToken);

    Task<RenovacionDto?> ObtenerAsync(int id, CancellationToken cancellationToken);
}
