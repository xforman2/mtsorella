using Mtsorella.Api.Domain.Common.ValueObjects;

namespace Mtsorella.Api.Tests.Domain.ValueObjects;

public class DateOfBirthTests
{
    [Fact]
    public void AgeOn_before_this_years_birthday_is_not_yet_incremented()
    {
        var dob = new DateOfBirth(new DateOnly(2015, 7, 10));

        Assert.Equal(10, dob.AgeOn(new DateOnly(2026, 6, 1)));
    }

    [Fact]
    public void AgeOn_after_this_years_birthday_is_incremented()
    {
        var dob = new DateOfBirth(new DateOnly(2015, 5, 10));

        Assert.Equal(11, dob.AgeOn(new DateOnly(2026, 6, 1)));
    }
}
