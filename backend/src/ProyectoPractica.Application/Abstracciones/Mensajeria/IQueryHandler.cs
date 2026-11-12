namespace ProyectoPractica.Application.Abstracciones.Mensajeria;

/// <summary>Manejador de una consulta (lectura).</summary>
public interface IQueryHandler<in TQuery, TRespuesta> : IManejadorPeticion<TQuery, TRespuesta>
    where TQuery : IQuery<TRespuesta>;
