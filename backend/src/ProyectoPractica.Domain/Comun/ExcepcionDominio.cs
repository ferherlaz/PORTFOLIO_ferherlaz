namespace ProyectoPractica.Domain.Comun;

/// <summary>Se lanza cuando una operación rompería una invariante del dominio.</summary>
public sealed class ExcepcionDominio(string mensaje) : Exception(mensaje);
