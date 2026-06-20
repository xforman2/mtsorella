using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Persistence.Converters;

namespace Mtsorella.Api.Tests.Persistence.Converters;

public class IdConverterTests
{
    [Fact]
    public void Converts_id_to_its_guid()
    {
        var converter = new MemberIdConverter();
        var guid = Guid.NewGuid();

        Assert.Equal(guid, converter.ConvertToProvider(new MemberId(guid)));
    }

    [Fact]
    public void Rebuilds_id_from_guid()
    {
        var converter = new MemberIdConverter();
        var guid = Guid.NewGuid();

        Assert.Equal(new MemberId(guid), converter.ConvertFromProvider(guid));
    }

    [Fact]
    public void Round_trips_a_different_id_type()
    {
        var converter = new ChallengeIdConverter();
        var id = ChallengeId.New();

        var asGuid = converter.ConvertToProvider(id);
        var back = converter.ConvertFromProvider(asGuid);

        Assert.Equal(id, back);
    }
}
