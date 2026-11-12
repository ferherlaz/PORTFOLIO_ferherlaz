using System.Net.Mail;
using System.Numerics;
using System.Text.RegularExpressions;

namespace ProyectoPractica.Api.Validation;

/// <summary>
/// Reglas de formato y dígitos de control para documentos y datos españoles.
/// </summary>
public static partial class ValidadoresEspana
{
    private const string LetrasNif = "TRWAGMYFPDXBNJZSQVHLCKE";
    private const string LetrasControlCif = "JABCDEFGHI";

    public static string Normalizar(string? valor) =>
        (valor ?? "").Replace(" ", "").Replace("-", "").Trim().ToUpperInvariant();

    /// <summary>Acepta NIF (DNI), NIE o CIF.</summary>
    public static bool EsDocumentoValido(string? valor)
    {
        var doc = Normalizar(valor);
        return EsNif(doc) || EsNie(doc) || EsCif(doc);
    }

    public static bool EsNif(string doc)
    {
        if (!NifRegex().IsMatch(doc)) return false;
        var numero = int.Parse(doc[..8]);
        return doc[8] == LetrasNif[numero % 23];
    }

    public static bool EsNie(string doc)
    {
        if (!NieRegex().IsMatch(doc)) return false;
        var prefijo = doc[0] switch { 'X' => "0", 'Y' => "1", _ => "2" };
        var numero = int.Parse(prefijo + doc[1..8]);
        return doc[8] == LetrasNif[numero % 23];
    }

    public static bool EsCif(string doc)
    {
        if (!CifRegex().IsMatch(doc)) return false;

        var control = DigitoControlCif(doc[1..8]);
        var letraControl = LetrasControlCif[control];
        var recibido = doc[8];

        return doc[0] switch
        {
            // Entidades que llevan siempre letra de control.
            'N' or 'P' or 'Q' or 'R' or 'S' or 'W' => recibido == letraControl,
            // Entidades que llevan siempre número de control.
            'A' or 'B' or 'E' or 'H' => recibido == (char)('0' + control),
            _ => recibido == letraControl || recibido == (char)('0' + control)
        };
    }

    /// <summary>Calcula el dígito de control de los 7 dígitos centrales de un CIF.</summary>
    public static int DigitoControlCif(string sieteDigitos)
    {
        var suma = 0;
        for (var i = 0; i < 7; i++)
        {
            var d = sieteDigitos[i] - '0';
            if (i % 2 == 0)
            {
                var doble = d * 2;
                suma += doble / 10 + doble % 10;
            }
            else
            {
                suma += d;
            }
        }
        return (10 - suma % 10) % 10;
    }

    public static char LetraNif(int numero) => LetrasNif[numero % 23];

    /// <summary>IBAN genérico (ISO 13616, módulo 97); si es español exige 24 caracteres.</summary>
    public static bool EsIbanValido(string? valor)
    {
        var iban = Normalizar(valor);
        if (!IbanRegex().IsMatch(iban)) return false;
        if (iban.StartsWith("ES") && iban.Length != 24) return false;

        var reordenado = iban[4..] + iban[..4];
        return Modulo97(reordenado) == 1;
    }

    /// <summary>Calcula los dos dígitos de control de un IBAN a partir del país y la cuenta (BBAN).</summary>
    public static string DigitosControlIban(string pais, string bban)
    {
        var resto = Modulo97(bban + pais + "00");
        return (98 - resto).ToString("00");
    }

    private static int Modulo97(string alfanumerico)
    {
        var digitos = string.Concat(alfanumerico.Select(c =>
            char.IsLetter(c) ? (c - 'A' + 10).ToString() : c.ToString()));
        return (int)(BigInteger.Parse(digitos) % 97);
    }

    /// <summary>Código postal español: 5 dígitos y provincia entre 01 y 52.</summary>
    public static bool EsCodigoPostalValido(string? valor)
    {
        var cp = (valor ?? "").Trim();
        if (!CodigoPostalRegex().IsMatch(cp)) return false;
        var provincia = int.Parse(cp[..2]);
        return provincia is >= 1 and <= 52;
    }

    public static bool EsEmailValido(string? valor)
    {
        var email = (valor ?? "").Trim();
        if (email.Length is 0 or > 254 || !EmailRegex().IsMatch(email)) return false;
        return MailAddress.TryCreate(email, out var direccion) && direccion.Address == email;
    }

    public static bool EsTelefonoValido(string? valor) =>
        TelefonoRegex().IsMatch((valor ?? "").Trim());

    [GeneratedRegex(@"^\d{8}[A-Z]$")]
    private static partial Regex NifRegex();

    [GeneratedRegex(@"^[XYZ]\d{7}[A-Z]$")]
    private static partial Regex NieRegex();

    [GeneratedRegex(@"^[ABCDEFGHJNPQRSUVW]\d{7}[0-9A-J]$")]
    private static partial Regex CifRegex();

    [GeneratedRegex(@"^[A-Z]{2}\d{2}[A-Z0-9]{11,30}$")]
    private static partial Regex IbanRegex();

    [GeneratedRegex(@"^\d{5}$")]
    private static partial Regex CodigoPostalRegex();

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$")]
    private static partial Regex EmailRegex();

    [GeneratedRegex(@"^\+?[0-9][0-9 ]{7,16}[0-9]$")]
    private static partial Regex TelefonoRegex();
}
