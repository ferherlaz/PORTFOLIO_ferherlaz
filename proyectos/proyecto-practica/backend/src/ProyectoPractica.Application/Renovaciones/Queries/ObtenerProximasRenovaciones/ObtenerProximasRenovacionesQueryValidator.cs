using FluentValidation;

namespace ProyectoPractica.Application.Renovaciones.Queries.ObtenerProximasRenovaciones;

public sealed class ObtenerProximasRenovacionesQueryValidator : AbstractValidator<ObtenerProximasRenovacionesQuery>
{
    public ObtenerProximasRenovacionesQueryValidator()
    {
        RuleFor(q => q.Limite)
            .InclusiveBetween(1, 20)
            .WithMessage("El límite debe estar entre 1 y 20.");
    }
}
