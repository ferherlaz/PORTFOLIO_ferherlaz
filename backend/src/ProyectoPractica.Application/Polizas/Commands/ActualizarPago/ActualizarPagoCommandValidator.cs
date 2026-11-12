using FluentValidation;
using ProyectoPractica.Application.Comun.Validacion;

namespace ProyectoPractica.Application.Polizas.Commands.ActualizarPago;

public sealed class ActualizarPagoCommandValidator : AbstractValidator<ActualizarPagoCommand>
{
    public ActualizarPagoCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(c => c.Nombre).Obligatorio("El nombre del pagador es obligatorio.").Maximo(120);
        RuleFor(c => c.Cif).Obligatorio("El CIF/NIF es obligatorio.").DocumentoIdentidad();
        RuleFor(c => c.TipoCobro).Obligatorio("El tipo de cobro es obligatorio.").Maximo(60);
        RuleFor(c => c.Iban).Obligatorio("El IBAN es obligatorio.").Iban();
        RuleFor(c => c.Banco).Maximo(80);
    }
}
