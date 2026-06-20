using ErrorOr;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;

namespace Mtsorella.Api.Domain.Gallery;

// A photo in the public gallery, categorised and dated (aggregate root). Points at stored media via
// MediaRef; the bytes/storage are a boundary (§8). FR-P10–P13 / BE-13 / BE-21.
public sealed class GalleryPhoto : AggregateRoot<GalleryPhotoId>
{
    public MediaRef Media { get; private set; }
    public PhotoCategory Category { get; private set; }
    public int Year { get; private set; }
    public string? Caption { get; private set; }

    private GalleryPhoto(GalleryPhotoId id, MediaRef media, PhotoCategory category, int year, string? caption)
    {
        Id = id;
        Media = media;
        Category = category;
        Year = year;
        Caption = caption;
    }

    public static ErrorOr<GalleryPhoto> Create(MediaRef media, PhotoCategory category, int year, string? caption = null)
    {
        if (year <= 0)
        {
            return Error.Validation("GalleryPhoto.InvalidYear", "Year must be a positive number.");
        }

        return new GalleryPhoto(GalleryPhotoId.New(), media, category, year, caption);
    }

    public void Recategorize(PhotoCategory category) => Category = category;
}
