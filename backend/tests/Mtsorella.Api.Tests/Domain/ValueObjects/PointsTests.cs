using Mtsorella.Api.Domain.Common.ValueObjects;

namespace Mtsorella.Api.Tests.Domain.ValueObjects;

public class PointsTests
{
    [Fact]
    public void From_negative_is_validation_error()
    {
        var result = Points.From(-1);

        Assert.True(result.IsError);
        Assert.Equal("Points.Negative", result.FirstError.Code);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(150)]
    public void From_non_negative_succeeds(int value)
    {
        var result = Points.From(value);

        Assert.False(result.IsError);
        Assert.Equal(value, result.Value.Value);
    }

    [Fact]
    public void Zero_is_zero() => Assert.Equal(0, Points.Zero.Value);

    [Fact]
    public void Add_increases_value()
    {
        var points = Points.Zero.Add(30);

        Assert.Equal(30, points.Value);
    }

    [Fact]
    public void Add_clamps_at_zero()
    {
        var points = Points.From(10).Value.Add(-50);

        Assert.Equal(0, points.Value);
    }
}
