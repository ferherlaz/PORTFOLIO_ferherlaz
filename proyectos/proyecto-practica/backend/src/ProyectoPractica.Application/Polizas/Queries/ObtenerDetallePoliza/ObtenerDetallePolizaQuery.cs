using ProyectoPractica.Application.Abstracciones.Mensajeria;
using ProyectoPractica.Application.Polizas.Dtos;

namespace ProyectoPractica.Application.Polizas.Queries.ObtenerDetallePoliza;

public sealed record ObtenerDetallePolizaQuery(int Id) : IQuery<PolizaDetalleDto>;
