using Mtsorella.Api.Domain.Coaches;

namespace Mtsorella.Api.Tests.Domain.Coaches;

public class CoachTests
{
    [Fact]
    public void Create_valid_coach_is_hidden_by_default_and_can_be_shown()
    {
        var coach = Coach.Create("Jane Doe", "Head Coach", 5, "Bio").Value;

        Assert.False(coach.ShowOnWebsite);

        coach.Show();
        Assert.True(coach.ShowOnWebsite);

        coach.Hide();
        Assert.False(coach.ShowOnWebsite);
    }

    [Fact]
    public void Create_rejects_blank_role_title()
    {
        var result = Coach.Create("Jane Doe", " ", 5, "Bio");

        Assert.True(result.IsError);
    }
}
