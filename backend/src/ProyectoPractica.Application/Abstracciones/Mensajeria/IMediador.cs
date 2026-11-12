namespace ProyectoPractica.Application.Abstracciones.Mensajeria;

/// <summary>Punto de entrada a los casos de uso: los controladores solo dependen de esta interfaz.</summary>
public interface IMediador
{
    Task<TRespuesta> EnviarAsync<TRespuesta>(IPeticion<TRespuesta> peticion, CancellationToken cancellationToken = default);
}
