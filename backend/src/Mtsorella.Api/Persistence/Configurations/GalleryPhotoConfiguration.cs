using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mtsorella.Api.Domain.Gallery;

namespace Mtsorella.Api.Persistence.Configurations;

public sealed class GalleryPhotoConfiguration : IEntityTypeConfiguration<GalleryPhoto>
{
    public void Configure(EntityTypeBuilder<GalleryPhoto> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Caption).HasMaxLength(500);

        builder.OwnsMediaRef(g => g.Media);
    }
}
