using Mtsorella.Api.Domain.Common;

namespace Mtsorella.Api.Domain.Announcements;

public sealed record AnnouncementPublished(AnnouncementId AnnouncementId, CoachId AuthorId) : IDomainEvent;

public sealed record AnnouncementPinned(AnnouncementId AnnouncementId) : IDomainEvent;
