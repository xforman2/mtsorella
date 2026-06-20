using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mtsorella.Api.Domain.Inbox;

namespace Mtsorella.Api.Persistence.Configurations;

public sealed class ApplicationConfiguration : IEntityTypeConfiguration<Application>
{
    public void Configure(EntityTypeBuilder<Application> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.ChildName).HasMaxLength(200);
        builder.Property(a => a.ParentName).HasMaxLength(200);
        builder.Property(a => a.ParentEmail).HasMaxLength(256);
        builder.Property(a => a.ParentPhone).HasMaxLength(32);
        builder.Property(a => a.PreviousExperience).HasMaxLength(2000);
    }
}
