namespace Mtsorella.Api.Domain.Common.ValueObjects;

// Consecutive-activity streak with milestone detection (FR-M42; prototype shows streak: 12).
public readonly record struct Streak(int Current, int Longest, DateOnly? LastActiveOn)
{
    // O3 (assumption): milestone thresholds in days; whether they grant badges is a later decision.
    private static readonly int[] Milestones = [7, 30, 100];

    public static Streak None => new(0, 0, null);

    // Register activity on `day`: extend the run on the next consecutive day, ignore a same-or-earlier
    // day (no double counting), otherwise reset the run to 1.
    public Streak Register(DateOnly day)
    {
        if (LastActiveOn is null)
        {
            return new Streak(1, Math.Max(Longest, 1), day);
        }

        if (day <= LastActiveOn.Value)
        {
            return this;
        }

        int current = day == LastActiveOn.Value.AddDays(1) ? Current + 1 : 1;
        return new Streak(current, Math.Max(Longest, current), day);
    }

    // True when Current has reached a milestone that previousCurrent had not yet reached.
    public bool CrossedMilestone(int previousCurrent)
    {
        int current = Current;
        return Milestones.Any(milestone => previousCurrent < milestone && current >= milestone);
    }
}
