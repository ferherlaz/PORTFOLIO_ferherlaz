namespace ProyectoPractica.Application.Comun.Modelos;

/// <summary>Página de resultados de un listado paginado en servidor.</summary>
public sealed record PaginaDto<T>(IReadOnlyList<T> Items, int Total, int Pagina, int Tamano)
{
    public int TotalPaginas => Tamano <= 0 ? 0 : (int)Math.Ceiling(Total / (double)Tamano);
}
