using ProyectoPractica.Api.Contracts;
using static ProyectoPractica.Api.Validation.ValidadoresEspana;

namespace ProyectoPractica.Api.Validation;

/// <summary>
/// Validación de cada bloque del detalle. Las claves del diccionario coinciden con los
/// nombres de campo en JSON (camelCase), para que el frontend pinte el error bajo cada input.
/// </summary>
public static class ValidadoresPoliza
{
    public static Dictionary<string, string[]> Validar(TitularDto dto) => new Errores()
        .Requerido("cif", dto.Cif, "El CIF/NIF es obligatorio.")
        .Si("cif", !string.IsNullOrWhiteSpace(dto.Cif) && !EsDocumentoValido(dto.Cif),
            "El CIF/NIF/NIE no es válido (revisa la letra o el dígito de control).")
        .Requerido("nombre", dto.Nombre, "El nombre es obligatorio.")
        .Longitud("nombre", dto.Nombre, 120)
        .Requerido("dirFiscal", dto.DirFiscal, "La dirección fiscal es obligatoria.")
        .Longitud("dirFiscal", dto.DirFiscal, 200)
        .Resultado;

    public static Dictionary<string, string[]> Validar(FacturacionDto dto) => new Errores()
        .Requerido("dirFacturacion", dto.DirFacturacion, "La dirección es obligatoria.")
        .Longitud("dirFacturacion", dto.DirFacturacion, 200)
        .Requerido("numero", dto.Numero, "El número es obligatorio.")
        .Longitud("numero", dto.Numero, 10)
        .Requerido("ciudad", dto.Ciudad, "La ciudad es obligatoria.")
        .Longitud("ciudad", dto.Ciudad, 80)
        .CodigoPostal("cp", dto.Cp)
        .Resultado;

    public static Dictionary<string, string[]> Validar(PagoDto dto) => new Errores()
        .Requerido("nombre", dto.Nombre, "El nombre del pagador es obligatorio.")
        .Longitud("nombre", dto.Nombre, 120)
        .Requerido("cif", dto.Cif, "El CIF/NIF es obligatorio.")
        .Si("cif", !string.IsNullOrWhiteSpace(dto.Cif) && !EsDocumentoValido(dto.Cif),
            "El CIF/NIF/NIE no es válido (revisa la letra o el dígito de control).")
        .Requerido("tipoCobro", dto.TipoCobro, "El tipo de cobro es obligatorio.")
        .Requerido("iban", dto.Iban, "El IBAN es obligatorio.")
        .Si("iban", !string.IsNullOrWhiteSpace(dto.Iban) && !EsIbanValido(dto.Iban),
            "El IBAN no es válido (revisa los dígitos de control).")
        .Longitud("banco", dto.Banco, 80)
        .Resultado;

    public static Dictionary<string, string[]> Validar(CorrespondenciaDto dto) => new Errores()
        .Requerido("datos", dto.Datos, "El destinatario es obligatorio.")
        .Longitud("datos", dto.Datos, 120)
        .Requerido("poblacion", dto.Poblacion, "La población es obligatoria.")
        .Longitud("poblacion", dto.Poblacion, 80)
        .CodigoPostal("cp", dto.Cp)
        .Requerido("direccion", dto.Direccion, "La dirección es obligatoria.")
        .Longitud("direccion", dto.Direccion, 200)
        .Requerido("numero", dto.Numero, "El número es obligatorio.")
        .Longitud("numero", dto.Numero, 10)
        .Longitud("aclarador", dto.Aclarador, 60)
        .Resultado;

    public static Dictionary<string, string[]> Validar(ContactoDto dto) => new Errores()
        .Requerido("correoPrincipal", dto.CorreoPrincipal, "El correo principal es obligatorio.")
        .Si("correoPrincipal", !string.IsNullOrWhiteSpace(dto.CorreoPrincipal) && !EsEmailValido(dto.CorreoPrincipal),
            "El correo principal no tiene un formato válido.")
        .Si("correoSecundario", !string.IsNullOrWhiteSpace(dto.CorreoSecundario) && !EsEmailValido(dto.CorreoSecundario),
            "El correo secundario no tiene un formato válido.")
        .Requerido("telefono", dto.Telefono, "El teléfono es obligatorio.")
        .Si("telefono", !string.IsNullOrWhiteSpace(dto.Telefono) && !EsTelefonoValido(dto.Telefono),
            "El teléfono no es válido (solo dígitos, espacios y prefijo +).")
        .Resultado;

    /// <summary>Acumulador de errores por campo.</summary>
    internal sealed class Errores
    {
        public Dictionary<string, string[]> Resultado { get; } = [];

        public Errores Si(string campo, bool condicion, string mensaje)
        {
            if (condicion)
            {
                Resultado[campo] = Resultado.TryGetValue(campo, out var previos)
                    ? [.. previos, mensaje]
                    : [mensaje];
            }
            return this;
        }

        public Errores Requerido(string campo, string? valor, string mensaje) =>
            Si(campo, string.IsNullOrWhiteSpace(valor), mensaje);

        public Errores Longitud(string campo, string? valor, int maximo) =>
            Si(campo, (valor?.Trim().Length ?? 0) > maximo, $"Máximo {maximo} caracteres.");

        public Errores CodigoPostal(string campo, string? valor) => this
            .Requerido(campo, valor, "El código postal es obligatorio.")
            .Si(campo, !string.IsNullOrWhiteSpace(valor) && !EsCodigoPostalValido(valor),
                "El código postal debe tener 5 dígitos y una provincia válida (01-52).");
    }
}
