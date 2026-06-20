using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mtsorella.Api.Domain.Common.ValueObjects;

namespace Mtsorella.Api.Persistence.Converters;

// Points <-> int. The non-negative invariant held on write, so Points.From always succeeds here.
public sealed class PointsConverter : ValueConverter<Points, int>
{
    public PointsConverter()
        : base(points => points.Value, value => Points.From(value).Value)
    {
    }
}
