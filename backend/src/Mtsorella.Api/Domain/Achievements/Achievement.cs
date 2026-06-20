using ErrorOr;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;

namespace Mtsorella.Api.Domain.Achievements;

// A public award on the timeline (aggregate root). FR-P14–P17 / FR-A11 / BE-8.
public sealed class Achievement : AggregateRoot<AchievementId>
{
    public int Year { get; private set; }
    public CompetitionType CompetitionType { get; private set; }
    public string Name { get; private set; }
    public Placement Placement { get; private set; }
    public Medal Medal { get; private set; }
    public string Description { get; private set; }

    private Achievement(
        AchievementId id,
        int year,
        CompetitionType competitionType,
        string name,
        Placement placement,
        Medal medal,
        string description)
    {
        Id = id;
        Year = year;
        CompetitionType = competitionType;
        Name = name;
        Placement = placement;
        Medal = medal;
        Description = description;
    }

    public static ErrorOr<Achievement> Create(
        int year,
        CompetitionType competitionType,
        string name,
        Placement placement,
        Medal medal,
        string description)
    {
        ErrorOr<Success> validation = Validate(year, name);
        if (validation.IsError)
        {
            return validation.Errors;
        }

        return new Achievement(AchievementId.New(), year, competitionType, name.Trim(), placement, medal, description?.Trim() ?? string.Empty);
    }

    public ErrorOr<Updated> Edit(
        int year,
        CompetitionType competitionType,
        string name,
        Placement placement,
        Medal medal,
        string description)
    {
        ErrorOr<Success> validation = Validate(year, name);
        if (validation.IsError)
        {
            return validation.Errors;
        }

        Year = year;
        CompetitionType = competitionType;
        Name = name.Trim();
        Placement = placement;
        Medal = medal;
        Description = description?.Trim() ?? string.Empty;
        return Result.Updated;
    }

    private static ErrorOr<Success> Validate(int year, string name)
    {
        if (year <= 0)
        {
            return Error.Validation("Achievement.InvalidYear", "Year must be a positive number.");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            return Error.Validation("Achievement.NameRequired", "Name is required.");
        }

        return Result.Success;
    }
}
