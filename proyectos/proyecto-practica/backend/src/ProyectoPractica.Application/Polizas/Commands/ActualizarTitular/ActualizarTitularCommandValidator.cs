using FluentValidation;
using ProyectoPractica.Application.Comun.Validacion;

namespace ProyectoPractica.Application.Polizas.Commands.ActualizarTitular;

public sealed class ActualizarTitularCommandValidator : AbstractValidator<ActualizarTitularCommand>
{
    public ActualizarTitularCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(c => c.Cif).Obligatorio("El CIF/NIF es obligatorio.").DocumentoIdentidad();
        RuleFor(c => c.Nombre).Obligatorio("El nombre es obligatorio.").Maximo(120);
        RuleFor(c => c.DirFiscal).Obligatorio("La dirección fiscal es obligatoria.").Maximo(200);
    }
}
