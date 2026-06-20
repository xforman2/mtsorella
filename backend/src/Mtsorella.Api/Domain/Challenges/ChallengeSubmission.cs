using ErrorOr;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;

namespace Mtsorella.Api.Domain.Challenges;

// A member's video response to a challenge (aggregate root). A coach reviews it and the score becomes
// points. References Challenge + Member by id (D5) and snapshots the deadline so on-time can be
// computed without loading the Challenge.
//
// O1 (confirmed): one submission per member per challenge. That uniqueness spans aggregates, so it is
// enforced at the application/persistence boundary — this aggregate cannot self-enforce it.
//
// FR-M24–M27 / FR-A6 / BE-17 / BE-27.
public sealed class ChallengeSubmission : AggregateRoot<ChallengeSubmissionId>
{
    public ChallengeId ChallengeId { get; private set; }
    public MemberId MemberId { get; private set; }
    public MediaRef Video { get; private set; }
    public DateTimeOffset SubmittedOn { get; private set; }
    public DateTimeOffset DeadlineSnapshot { get; private set; }
    public SubmissionStatus Status { get; private set; }
    public ChallengeScore? Score { get; private set; }
    public CoachId? ReviewedBy { get; private set; }
    public string? ReviewComment { get; private set; }
    public DateTimeOffset? ReviewedOn { get; private set; }

    private ChallengeSubmission(
        ChallengeSubmissionId id,
        ChallengeId challengeId,
        MemberId memberId,
        MediaRef video,
        DateTimeOffset deadlineSnapshot,
        DateTimeOffset submittedOn)
    {
        Id = id;
        ChallengeId = challengeId;
        MemberId = memberId;
        Video = video;
        DeadlineSnapshot = deadlineSnapshot;
        SubmittedOn = submittedOn;
        Status = SubmissionStatus.Submitted;
    }

    public static ErrorOr<ChallengeSubmission> Submit(
        ChallengeId challengeId,
        MemberId memberId,
        MediaRef video,
        DateTimeOffset deadline,
        DateTimeOffset submittedOn)
    {
        var submission = new ChallengeSubmission(
            ChallengeSubmissionId.New(), challengeId, memberId, video, deadline, submittedOn);
        submission.RaiseDomainEvent(new ChallengeSubmissionCreated(submission.Id, challengeId, memberId));
        return submission;
    }

    // Scores the submission: +10 completion, +5 if it was on time, +0..20 quality. Allowed only while
    // the submission is unreviewed; raises ChallengeSubmissionReviewed carrying the total (FR-A6).
    public ErrorOr<Success> Review(int quality, CoachId by, string? comment, DateTimeOffset reviewedOn)
    {
        if (Status == SubmissionStatus.Reviewed)
        {
            return Error.Conflict("ChallengeSubmission.AlreadyReviewed", "Submission has already been reviewed.");
        }

        bool onTime = SubmittedOn <= DeadlineSnapshot;
        ErrorOr<ChallengeScore> score = ChallengeScore.Create(quality, onTime);
        if (score.IsError)
        {
            return score.Errors;
        }

        Score = score.Value;
        ReviewedBy = by;
        ReviewComment = comment;
        ReviewedOn = reviewedOn;
        Status = SubmissionStatus.Reviewed;
        RaiseDomainEvent(new ChallengeSubmissionReviewed(Id, ChallengeId, MemberId, Score.Total));
        return Result.Success;
    }
}
