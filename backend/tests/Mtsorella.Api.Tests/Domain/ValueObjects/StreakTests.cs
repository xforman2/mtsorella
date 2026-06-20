using Mtsorella.Api.Domain.Common.ValueObjects;

namespace Mtsorella.Api.Tests.Domain.ValueObjects;

public class StreakTests
{
    [Fact]
    public void Register_first_day_starts_at_one()
    {
        var streak = Streak.None.Register(new DateOnly(2026, 6, 1));

        Assert.Equal(1, streak.Current);
        Assert.Equal(1, streak.Longest);
    }

    [Fact]
    public void Register_consecutive_day_increments()
    {
        var streak = Streak.None
            .Register(new DateOnly(2026, 6, 1))
            .Register(new DateOnly(2026, 6, 2));

        Assert.Equal(2, streak.Current);
        Assert.Equal(2, streak.Longest);
    }

    [Fact]
    public void Register_same_day_is_noop()
    {
        var day = new DateOnly(2026, 6, 1);
        var streak = Streak.None.Register(day).Register(day);

        Assert.Equal(1, streak.Current);
    }

    [Fact]
    public void Register_gap_resets_current_but_keeps_longest()
    {
        var streak = Streak.None
            .Register(new DateOnly(2026, 6, 1))
            .Register(new DateOnly(2026, 6, 2))
            .Register(new DateOnly(2026, 6, 5));

        Assert.Equal(1, streak.Current);
        Assert.Equal(2, streak.Longest);
    }

    [Fact]
    public void CrossedMilestone_is_true_only_when_threshold_newly_reached()
    {
        var streak = new Streak(7, 7, new DateOnly(2026, 6, 7));

        Assert.True(streak.CrossedMilestone(6));
        Assert.False(streak.CrossedMilestone(7));
    }
}
