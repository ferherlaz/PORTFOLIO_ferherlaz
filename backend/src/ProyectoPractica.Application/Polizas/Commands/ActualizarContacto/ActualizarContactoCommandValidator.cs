using FluentValidation;
using ProyectoPractica.Application.Comun.Validacion;

namespace ProyectoPractica.Application.Polizas.Commands.ActualizarContacto;

public sealed class ActualizarContactoCommandValidator : AbstractValidator<ActualizarContactoCommand>
{
    public ActualizarContactoCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(c => c.CorreoPrincipal)
            .Obligatorio("El correo principal es obligatorio.")
            .Correo("El correo principal no tiene un formato válido.");
        RuleFor(c => c.CorreoSecundario).Correo("El correo secundario no tiene un formato válido.");
        RuleFor(c => c.Telefono).Obligatorio("El teléfono es obligatorio.").Telefono();
    }
}
