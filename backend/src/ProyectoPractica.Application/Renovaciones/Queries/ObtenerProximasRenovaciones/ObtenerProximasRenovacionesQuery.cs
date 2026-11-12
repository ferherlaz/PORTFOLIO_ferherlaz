using ProyectoPractica.Application.Abstracciones.Mensajeria;

namespace ProyectoPractica.Application.Renovaciones.Queries.ObtenerProximasRenovaciones;

/// <summary>Próximas renovaciones a partir de hoy, ordenadas por fecha de vencimiento.</summary>
public sealed record ObtenerProximasRenovacionesQuery(int Limite = 3) : IQuery<IReadOnlyList<RenovacionDto>>;
