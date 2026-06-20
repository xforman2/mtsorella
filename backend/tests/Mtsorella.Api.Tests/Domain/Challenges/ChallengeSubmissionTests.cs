using Mtsorella.Api.Domain.Challenges;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;

namespace Mtsorella.Api.Tests.Domain.Challenges;

public class ChallengeSubmissionTests
{
    private static ChallengeSubmission NewSubmission(DateTimeOffset? submittedOn = null, DateTimeOffset? deadline = null)
    {
        var video = new MediaRef(MediaKind.Video, "videos/abc");
        var when = submittedOn ?? DateTimeOffset.UtcNow;
        var due = deadline ?? DateTimeOffset.UtcNow.AddDays(1);
        return ChallengeSubmission.Submit(ChallengeId.New(), MemberId.New(), video, due, when).Value;
    }

    [Fact]
    public void Submit_starts_submitted_and_raises_event()
    {
        var submission = NewSubmission();

        Assert.Equal(SubmissionStatus.Submitted, submission.Status);
        Assert.Contains(submission.DomainEvents, domainEvent => domainEvent is ChallengeSubmissionCreated);
    }

    [Fact]
    public void Review_on_time_totals_completion_bonus_and_quality_and_raises_event()
    {
        var now = DateTimeOffset.UtcNow;
        var submission = NewSubmission(submittedOn: now, deadline: now.AddDays(1));
        submission.ClearDomainEvents();

        var result = submission.Review(20, CoachId.New(), "great", now.AddDays(2));

        Assert.False(result.IsError);
        Assert.Equal(SubmissionStatus.Reviewed, submission.Status);
        Assert.Equal(35, submission.Score!.Total); // 10 + 5 + 20

        var reviewed = Assert.IsType<ChallengeSubmissionReviewed>(Assert.Single(submission.DomainEvents));
        Assert.Equal(35, reviewed.AwardedPoints);
    }

    [Fact]
    public void Review_late_submission_has_no_on_time_bonus()
    {
        var now = DateTimeOffset.UtcNow;
        var submission = NewSubmission(submittedOn: now, deadline: now.AddDays(-1));

        submission.Review(10, CoachId.New(), null, now);

        Assert.Equal(20, submission.Score!.Total); // 10 + 0 + 10
    }

    [Fact]
    public void Review_rejects_quality_out_of_range()
    {
        var submission = NewSubmission();

        var result = submission.Review(21, CoachId.New(), null, DateTimeOffset.UtcNow);

        Assert.True(result.IsError);
    }

    [Fact]
    public void Review_twice_is_a_conflict()
    {
        var submission = NewSubmission();
        submission.Review(10, CoachId.New(), null, DateTimeOffset.UtcNow);

        var second = submission.Review(10, CoachId.New(), null, DateTimeOffset.UtcNow);

        Assert.True(second.IsError);
        Assert.Equal("ChallengeSubmission.AlreadyReviewed", second.FirstError.Code);
    }
}
