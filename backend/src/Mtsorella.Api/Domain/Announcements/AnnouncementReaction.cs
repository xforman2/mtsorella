using Mtsorella.Api.Domain.Common;

namespace Mtsorella.Api.Domain.Announcements;

// Owned value object — a member's reaction to an announcement (FR-M22). References Member by id (D5).
public sealed record AnnouncementReaction(MemberId MemberId, AnnouncementReactionType Type, DateTime On);
