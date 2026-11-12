using ProyectoPractica.Application.Abstracciones.Mensajeria;

namespace ProyectoPractica.Application.Renovaciones.Queries.ObtenerRenovacion;

public sealed record ObtenerRenovacionQuery(int Id) : IQuery<RenovacionDto>;
