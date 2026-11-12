using ProyectoPractica.Application.Abstracciones.Mensajeria;

namespace ProyectoPractica.Application.Polizas.Commands;

/// <summary>Comando que sustituye uno de los bloques editables de una póliza.</summary>
public interface IComandoBloquePoliza<TRespuesta> : ICommand<TRespuesta>
{
    int PolizaId { get; }
}
