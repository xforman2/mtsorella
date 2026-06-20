using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mtsorella.Api.Domain.Common.ValueObjects;

namespace Mtsorella.Api.Persistence.Converters;

// DateOfBirth <-> DateOnly. A plain wrapper, so the conversion is a direct unwrap/rewrap.
public sealed class DateOfBirthConverter : ValueConverter<DateOfBirth, DateOnly>
{
    public DateOfBirthConverter()
        : base(dateOfBirth => dateOfBirth.Value, value => new DateOfBirth(value))
    {
    }
}
