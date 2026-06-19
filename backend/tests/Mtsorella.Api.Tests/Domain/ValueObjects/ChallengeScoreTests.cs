using Mtsorella.Api.Domain.Common.ValueObjects;

namespace Mtsorella.Api.Tests.Domain.ValueObjects;

public class ChallengeScoreTests
{
    [Fact]
    public void Create_on_time_includes_completion_bonus_and_quality()
    {
        var score = ChallengeScore.Create(15, onTime: true).Value;

        Assert.Equal(10, score.Completion);
        Assert.Equal(5, score.OnTimeBonus);
        Assert.Equal(15, score.Quality);
        Assert.Equal(30, score.Total);
    }

    [Fact]
    public void Create_late_has_no_bonus()
    {
        var score = ChallengeScore.Create(0, onTime: false).Value;

        Assert.Equal(0, score.OnTimeBonus);
        Assert.Equal(10, score.Total);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(21)]
    public void Create_rejects_quality_out_of_range(int quality)
    {
        var result = ChallengeScore.Create(quality, onTime: true);

        Assert.True(result.IsError);
        Assert.Equal("ChallengeScore.QualityOutOfRange", result.FirstError.Code);
    }
}
