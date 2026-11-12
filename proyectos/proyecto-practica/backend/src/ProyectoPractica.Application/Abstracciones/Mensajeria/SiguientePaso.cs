namespace ProyectoPractica.Application.Abstracciones.Mensajeria;

/// <summary>Siguiente eslabón del pipeline del mediador (otro comportamiento o el manejador).</summary>
public delegate Task<TRespuesta> SiguientePaso<TRespuesta>();
