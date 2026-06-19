using Mtsorella.Api.Domain.Announcements;
using Mtsorella.Api.Domain.Common;

namespace Mtsorella.Api.Tests.Domain.Announcements;

public class AnnouncementTests
{
    private static Announcement NewAnnouncement() =>
        Announcement.Publish(CoachId.New(), "Title", "Body", DateTimeOffset.UtcNow).Value;

    [Fact]
    public void Publish_raises_event()
    {
        var announcement = NewAnnouncement();

        Assert.Contains(announcement.DomainEvents, domainEvent => domainEvent is AnnouncementPublished);
    }

    [Fact]
    public void Publish_blank_title_is_error()
    {
        var result = Announcement.Publish(CoachId.New(), " ", "Body", DateTimeOffset.UtcNow);

        Assert.True(result.IsError);
    }

    [Fact]
    public void React_keeps_one_reaction_per_member_and_replaces_it()
    {
        var announcement = NewAnnouncement();
        var memberId = MemberId.New();

        announcement.React(memberId, AnnouncementReactionType.Like, DateTime.UtcNow);
        announcement.React(memberId, AnnouncementReactionType.Heart, DateTime.UtcNow);

        Assert.Single(announcement.Reactions);
        Assert.Equal(AnnouncementReactionType.Heart, announcement.Reactions[0].Type);
    }

    [Fact]
    public void RemoveReaction_removes_the_members_reaction()
    {
        var announcement = NewAnnouncement();
        var memberId = MemberId.New();
        announcement.React(memberId, AnnouncementReactionType.Like, DateTime.UtcNow);

        announcement.RemoveReaction(memberId);

        Assert.Empty(announcement.Reactions);
    }

    [Fact]
    public void Pin_sets_the_flag_and_raises_one_event_even_if_called_twice()
    {
        var announcement = NewAnnouncement();
        announcement.ClearDomainEvents();

        announcement.Pin();
        announcement.Pin();

        Assert.True(announcement.IsPinned);
        Assert.Equal(1, announcement.DomainEvents.Count(domainEvent => domainEvent is AnnouncementPinned));
    }
}
