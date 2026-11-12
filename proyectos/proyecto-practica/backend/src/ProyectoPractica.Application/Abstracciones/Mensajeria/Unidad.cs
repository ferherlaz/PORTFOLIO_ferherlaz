namespace ProyectoPractica.Application.Abstracciones.Mensajeria;

/// <summary>Respuesta vacía para comandos que no devuelven datos.</summary>
public readonly record struct Unidad
{
    public static readonly Unidad Valor = default;
}
