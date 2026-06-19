using Mtsorella.Api.Domain.Achievements;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;

namespace Mtsorella.Api.Tests.Domain.Achievements;

public class AchievementTests
{
    [Fact]
    public void Create_valid_achievement_succeeds()
    {
        var result = Achievement.Create(
            2025, CompetitionType.National, "Nationals", new Placement(1, "1st"), Medal.Gold, "desc");

        Assert.False(result.IsError);
    }

    [Fact]
    public void Create_rejects_blank_name()
    {
        var result = Achievement.Create(
            2025, CompetitionType.National, " ", new Placement(1, "1st"), Medal.Gold, "desc");

        Assert.True(result.IsError);
    }

    [Fact]
    public void Edit_updates_the_fields()
    {
        var achievement = Achievement.Create(
            2025, CompetitionType.National, "Nationals", new Placement(1, "1st"), Medal.Gold, "desc").Value;

        var result = achievement.Edit(
            2024, CompetitionType.International, "Worlds", new Placement(2, "2nd"), Medal.Silver, "updated");

        Assert.False(result.IsError);
        Assert.Equal(2024, achievement.Year);
        Assert.Equal("Worlds", achievement.Name);
        Assert.Equal(Medal.Silver, achievement.Medal);
    }
}
