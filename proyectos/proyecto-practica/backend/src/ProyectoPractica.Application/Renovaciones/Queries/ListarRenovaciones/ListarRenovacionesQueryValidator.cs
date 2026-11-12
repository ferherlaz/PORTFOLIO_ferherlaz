using FluentValidation;
using ProyectoPractica.Domain.Polizas;

namespace ProyectoPractica.Application.Renovaciones.Queries.ListarRenovaciones;

public sealed class ListarRenovacionesQueryValidator : AbstractValidator<ListarRenovacionesQuery>
{
    public ListarRenovacionesQueryValidator()
    {
        RuleFor(q => q.Estado)
            .Must(e => string.IsNullOrWhiteSpace(e) || EsEstadoValido(e))
            .WithMessage($"Valores admitidos: {string.Join(", ", Enum.GetNames<EstadoPoliza>())}.");

        RuleFor(q => q.Hasta)
            .Must((q, hasta) => q.Desde is null || hasta is null || q.Desde <= hasta)
            .WithMessage("La fecha 'hasta' no puede ser anterior a 'desde'.");

        RuleFor(q => q.ImporteMin)
            .GreaterThanOrEqualTo(0).When(q => q.ImporteMin is not null)
            .WithMessage("El importe mínimo no puede ser negativo.");

        RuleFor(q => q.ImporteMax)
            .Must((q, max) => q.ImporteMin is null || max is null || q.ImporteMin <= max)
            .WithMessage("El importe máximo no puede ser menor que el mínimo.");

        RuleFor(q => q.Orden)
            .Must(o => ParametroOrden.TryParse(o, out _))
            .WithMessage($"Valores admitidos: {string.Join(", ", ParametroOrden.Admitidos)}.");

        RuleFor(q => q.Pagina)
            .GreaterThanOrEqualTo(1).When(q => q.Pagina is not null)
            .WithMessage("La página empieza en 1.");

        RuleFor(q => q.Tamano)
            .InclusiveBetween(1, ListarRenovacionesQuery.TamanoMaximo).When(q => q.Tamano is not null)
            .WithMessage($"El tamaño debe estar entre 1 y {ListarRenovacionesQuery.TamanoMaximo}.");
    }

    internal static bool EsEstadoValido(string valor) =>
        Enum.TryParse<EstadoPoliza>(valor, ignoreCase: true, out var estado) && Enum.IsDefined(estado);
}
