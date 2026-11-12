using FluentValidation;
using ProyectoPractica.Application.Comun.Validacion;

namespace ProyectoPractica.Application.Polizas.Commands.ActualizarCorrespondencia;

public sealed class ActualizarCorrespondenciaCommandValidator : AbstractValidator<ActualizarCorrespondenciaCommand>
{
    public ActualizarCorrespondenciaCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(c => c.Datos).Obligatorio("El destinatario es obligatorio.").Maximo(120);
        RuleFor(c => c.Poblacion).Obligatorio("La población es obligatoria.").Maximo(80);
        RuleFor(c => c.Cp).Obligatorio("El código postal es obligatorio.").CodigoPostal();
        RuleFor(c => c.Direccion).Obligatorio("La dirección es obligatoria.").Maximo(200);
        RuleFor(c => c.Numero).Obligatorio("El número es obligatorio.").Maximo(10);
        RuleFor(c => c.Aclarador).Maximo(60);
    }
}
