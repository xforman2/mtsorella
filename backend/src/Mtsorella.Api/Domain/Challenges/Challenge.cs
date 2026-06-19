using ErrorOr;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;

namespace Mtsorella.Api.Domain.Challenges;

// A gamification task with an instructional video, deadline, and reward (aggregate root).
// Kept separate from its submissions — different lifecycle and authors (D5). FR-M23 / FR-A7 / BE-11.
public sealed class Challenge : AggregateRoot<ChallengeId>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string? Category { get; private set; }
    public DateTimeOffset Deadline { get; private set; }
    public int CompletionPoints { get; private set; }
    public MediaRef InstructionalVideo { get; private set; }
    public CoachId CreatedBy { get; private set; }
    public bool IsActive { get; private set; }

    private Challenge(
        ChallengeId id,
        string name,
        string description,
        string? category,
        DateTimeOffset deadline,
        int completionPoints,
        MediaRef instructionalVideo,
        CoachId createdBy)
    {
        Id = id;
        Name = name;
        Description = description;
        Category = category;
        Deadline = deadline;
        CompletionPoints = completionPoints;
        InstructionalVideo = instructionalVideo;
        CreatedBy = createdBy;
        IsActive = true;
    }

    public static ErrorOr<Challenge> Create(
        string name,
        string description,
        DateTimeOffset deadline,
        MediaRef instructionalVideo,
        CoachId createdBy,
        string? category = null,
        int completionPoints = ChallengeScore.CompletionPoints)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Error.Validation("Challenge.NameRequired", "Challenge name is required.");
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            return Error.Validation("Challenge.DescriptionRequired", "Challenge description is required.");
        }

        if (completionPoints < 0)
        {
            return Error.Validation("Challenge.CompletionPointsNegative", "Completion points cannot be negative.");
        }

        var challenge = new Challenge(
            ChallengeId.New(), name.Trim(), description.Trim(), category,
            deadline, completionPoints, instructionalVideo, createdBy);
        challenge.RaiseDomainEvent(new ChallengeCreated(challenge.Id, createdBy));
        return challenge;
    }

    public void Close() => IsActive = false;
}
