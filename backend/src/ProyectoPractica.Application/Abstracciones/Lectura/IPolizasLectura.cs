using ProyectoPractica.Application.Polizas.Dtos;

namespace ProyectoPractica.Application.Abstracciones.Lectura;

/// <summary>Lado de lectura (CQRS) del detalle de póliza.</summary>
public interface IPolizasLectura
{
    Task<PolizaDetalleDto?> ObtenerDetalleAsync(int id, CancellationToken cancellationToken);
}
