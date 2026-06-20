using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mtsorella.Api.Domain.Performances;

namespace Mtsorella.Api.Persistence.Configurations;

public sealed class PerformanceConfiguration : IEntityTypeConfiguration<Performance>
{
    public void Configure(EntityTypeBuilder<Performance> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).HasMaxLength(200);
        builder.Property(p => p.Location).HasMaxLength(200);
        builder.Property(p => p.Type).HasMaxLength(100);
    }
}
