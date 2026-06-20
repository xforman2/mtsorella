using ErrorOr;

namespace Mtsorella.Api.Domain.Common.ValueObjects;

// FR-P29 / FR-P32 / FR-P27. Permissive on format (international numbers vary); normalizes to an
// optional leading '+' followed by digits, and guards a sane digit count.
public sealed record PhoneNumber
{
    public string Value { get; }

    private PhoneNumber(string value) => Value = value;

    public static ErrorOr<PhoneNumber> Create(string? raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
        {
            return Error.Validation("PhoneNumber.Empty", "Phone number is required.");
        }

        string normalized = Normalize(raw);
        int digitCount = normalized.Count(char.IsDigit);

        return digitCount is >= 6 and <= 15
            ? new PhoneNumber(normalized)
            : Error.Validation("PhoneNumber.Invalid", "Phone number is not valid.");
    }

    private static string Normalize(string raw)
    {
        string trimmed = raw.Trim();
        bool hasPlus = trimmed.StartsWith('+');
        string digits = new string(trimmed.Where(char.IsDigit).ToArray());
        return hasPlus ? "+" + digits : digits;
    }

    public override string ToString() => Value;
}
