using Mtsorella.Api.Domain.Common.ValueObjects;

namespace Mtsorella.Api.Tests.Domain.ValueObjects;

public class PhoneNumberTests
{
    [Theory]
    [InlineData("+421 900 123 456", "+421900123456")]
    [InlineData("0900 123 456", "0900123456")]
    public void Create_normalizes_valid_number(string raw, string expected)
    {
        var result = PhoneNumber.Create(raw);

        Assert.False(result.IsError);
        Assert.Equal(expected, result.Value.Value);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("12345")]              // too few digits
    [InlineData("1234567890123456")]   // too many digits
    public void Create_rejects_invalid_number(string? raw)
    {
        Assert.True(PhoneNumber.Create(raw).IsError);
    }
}
