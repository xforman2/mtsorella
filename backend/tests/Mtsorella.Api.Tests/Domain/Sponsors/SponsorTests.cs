using Mtsorella.Api.Domain.Sponsors;

namespace Mtsorella.Api.Tests.Domain.Sponsors;

public class SponsorTests
{
    [Fact]
    public void Create_valid_sponsor_succeeds()
    {
        var result = Sponsor.Create("Acme", "Helps us out");

        Assert.False(result.IsError);
    }

    [Fact]
    public void Create_rejects_blank_name()
    {
        var result = Sponsor.Create(" ", "desc");

        Assert.True(result.IsError);
    }
}
