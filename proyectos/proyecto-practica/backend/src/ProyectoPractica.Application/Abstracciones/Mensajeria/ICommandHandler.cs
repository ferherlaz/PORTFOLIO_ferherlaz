namespace ProyectoPractica.Application.Abstracciones.Mensajeria;

/// <summary>Manejador de un comando (escritura).</summary>
public interface ICommandHandler<in TCommand, TRespuesta> : IManejadorPeticion<TCommand, TRespuesta>
    where TCommand : ICommand<TRespuesta>;
