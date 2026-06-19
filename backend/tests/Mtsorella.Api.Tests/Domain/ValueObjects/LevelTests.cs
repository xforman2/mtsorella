using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;

namespace Mtsorella.Api.Tests.Domain.ValueObjects;

public class LevelTests
{
    [Theory]
    [InlineData(0, 1, LevelName.Beginner, LevelTier.Bronze)]
    [InlineData(100, 1, LevelName.Beginner, LevelTier.Bronze)]
    [InlineData(101, 2, LevelName.Improver, LevelTier.BronzePlus)]
    [InlineData(300, 2, LevelName.Improver, LevelTier.BronzePlus)]
    [InlineData(301, 3, LevelName.Advanced, LevelTier.Silver)]
    [InlineData(600, 3, LevelName.Advanced, LevelTier.Silver)]
    [InlineData(601, 4, LevelName.Professional, LevelTier.Gold)]
    [InlineData(1000, 4, LevelName.Professional, LevelTier.Gold)]
    [InlineData(1001, 5, LevelName.Star, LevelTier.GoldPlus)]
    [InlineData(1500, 5, LevelName.Star, LevelTier.GoldPlus)]
    [InlineData(1501, 6, LevelName.Sorella, LevelTier.Diamond)]
    [InlineData(99999, 6, LevelName.Sorella, LevelTier.Diamond)]
    public void For_maps_points_to_the_right_rung(int value, int rung, LevelName name, LevelTier tier)
    {
        var level = Level.For(Points.From(value).Value);

        Assert.Equal(rung, level.Rung);
        Assert.Equal(name, level.Name);
        Assert.Equal(tier, level.Tier);
    }

    [Fact]
    public void PointsToNext_returns_points_until_next_rung()
    {
        var points = Points.From(50).Value;
        var level = Level.For(points);

        Assert.Equal(51, level.PointsToNext(points)); // 101 - 50
    }

    [Fact]
    public void PointsToNext_is_null_at_top_rung()
    {
        var points = Points.From(2000).Value;
        var level = Level.For(points);

        Assert.Null(level.PointsToNext(points));
    }
}
