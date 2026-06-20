using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mtsorella.Api.Domain.Coaches;

namespace Mtsorella.Api.Persistence.Configurations;

public sealed class CoachConfiguration : IEntityTypeConfiguration<Coach>
{
    public void Configure(EntityTypeBuilder<Coach> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.FullName).HasMaxLength(200);
        builder.Property(c => c.RoleTitle).HasMaxLength(150);

        builder.OwnsMediaRef(c => c.Photo);
    }
}
