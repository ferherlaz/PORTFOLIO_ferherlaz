using FluentValidation;
using ProyectoPractica.Domain.Reglas;

namespace ProyectoPractica.Application.Comun.Validacion;

/// <summary>Reglas reutilizables de FluentValidation basadas en las reglas de negocio del dominio.</summary>
public static class ReglasValidacion
{
    public static IRuleBuilderOptions<T, string?> Obligatorio<T>(this IRuleBuilder<T, string?> regla, string mensaje) =>
        regla.Must(v => !string.IsNullOrWhiteSpace(v)).WithMessage(mensaje);

    public static IRuleBuilderOptions<T, string?> Maximo<T>(this IRuleBuilder<T, string?> regla, int maximo) =>
        regla.Must(v => (v?.Trim().Length ?? 0) <= maximo).WithMessage($"Máximo {maximo} caracteres.");

    public static IRuleBuilderOptions<T, string?> DocumentoIdentidad<T>(this IRuleBuilder<T, string?> regla) =>
        regla.Must(v => string.IsNullOrWhiteSpace(v) || ReglasEspana.EsDocumentoValido(v))
            .WithMessage("El CIF/NIF/NIE no es válido (revisa la letra o el dígito de control).");

    public static IRuleBuilderOptions<T, string?> Iban<T>(this IRuleBuilder<T, string?> regla) =>
        regla.Must(v => string.IsNullOrWhiteSpace(v) || ReglasEspana.EsIbanValido(v))
            .WithMessage("El IBAN no es válido (revisa los dígitos de control).");

    public static IRuleBuilderOptions<T, string?> CodigoPostal<T>(this IRuleBuilder<T, string?> regla) =>
        regla.Must(v => string.IsNullOrWhiteSpace(v) || ReglasEspana.EsCodigoPostalValido(v))
            .WithMessage("El código postal debe tener 5 dígitos y una provincia válida (01-52).");

    public static IRuleBuilderOptions<T, string?> Correo<T>(this IRuleBuilder<T, string?> regla, string mensaje) =>
        regla.Must(v => string.IsNullOrWhiteSpace(v) || ReglasEspana.EsEmailValido(v)).WithMessage(mensaje);

    public static IRuleBuilderOptions<T, string?> Telefono<T>(this IRuleBuilder<T, string?> regla) =>
        regla.Must(v => string.IsNullOrWhiteSpace(v) || ReglasEspana.EsTelefonoValido(v))
            .WithMessage("El teléfono no es válido (solo dígitos, espacios y prefijo +).");
}
