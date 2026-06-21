namespace Mtsorella.Api.Domain.Common.ValueObjects;

// A training's schedule pattern (FR-A8). Days is an ordered collection rather than a set: EF Core stores it
// as a JSON array inside the Recurrence column, and its change tracker can only snapshot arrays/ordered
// lists, not sets. Callers should pass distinct, ascending days.
public sealed record Recurrence(RecurrenceFrequency Frequency, IReadOnlyList<DayOfWeek> Days, DateOnly? Until)
{
    public static Recurrence None => new(RecurrenceFrequency.None, [], null);
}

public enum RecurrenceFrequency
{
    None,
    Weekly
}
