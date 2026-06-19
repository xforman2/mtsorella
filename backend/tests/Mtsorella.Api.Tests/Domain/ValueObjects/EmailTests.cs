using Mtsorella.Api.Domain.Common.ValueObjects;

namespace Mtsorella.Api.Tests.Domain.ValueObjects;

public class EmailTests
{
    [Theory]
    [InlineData("parent@example.com")]
    [InlineData("  parent@example.com  ")]
    public void Create_valid_email_trims_and_succeeds(string raw)
    {
        var result = Email.Create(raw);

        Assert.False(result.IsError);
        Assert.Equal("parent@example.com", result.Value.Value);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("not-an-email")]
    [InlineData("two@@at.com")]
    [InlineData("no@domain")]
    [InlineData("spa ce@x.com")]
    public void Create_invalid_email_is_error(string? raw)
    {
        Assert.True(Email.Create(raw).IsError);
    }
}
