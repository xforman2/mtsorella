using Mtsorella.Api.Domain.Common.ValueObjects;
using Mtsorella.Api.Domain.TeamGoals;

namespace Mtsorella.Api.Tests.Domain.TeamGoals;

public class TeamGoalTests
{
    private static TeamGoal NewGoal(int target = 100) =>
        TeamGoal.Create("Season goal", Points.From(target).Value, new DateOnly(2026, 1, 1)).Value;

    [Fact]
    public void Create_rejects_non_positive_target()
    {
        var result = TeamGoal.Create("Season goal", Points.Zero, new DateOnly(2026, 1, 1));

        Assert.True(result.IsError);
        Assert.Equal("TeamGoal.TargetNotPositive", result.FirstError.Code);
    }

    [Fact]
    public void RecordProgress_below_target_stays_active()
    {
        var goal = NewGoal(100);

        goal.RecordProgress(Points.From(50).Value, new DateOnly(2026, 2, 1));

        Assert.Equal(TeamGoalStatus.Active, goal.Status);
        Assert.Null(goal.CompletedOn);
    }

    [Fact]
    public void RecordProgress_reaching_target_completes_and_raises_event()
    {
        var goal = NewGoal(100);

        goal.RecordProgress(Points.From(120).Value, new DateOnly(2026, 3, 1));

        Assert.Equal(TeamGoalStatus.Completed, goal.Status);
        Assert.Equal(new DateOnly(2026, 3, 1), goal.CompletedOn);
        Assert.Contains(goal.DomainEvents, domainEvent => domainEvent is TeamGoalCompleted);
    }

    [Fact]
    public void RecordProgress_after_completion_is_a_noop()
    {
        var goal = NewGoal(100);
        goal.RecordProgress(Points.From(120).Value, new DateOnly(2026, 3, 1));
        goal.ClearDomainEvents();

        goal.RecordProgress(Points.From(200).Value, new DateOnly(2026, 4, 1));

        Assert.Empty(goal.DomainEvents);
        Assert.Equal(new DateOnly(2026, 3, 1), goal.CompletedOn);
    }
}
