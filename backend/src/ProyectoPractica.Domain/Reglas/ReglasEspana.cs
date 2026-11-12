using System.Net.Mail;
using System.Numerics;
using System.Text.RegularExpressions;
using ProyectoPractica.Domain.Comun;

namespace ProyectoPractica.Domain.Reglas;

/// <summary>
/// Reglas de negocio sobre datos españoles: dígitos y letras de control de NIF/NIE/CIF,
/// IBAN (ISO 13616, módulo 97), código postal, email y teléfono.
/// </summary>
public static partial class ReglasEspana
{
    private const string LetrasNif = "TRWAGMYFPDXBNJZSQVHLCKE";
    private const string LetrasControlCif = "JABCDEFGHI";

    /// <summary>Acepta NIF (DNI), NIE o CIF.</summary>
    public static bool EsDocumentoValido(string? valor)
    {
        var documento = Texto.Codigo(valor);
        return EsNif(documento) || EsNie(documento) || EsCif(documento);
    }

    public static bool EsNif(string documento)
    {
        if (!NifRegex().IsMatch(documento)) return false;
        return documento[8] == LetraNif(int.Parse(documento[..8]));
    }

    public static bool EsNie(string documento)
    {
        if (!NieRegex().IsMatch(documento)) return false;
        var prefijo = documento[0] switch { 'X' => "0", 'Y' => "1", _ => "2" };
        return documento[8] == LetraNif(int.Parse(prefijo + documento[1..8]));
    }

    public static bool EsCif(string documento)
    {
        if (!CifRegex().IsMatch(documento)) return false;

        var control = DigitoControlCif(documento[1..8]);
        var letraControl = LetrasControlCif[control];
        var digitoControl = (char)('0' + control);
        var recibido = documento[8];

        return documento[0] switch
        {
            // Entidades que llevan siempre letra de control.
            'N' or 'P' or 'Q' or 'R' or 'S' or 'W' => recibido == letraControl,
            // Entidades que llevan siempre número de control.
            'A' or 'B' or 'E' or 'H' => recibido == digitoControl,
            _ => recibido == letraControl || recibido == digitoControl,
        };
    }

    public static char LetraNif(int numero) => LetrasNif[numero % 23];

    /// <summary>Dígito de control de los 7 dígitos centrales de un CIF.</summary>
    public static int DigitoControlCif(string sieteDigitos)
    {
        var suma = 0;
        for (var i = 0; i < 7; i++)
        {
            var digito = sieteDigitos[i] - '0';
            if (i % 2 == 0)
            {
                var doble = digito * 2;
                suma += doble / 10 + doble % 10;
            }
            else
            {
                suma += digito;
            }
        }
        return (10 - suma % 10) % 10;
    }

    /// <summary>IBAN genérico (módulo 97); si es español exige 24 caracteres.</summary>
    public static bool EsIbanValido(string? valor)
    {
        var iban = Texto.Codigo(valor);
        if (!IbanRegex().IsMatch(iban)) return false;
        if (iban.StartsWith("ES", StringComparison.Ordinal) && iban.Length != 24) return false;
        return Modulo97(iban[4..] + iban[..4]) == 1;
    }

    /// <summary>Dígitos de control de un IBAN a partir del país y la cuenta (BBAN).</summary>
    public static string DigitosControlIban(string pais, string bban) =>
        (98 - Modulo97(bban + pais + "00")).ToString("00");

    /// <summary>Código postal español: 5 dígitos y provincia entre 01 y 52.</summary>
    public static bool EsCodigoPostalValido(string? valor)
    {
        var cp = Texto.Limpio(valor);
        return CodigoPostalRegex().IsMatch(cp) && int.Parse(cp[..2]) is >= 1 and <= 52;
    }

    public static bool EsEmailValido(string? valor)
    {
        var email = Texto.Limpio(valor);
        if (email.Length is 0 or > 254 || !EmailRegex().IsMatch(email)) return false;
        return MailAddress.TryCreate(email, out var direccion) && direccion.Address == email;
    }

    public static bool EsTelefonoValido(string? valor) => TelefonoRegex().IsMatch(Texto.Limpio(valor));

    private static int Modulo97(string alfanumerico)
    {
        var digitos = string.Concat(alfanumerico.Select(c =>
            char.IsLetter(c) ? (c - 'A' + 10).ToString() : c.ToString()));
        return (int)(BigInteger.Parse(digitos) % 97);
    }

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
