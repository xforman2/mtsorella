using ErrorOr;

namespace Mtsorella.Api.Domain.Common.ValueObjects;

// Validated e-mail — member login identity and the public forms.
// FR-M1 (login) / FR-P26 (contact) / FR-P29 (application) / FR-P32 (partnership).
public sealed record Email
{
    public string Value { get; }

    private Email(string value) => Value = value;

    public static ErrorOr<Email> Create(string? raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
        {
            return Error.Validation("Email.Empty", "E-mail is required.");
        }

        string trimmed = raw.Trim();
        int at = trimmed.IndexOf('@');

        // Minimal structural check ("local@domain.tld", single '@', no spaces). A stricter policy is a
        // separate concern; the domain only guarantees the value is shaped like an address.
        bool looksValid = at > 0
            && at < trimmed.Length - 1
            && trimmed.IndexOf('@', at + 1) < 0
            && trimmed.LastIndexOf('.') > at + 1
            && !trimmed.Contains(' ');

        return looksValid
            ? new Email(trimmed)
            : Error.Validation("Email.Invalid", "E-mail is not valid.");
    }

    public override string ToString() => Value;
}
