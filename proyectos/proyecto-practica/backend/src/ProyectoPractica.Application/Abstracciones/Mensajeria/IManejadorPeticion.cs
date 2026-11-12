namespace ProyectoPractica.Application.Abstracciones.Mensajeria;

/// <summary>Manejador de una petición concreta. Un caso de uso = un manejador (responsabilidad única).</summary>
public interface IManejadorPeticion<in TPeticion, TRespuesta>
    where TPeticion : IPeticion<TRespuesta>
{
    Task<TRespuesta> ManejarAsync(TPeticion peticion, CancellationToken cancellationToken);
}
