using Mtsorella.Api.Domain.Performances;

namespace Mtsorella.Api.Tests.Domain.Performances;

public class PerformanceTests
{
    [Fact]
    public void Create_valid_performance_succeeds()
    {
        var result = Performance.Create("Spring Gala", DateTimeOffset.UtcNow, "City Hall", "Show");

        Assert.False(result.IsError);
    }

    [Fact]
    public void Create_rejects_blank_location()
    {
        var result = Performance.Create("Spring Gala", DateTimeOffset.UtcNow, " ", "Show");

        Assert.True(result.IsError);
    }
}
