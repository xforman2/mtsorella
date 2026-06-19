using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;
using Mtsorella.Api.Domain.Gallery;

namespace Mtsorella.Api.Tests.Domain.Gallery;

public class GalleryPhotoTests
{
    [Fact]
    public void Create_valid_photo_succeeds_and_can_be_recategorized()
    {
        var media = new MediaRef(MediaKind.Image, "photos/1");
        var photo = GalleryPhoto.Create(media, PhotoCategory.Competition, 2025).Value;

        photo.Recategorize(PhotoCategory.Training);

        Assert.Equal(PhotoCategory.Training, photo.Category);
    }

    [Fact]
    public void Create_rejects_invalid_year()
    {
        var media = new MediaRef(MediaKind.Image, "photos/1");

        var result = GalleryPhoto.Create(media, PhotoCategory.Competition, 0);

        Assert.True(result.IsError);
    }
}
