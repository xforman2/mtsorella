using System.Text.Json;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Members;

namespace Mtsorella.Api.Tests.Persistence.Outbox;

// Guards the wire format the outbox relies on: every event must survive serialize -> store EventType ->
// resolve the type -> deserialize, including strongly-typed ids (record structs) and enum payloads.
public class OutboxSerializationTests
{
    public static TheoryData<IDomainEvent> Events =>
    [
        new MemberCreated(MemberId.New()),
        new MemberPointsAwarded(MemberId.New(), 50, 110),
        new MemberLeveledUp(MemberId.New(), 2, LevelName.Improver),
        new BadgeEarned(MemberId.New(), BadgeId.New()),
        new StreakMilestoneReached(MemberId.New(), 7),
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
