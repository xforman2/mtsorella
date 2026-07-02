using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mtsorella.Api.Domain.Identity;

namespace Mtsorella.Api.Persistence.Configurations;

public sealed class UserAccountConfiguration : IEntityTypeConfiguration<UserAccount>
{
    public void Configure(EntityTypeBuilder<UserAccount> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Email).HasMaxLength(256);
        builder.Property(a => a.PasswordHash).HasMaxLength(256);

        // Login is by email, so it must be unique across accounts.
        builder.HasIndex(a => a.Email).IsUnique();
    }
}
