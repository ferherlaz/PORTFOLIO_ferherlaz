namespace ProyectoPractica.Application.Comun.Excepciones;

/// <summary>El recurso pedido no existe.</summary>
public sealed class ExcepcionNoEncontrado(string recurso, object clave)
    : Exception($"No existe {recurso} con identificador '{clave}'.")
{
    public string Recurso { get; } = recurso;
    public object Clave { get; } = clave;
}
