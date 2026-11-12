namespace ProyectoPractica.Application.Abstracciones.Mensajeria;

/// <summary>Petición que modifica el estado del sistema (CQRS: lado de escritura).</summary>
public interface ICommand<TRespuesta> : IPeticion<TRespuesta>;
