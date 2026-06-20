using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mtsorella.Api.Domain.Sponsors;

namespace Mtsorella.Api.Persistence.Configurations;

public sealed class SponsorConfiguration : IEntityTypeConfiguration<Sponsor>
{
    public void Configure(EntityTypeBuilder<Sponsor> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name).HasMaxLength(200);
        builder.Property(s => s.WebsiteUrl).HasMaxLength(2048);

        builder.OwnsMediaRef(s => s.Logo);
    }
}
