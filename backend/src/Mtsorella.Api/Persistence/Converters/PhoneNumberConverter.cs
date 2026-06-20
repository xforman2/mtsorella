using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mtsorella.Api.Domain.Common.ValueObjects;

namespace Mtsorella.Api.Persistence.Converters;

// PhoneNumber <-> string. Stored value was normalized/validated by PhoneNumber.Create on write.
public sealed class PhoneNumberConverter : ValueConverter<PhoneNumber, string>
{
    public PhoneNumberConverter()
        : base(phone => phone.Value, value => PhoneNumber.Create(value).Value)
    {
    }
}
