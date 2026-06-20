using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mtsorella.Api.Domain.Achievements;

namespace Mtsorella.Api.Persistence.Configurations;

public sealed class AchievementConfiguration : IEntityTypeConfiguration<Achievement>
{
    public void Configure(EntityTypeBuilder<Achievement> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name).HasMaxLength(200);

        // Placement is a value object → inline columns (Placement_Rank, Placement_Label).
        builder.OwnsOne(a => a.Placement, placement => placement.Property(p => p.Label).HasMaxLength(200));
    }
}
