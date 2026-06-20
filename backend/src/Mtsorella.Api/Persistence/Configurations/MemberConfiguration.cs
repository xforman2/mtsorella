using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mtsorella.Api.Domain.Members;

namespace Mtsorella.Api.Persistence.Configurations;

public sealed class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.FullName).HasMaxLength(200);
        builder.Property(m => m.Nickname).HasMaxLength(100);
        builder.Property(m => m.TeamRole).HasMaxLength(100);
        builder.Property(m => m.ParentEmail).HasMaxLength(256);

        // Derived from Points via the level ladder — never stored.
        builder.Ignore(m => m.Level);

        // Streak is a value-type value object → inline columns (Streak_Current, ...).
        builder.ComplexProperty(m => m.Streak);

        builder.OwnsMediaRef(m => m.Photo);

        // Owned children, reached through read-only collections backed by private fields.
        builder.OwnsMany(m => m.PointHistory, history =>
        {
            history.HasKey(t => t.Id);
            history.Property(t => t.Reason).HasMaxLength(500);
        });
        builder.Navigation(m => m.PointHistory).UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.OwnsMany(m => m.Badges);
        builder.Navigation(m => m.Badges).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
