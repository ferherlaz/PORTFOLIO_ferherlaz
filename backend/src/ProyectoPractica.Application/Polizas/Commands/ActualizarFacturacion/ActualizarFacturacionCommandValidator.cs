using FluentValidation;
using ProyectoPractica.Application.Comun.Validacion;

namespace ProyectoPractica.Application.Polizas.Commands.ActualizarFacturacion;

public sealed class ActualizarFacturacionCommandValidator : AbstractValidator<ActualizarFacturacionCommand>
{
    public ActualizarFacturacionCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(c => c.DirFacturacion).Obligatorio("La dirección es obligatoria.").Maximo(200);
        RuleFor(c => c.Numero).Obligatorio("El número es obligatorio.").Maximo(10);
        RuleFor(c => c.Ciudad).Obligatorio("La ciudad es obligatoria.").Maximo(80);
        RuleFor(c => c.Cp).Obligatorio("El código postal es obligatorio.").CodigoPostal();
    }
}
