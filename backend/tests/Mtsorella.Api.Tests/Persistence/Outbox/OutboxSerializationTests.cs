using System.Text.Json;
using Mtsorella.Api.Domain.Announcements;
using Mtsorella.Api.Domain.Challenges;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Inbox;
using Mtsorella.Api.Domain.Members;
using Mtsorella.Api.Domain.TeamGoals;
using Mtsorella.Api.Domain.Trainings;
// Disambiguate from System.ApplicationId, which ImplicitUsings brings into scope via `using System;`.
using ApplicationId = Mtsorella.Api.Domain.Common.ApplicationId;

namespace Mtsorella.Api.Tests.Persistence.Outbox;

// Guards the wire format the outbox relies on: every domain event must survive serialize -> store EventType
// -> resolve the type -> deserialize, including strongly-typed ids (record structs), enum and
// DateTimeOffset payloads. Every IDomainEvent is covered, since any of them can land in the outbox.
public class OutboxSerializationTests
{
    public static TheoryData<IDomainEvent> Events =>
    [
        new MemberCreated(MemberId.New()),
        new MemberPointsAwarded(MemberId.New(), 50, 110),
        new MemberLeveledUp(MemberId.New(), 2, LevelName.Improver),
        new BadgeEarned(MemberId.New(), BadgeId.New()),
        new StreakMilestoneReached(MemberId.New(), 7),
        new TrainingScheduled(TrainingId.New(), DateTimeOffset.UtcNow),
        new TrainingAttendanceConfirmed(TrainingId.New(), MemberId.New(), 10),
        new ChallengeCreated(ChallengeId.New(), CoachId.New()),
        new ChallengeSubmissionCreated(ChallengeSubmissionId.New(), ChallengeId.New(), MemberId.New()),
        new ChallengeSubmissionReviewed(ChallengeSubmissionId.New(), ChallengeId.New(), MemberId.New(), 30),
        new AnnouncementPublished(AnnouncementId.New(), CoachId.New()),
        new AnnouncementPinned(AnnouncementId.New()),
        new ApplicationSubmitted(ApplicationId.New()),
        new TeamGoalCompleted(TeamGoalId.New()),
    ];

    private static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web);

    [Theory]
    [MemberData(nameof(Events))]
    public void Round_trips_through_type_name_and_json(IDomainEvent domainEvent)
    {
        var eventTypeName = domainEvent.GetType().AssemblyQualifiedName!;
        var payload = JsonSerializer.Serialize(domainEvent, domainEvent.GetType(), Options);

        var resolvedType = Type.GetType(eventTypeName);
        Assert.NotNull(resolvedType);
        var rehydrated = JsonSerializer.Deserialize(payload, resolvedType, Options);

        Assert.Equal(domainEvent, rehydrated);
    }
}
