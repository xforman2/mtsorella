using ErrorOr;
using Mtsorella.Api.Domain.Common;

namespace Mtsorella.Api.Domain.Performances;

// An upcoming public event for the calendar (aggregate root). The .ics export is infrastructure,
// kept as a boundary (§8). FR-P21–P23 / FR-A9 / BE-9.
public sealed class Performance : AggregateRoot<PerformanceId>
{
    public string Name { get; private set; }
    public DateTimeOffset StartsAt { get; private set; }
    public string Location { get; private set; }
    public string Type { get; private set; }

    private Performance(PerformanceId id, string name, DateTimeOffset startsAt, string location, string type)
    {
        Id = id;
        Name = name;
        StartsAt = startsAt;
        Location = location;
        Type = type;
    }

    public static ErrorOr<Performance> Create(string name, DateTimeOffset startsAt, string location, string type)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Error.Validation("Performance.NameRequired", "Name is required.");
        }

        if (string.IsNullOrWhiteSpace(location))
        {
            return Error.Validation("Performance.LocationRequired", "Location is required.");
        }

        if (string.IsNullOrWhiteSpace(type))
        {
            return Error.Validation("Performance.TypeRequired", "Type is required.");
        }

        return new Performance(PerformanceId.New(), name.Trim(), startsAt, location.Trim(), type.Trim());
    }
}
