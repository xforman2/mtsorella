using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mtsorella.Api.Domain.Common.ValueObjects;

namespace Mtsorella.Api.Persistence.Converters;

// Email <-> string. The stored value was validated by Email.Create on write, so reconstruction trusts it.
public sealed class EmailConverter : ValueConverter<Email, string>
{
    public EmailConverter()
        : base(email => email.Value, value => Email.Create(value).Value)
    {
    }
}
