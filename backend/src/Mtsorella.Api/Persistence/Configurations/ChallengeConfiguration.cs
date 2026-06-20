using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mtsorella.Api.Domain.Challenges;

namespace Mtsorella.Api.Persistence.Configurations;

public sealed class ChallengeConfiguration : IEntityTypeConfiguration<Challenge>
{
    public void Configure(EntityTypeBuilder<Challenge> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).HasMaxLength(200);
        builder.Property(c => c.Category).HasMaxLength(200);

        builder.OwnsMediaRef(c => c.InstructionalVideo);

        builder.HasIndex(c => c.CreatedBy);
    }
}
