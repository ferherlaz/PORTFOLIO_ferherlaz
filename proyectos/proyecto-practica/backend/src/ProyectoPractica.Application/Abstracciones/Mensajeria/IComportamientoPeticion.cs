namespace ProyectoPractica.Application.Abstracciones.Mensajeria;

/// <summary>
/// Comportamiento del pipeline del mediador (validación, registro, transacciones...).
/// Añadir uno nuevo no requiere modificar el mediador ni los manejadores (principio abierto/cerrado).
/// </summary>
public interface IComportamientoPeticion<in TPeticion, TRespuesta>
    where TPeticion : IPeticion<TRespuesta>
{
    Task<TRespuesta> ManejarAsync(TPeticion peticion, SiguientePaso<TRespuesta> siguiente, CancellationToken cancellationToken);
}
