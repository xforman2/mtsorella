using ErrorOr;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;

namespace Mtsorella.Api.Domain.Members;

// The gamified team member (aggregate root). Created by an admin only; accumulates points, levels up,
// and earns badges. Points/Level/Streak are encapsulated and mutated only through the methods below.
// FR-M11–M13 / FR-M34–M42 / BE-6 / BE-15.
public sealed class Member : AggregateRoot<MemberId>
{
    public string FullName { get; private set; }
    public string? Nickname { get; private set; }
    public MemberCategory Category { get; private set; }
    public string? TeamRole { get; private set; }
    public int YearsInTeam { get; private set; }
    public string? Bio { get; private set; }
    public Email ParentEmail { get; private set; }
    public MediaRef? Photo { get; private set; }
    public Points Points { get; private set; }
    public Level Level => Level.For(Points);
    public Streak Streak { get; private set; }
    public bool IsActive { get; private set; }

    private readonly List<PointTransaction> _pointHistory = new();
    public IReadOnlyList<PointTransaction> PointHistory => _pointHistory;

    private readonly List<EarnedBadge> _badges = new();
    public IReadOnlyList<EarnedBadge> Badges => _badges;

    private Member(
        MemberId id,
        string fullName,
        MemberCategory category,
        Email parentEmail,
        string? nickname,
        string? teamRole,
        int yearsInTeam,
        string? bio,
        MediaRef? photo)
    {
        Id = id;
        FullName = fullName;
        Category = category;
        ParentEmail = parentEmail;
        Nickname = nickname;
        TeamRole = teamRole;
        YearsInTeam = yearsInTeam;
        Bio = bio;
        Photo = photo;
        Points = Points.Zero;
        Streak = Streak.None;
        IsActive = true;
    }

    public static ErrorOr<Member> Create(
        string fullName,
        MemberCategory category,
        Email parentEmail,
        string? nickname = null,
        string? teamRole = null,
        int yearsInTeam = 0,
        string? bio = null,
        MediaRef? photo = null)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            return Error.Validation("Member.FullNameRequired", "Full name is required.");
        }

        if (yearsInTeam < 0)
        {
            return Error.Validation("Member.YearsInTeamNegative", "Years in team cannot be negative.");
        }

        var member = new Member(
            MemberId.New(), fullName.Trim(), category, parentEmail,
            nickname, teamRole, yearsInTeam, bio, photo);
        member.RaiseDomainEvent(new MemberCreated(member.Id));
        return member;
    }

    // Appends a point transaction, bumps the running total, and raises MemberPointsAwarded — plus
    // MemberLeveledUp when the new total crosses into a higher rung (FR-M17 / FR-M27 / BE-15).
    public void AwardPoints(int amount, PointSource source, string reason, DateTime occurredOn)
    {
        int previousRung = Level.Rung;

        _pointHistory.Add(PointTransaction.Record(amount, source, reason, occurredOn));
        Points = Points.Add(amount);

        RaiseDomainEvent(new MemberPointsAwarded(Id, amount, Points.Value));

        if (Level.Rung > previousRung)
        {
            RaiseDomainEvent(new MemberLeveledUp(Id, Level.Rung, Level.Name));
        }
    }

    // Records an activity day for the streak; raises StreakMilestoneReached when a threshold is crossed.
    public void RegisterAttendanceActivity(DateOnly day)
    {
        int previousCurrent = Streak.Current;
        Streak = Streak.Register(day);

        if (Streak.CrossedMilestone(previousCurrent))
        {
            RaiseDomainEvent(new StreakMilestoneReached(Id, Streak.Current));
        }
    }

    // Idempotent: earning a badge already held is a no-op (no duplicate, no second event).
    public ErrorOr<Success> EarnBadge(BadgeId badgeId, DateTime earnedOn)
    {
        if (_badges.Any(badge => badge.BadgeId == badgeId))
        {
            return Result.Success;
        }

        _badges.Add(new EarnedBadge(badgeId, earnedOn));
        RaiseDomainEvent(new BadgeEarned(Id, badgeId));
        return Result.Success;
    }

    public ErrorOr<Updated> UpdateProfile(string fullName, string? nickname, Email parentEmail, MediaRef? photo)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            return Error.Validation("Member.FullNameRequired", "Full name is required.");
        }

        FullName = fullName.Trim();
        Nickname = nickname;
        ParentEmail = parentEmail;
        Photo = photo;
        return Result.Updated;
    }

    public void Deactivate() => IsActive = false;

    public void Reactivate() => IsActive = true;
}
