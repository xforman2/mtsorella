using Mtsorella.Api.Domain.Common;

namespace Mtsorella.Api.Domain.Members;

// Owned value object — which badge a member earned, and when (FR-M36). References Badge by id (D9).
public sealed record EarnedBadge(BadgeId BadgeId, DateTime EarnedOn);
