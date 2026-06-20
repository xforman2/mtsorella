using Mtsorella.Api.Domain.Common.ValueObjects;
using Mtsorella.Api.Persistence.Converters;

namespace Mtsorella.Api.Tests.Persistence.Converters;

public class ValueObjectConverterTests
{
    [Fact]
    public void Email_round_trips_through_its_string()
    {
        var converter = new EmailConverter();
        var email = Email.Create("majoretka@mtsorella.sk").Value;

        Assert.Equal("majoretka@mtsorella.sk", converter.ConvertToProvider(email));
        Assert.Equal(email, converter.ConvertFromProvider("majoretka@mtsorella.sk"));
    }

    [Fact]
    public void PhoneNumber_round_trips_through_its_normalized_string()
    {
        var converter = new PhoneNumberConverter();
        var phone = PhoneNumber.Create("+421 900 111 222").Value;

        // Create normalizes to '+' + digits; the column stores that same normalized string.
        Assert.Equal("+421900111222", converter.ConvertToProvider(phone));
        Assert.Equal(phone, converter.ConvertFromProvider("+421900111222"));
    }

    [Fact]
    public void Points_round_trips_through_its_int()
    {
        var converter = new PointsConverter();
        var points = Points.From(150).Value;

        Assert.Equal(150, converter.ConvertToProvider(points));
        Assert.Equal(points, converter.ConvertFromProvider(150));
    }

    [Fact]
    public void DateOfBirth_round_trips_through_its_date()
    {
        var converter = new DateOfBirthConverter();
        var date = new DateOnly(2010, 5, 1);
        var dateOfBirth = new DateOfBirth(date);

        Assert.Equal(date, converter.ConvertToProvider(dateOfBirth));
        Assert.Equal(dateOfBirth, converter.ConvertFromProvider(date));
    }
}
