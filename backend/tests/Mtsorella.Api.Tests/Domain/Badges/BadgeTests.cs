using Mtsorella.Api.Domain.Badges;

namespace Mtsorella.Api.Tests.Domain.Badges;

public class BadgeTests
{
    [Fact]
    public void Create_valid_badge_is_active_by_default()
    {
        var badge = Badge.Create("Streak Star", "Earned a 7-day streak").Value;

        Assert.True(badge.IsActive);

        badge.Deactivate();
        Assert.False(badge.IsActive);
    }

    [Fact]
    public void Create_rejects_blank_name()
    {
        Assert.True(Badge.Create(" ", "desc").IsError);
    }
}
