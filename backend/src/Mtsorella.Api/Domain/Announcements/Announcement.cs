using ErrorOr;
using Mtsorella.Api.Domain.Common;

namespace Mtsorella.Api.Domain.Announcements;

// A board post from a coach (aggregate root); can be pinned, and members react. Owns its reactions
// (D5), at most one per member. FR-M19–M22 / FR-A10 / BE-10.
public sealed class Announcement : AggregateRoot<AnnouncementId>
{
    public CoachId AuthorId { get; private set; }
    public string Title { get; private set; }
    public string Body { get; private set; }
    public bool IsPinned { get; private set; }
    public DateTimeOffset PublishedOn { get; private set; }

    private readonly List<AnnouncementReaction> _reactions = new();
    public IReadOnlyList<AnnouncementReaction> Reactions => _reactions;

    private Announcement(AnnouncementId id, CoachId authorId, string title, string body, DateTimeOffset publishedOn)
    {
        Id = id;
        AuthorId = authorId;
        Title = title;
        Body = body;
        PublishedOn = publishedOn;
    }

    public static ErrorOr<Announcement> Publish(CoachId author, string title, string body, DateTimeOffset publishedOn)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return Error.Validation("Announcement.TitleRequired", "Title is required.");
        }

        if (string.IsNullOrWhiteSpace(body))
        {
            return Error.Validation("Announcement.BodyRequired", "Body is required.");
        }

        var announcement = new Announcement(AnnouncementId.New(), author, title.Trim(), body, publishedOn);
        announcement.RaiseDomainEvent(new AnnouncementPublished(announcement.Id, author));
        return announcement;
    }

    public void Pin()
    {
        if (IsPinned)
        {
            return;
        }

        IsPinned = true;
        RaiseDomainEvent(new AnnouncementPinned(Id));
    }

    public void Unpin() => IsPinned = false;

    // At most one reaction per member — re-reacting replaces the previous one (FR-M22).
    public ErrorOr<Success> React(MemberId memberId, AnnouncementReactionType type, DateTime on)
    {
        _reactions.RemoveAll(reaction => reaction.MemberId == memberId);
        _reactions.Add(new AnnouncementReaction(memberId, type, on));
        return Result.Success;
    }

    public void RemoveReaction(MemberId memberId) =>
        _reactions.RemoveAll(reaction => reaction.MemberId == memberId);
}
