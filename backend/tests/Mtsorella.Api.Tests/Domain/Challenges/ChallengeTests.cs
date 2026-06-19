using Mtsorella.Api.Domain.Challenges;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;

namespace Mtsorella.Api.Tests.Domain.Challenges;

public class ChallengeTests
{
    [Fact]
    public void Create_valid_challenge_succeeds_and_raises_event()
    {
        var video = new MediaRef(MediaKind.Video, "videos/howto");

        var result = Challenge.Create("Technika", "Do the thing", DateTimeOffset.UtcNow.AddDays(7), video, CoachId.New());

        Assert.False(result.IsError);
        Assert.True(result.Value.IsActive);
        Assert.Contains(result.Value.DomainEvents, domainEvent => domainEvent is ChallengeCreated);
    }

    [Fact]
    public void Create_blank_name_is_error()
    {
        var video = new MediaRef(MediaKind.Video, "videos/howto");

        var result = Challenge.Create(" ", "desc", DateTimeOffset.UtcNow, video, CoachId.New());

        Assert.True(result.IsError);
    }

    [Fact]
    public void Close_deactivates_the_challenge()
    {
        var video = new MediaRef(MediaKind.Video, "videos/howto");
        var challenge = Challenge.Create("Technika", "desc", DateTimeOffset.UtcNow, video, CoachId.New()).Value;

        challenge.Close();

        Assert.False(challenge.IsActive);
    }
}
