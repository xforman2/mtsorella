using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mtsorella.Api.Domain.Announcements;

namespace Mtsorella.Api.Persistence.Configurations;

public sealed class AnnouncementConfiguration : IEntityTypeConfiguration<Announcement>
{
    public void Configure(EntityTypeBuilder<Announcement> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Title).HasMaxLength(200);

        builder.OwnsMany(a => a.Reactions);
        builder.Navigation(a => a.Reactions).UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasIndex(a => a.AuthorId);
    }
}
