namespace ProyectoPractica.Application.Abstracciones.Mensajeria;

/// <summary>Petición de solo lectura (CQRS: lado de lectura).</summary>
public interface IQuery<TRespuesta> : IPeticion<TRespuesta>;
