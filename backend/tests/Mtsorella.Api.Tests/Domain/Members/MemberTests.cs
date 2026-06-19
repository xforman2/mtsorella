using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;
using Mtsorella.Api.Domain.Members;

namespace Mtsorella.Api.Tests.Domain.Members;

public class MemberTests
{
    private static Member NewMember()
    {
        var email = Email.Create("parent@example.com").Value;
        return Member.Create("Ada Lovelace", MemberCategory.Seniors, email, yearsInTeam: 1).Value;
    }

    [Fact]
    public void Create_valid_member_starts_at_zero_points_and_bronze()
    {
        var member = NewMember();

        Assert.Equal(0, member.Points.Value);
        Assert.Equal(LevelTier.Bronze, member.Level.Tier);
        Assert.True(member.IsActive);
        Assert.Contains(member.DomainEvents, domainEvent => domainEvent is MemberCreated);
    }

    [Fact]
    public void Create_blank_name_is_error()
    {
        var email = Email.Create("parent@example.com").Value;

        var result = Member.Create("  ", MemberCategory.Juniors, email);

        Assert.True(result.IsError);
        Assert.Equal("Member.FullNameRequired", result.FirstError.Code);
    }

    [Fact]
    public void AwardPoints_increases_total_and_records_history()
    {
        var member = NewMember();
        member.ClearDomainEvents();

        member.AwardPoints(50, PointSource.Manual, "Bonus", DateTime.UtcNow);

        Assert.Equal(50, member.Points.Value);
        Assert.Single(member.PointHistory);
        Assert.Equal(50, member.PointHistory[0].Amount);
        Assert.Contains(member.DomainEvents, domainEvent => domainEvent is MemberPointsAwarded);
    }

    [Fact]
    public void AwardPoints_raises_leveled_up_only_when_crossing_a_rung()
    {
        var member = NewMember();

        member.AwardPoints(50, PointSource.Manual, "stays bronze", DateTime.UtcNow);
        Assert.DoesNotContain(member.DomainEvents, domainEvent => domainEvent is MemberLeveledUp);

        member.AwardPoints(60, PointSource.Manual, "crosses to 110 -> Improver", DateTime.UtcNow);
        Assert.Contains(member.DomainEvents, domainEvent => domainEvent is MemberLeveledUp);
    }

    [Fact]
    public void EarnBadge_is_idempotent()
    {
        var member = NewMember();
        var badgeId = BadgeId.New();

        member.EarnBadge(badgeId, DateTime.UtcNow);
        member.EarnBadge(badgeId, DateTime.UtcNow);

        Assert.Single(member.Badges);
        Assert.Equal(1, member.DomainEvents.Count(domainEvent => domainEvent is BadgeEarned));
    }

    [Fact]
    public void RegisterAttendanceActivity_raises_milestone_event_at_threshold()
    {
        var member = NewMember();
        var start = new DateOnly(2026, 6, 1);

        for (int day = 0; day < 7; day++)
        {
            member.RegisterAttendanceActivity(start.AddDays(day));
        }

        Assert.Equal(7, member.Streak.Current);
        Assert.Contains(member.DomainEvents, domainEvent => domainEvent is StreakMilestoneReached);
    }

    [Fact]
    public void UpdateProfile_rejects_blank_name()
    {
        var member = NewMember();
        var email = Email.Create("new@example.com").Value;

        var result = member.UpdateProfile("", null, email, null);

        Assert.True(result.IsError);
    }
}
