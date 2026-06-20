using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;
using Mtsorella.Api.Domain.Members;
using Mtsorella.Api.Persistence.Outbox;

namespace Mtsorella.Api.Tests.Persistence.Outbox;

public class ConvertDomainEventsToOutboxInterceptorTests
{
    private static Member NewMemberWithPoints()
    {
        var email = Email.Create("parent@example.com").Value;
        var member = Member.Create("Ada Lovelace", MemberCategory.Seniors, email).Value; // raises MemberCreated
        member.AwardPoints(110, PointSource.Manual, "crosses to Improver", DateTime.UtcNow); // + Awarded + LeveledUp
        return member;
    }

    [Fact]
    public async Task Save_writes_an_outbox_row_per_raised_event()
    {
        using var db = new SqliteAppDbContext(new ConvertDomainEventsToOutboxInterceptor());
        var member = NewMemberWithPoints();

        db.Context.Members.Add(member);
        await db.Context.SaveChangesAsync(CancellationToken.None);

        var messages = db.Context.OutboxMessages.ToList();
        Assert.Equal(3, messages.Count);
        Assert.Contains(messages, m => m.EventType.Contains(nameof(MemberCreated)));
        Assert.Contains(messages, m => m.EventType.Contains(nameof(MemberPointsAwarded)));
        Assert.Contains(messages, m => m.EventType.Contains(nameof(MemberLeveledUp)));
        Assert.All(messages, m => Assert.Null(m.ProcessedOnUtc));
        Assert.All(messages, m => Assert.False(string.IsNullOrWhiteSpace(m.Payload)));
    }

    [Fact]
    public async Task Save_clears_the_aggregate_domain_events()
    {
        using var db = new SqliteAppDbContext(new ConvertDomainEventsToOutboxInterceptor());
        var member = NewMemberWithPoints();

        db.Context.Members.Add(member);
        await db.Context.SaveChangesAsync(CancellationToken.None);

        Assert.Empty(member.DomainEvents);
    }

    [Fact]
    public async Task Save_without_events_writes_no_outbox_rows()
    {
        using var db = new SqliteAppDbContext(new ConvertDomainEventsToOutboxInterceptor());
        var member = NewMemberWithPoints();
        db.Context.Members.Add(member);
        await db.Context.SaveChangesAsync(CancellationToken.None); // drains the creation events

        // A second save that changes state but raises nothing new must not add outbox rows.
        member.Deactivate();
        await db.Context.SaveChangesAsync(CancellationToken.None);

        Assert.Equal(3, db.Context.OutboxMessages.Count());
    }
}
