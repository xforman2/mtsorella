using ErrorOr;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;

namespace Mtsorella.Api.Domain.TeamGoals;

// A shared points target (aggregate root). Progress is a synced snapshot of Σ member points kept via
// events (D5/D6), not a live cross-aggregate read. History = past Completed goals. FR-P6 / FR-M32–M33
// / FR-A12 / BE-19.
public sealed class TeamGoal : AggregateRoot<TeamGoalId>
{
    public string Title { get; private set; }
    public Points Target { get; private set; }
    public Points Progress { get; private set; }
    public TeamGoalStatus Status { get; private set; }
    public DateOnly StartedOn { get; private set; }
    public DateOnly? CompletedOn { get; private set; }

    private TeamGoal(TeamGoalId id, string title, Points target, DateOnly startedOn)
    {
        Id = id;
        Title = title;
        Target = target;
        Progress = Points.Zero;
        Status = TeamGoalStatus.Active;
        StartedOn = startedOn;
    }

    public static ErrorOr<TeamGoal> Create(string title, Points target, DateOnly startedOn)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return Error.Validation("TeamGoal.TitleRequired", "Title is required.");
        }

        if (target.Value <= 0)
        {
            return Error.Validation("TeamGoal.TargetNotPositive", "Target must be greater than zero.");
        }

        return new TeamGoal(TeamGoalId.New(), title.Trim(), target, startedOn);
    }

    // Syncs the progress snapshot; auto-completes (once) when the target is reached (→ TeamGoalCompleted).
    public void RecordProgress(Points current, DateOnly on)
    {
        if (Status == TeamGoalStatus.Completed)
        {
            return;
        }

        Progress = current;

        if (Progress.Value >= Target.Value)
        {
            Status = TeamGoalStatus.Completed;
            CompletedOn = on;
            RaiseDomainEvent(new TeamGoalCompleted(Id));
        }
    }
}

public enum TeamGoalStatus
{
    Active,
    Completed
}
