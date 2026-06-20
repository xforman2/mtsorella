using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mtsorella.Api.Domain.Badges;

namespace Mtsorella.Api.Persistence.Configurations;

public sealed class BadgeConfiguration : IEntityTypeConfiguration<Badge>
{
    public void Configure(EntityTypeBuilder<Badge> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Name).HasMaxLength(200);
        builder.Property(b => b.Criteria).HasMaxLength(1000);

        builder.OwnsMediaRef(b => b.Icon);
    }
}
