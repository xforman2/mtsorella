using Mtsorella.Api.Domain.Common;

namespace Mtsorella.Api.Domain.Members;

public sealed record MemberCreated(MemberId MemberId) : IDomainEvent;

public sealed record MemberPointsAwarded(MemberId MemberId, int Amount, int NewTotal) : IDomainEvent;

public sealed record MemberLeveledUp(MemberId MemberId, int NewRung, LevelName NewLevel) : IDomainEvent;

public sealed record BadgeEarned(MemberId MemberId, BadgeId BadgeId) : IDomainEvent;

public sealed record StreakMilestoneReached(MemberId MemberId, int StreakLength) : IDomainEvent;
