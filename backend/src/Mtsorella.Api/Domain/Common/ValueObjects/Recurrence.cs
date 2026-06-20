namespace Mtsorella.Api.Domain.Common.ValueObjects;

// A training's schedule pattern (FR-A8).
public sealed record Recurrence(RecurrenceFrequency Frequency, IReadOnlySet<DayOfWeek> Days, DateOnly? Until)
{
    public static Recurrence None => new(RecurrenceFrequency.None, new HashSet<DayOfWeek>(), null);
}

public enum RecurrenceFrequency
{
    None,
    Weekly
}
