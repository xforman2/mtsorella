using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mtsorella.Api.Domain.Inbox;

namespace Mtsorella.Api.Persistence.Configurations;

public sealed class PartnershipInquiryConfiguration : IEntityTypeConfiguration<PartnershipInquiry>
{
    public void Configure(EntityTypeBuilder<PartnershipInquiry> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.CompanyName).HasMaxLength(200);
        builder.Property(p => p.ContactPerson).HasMaxLength(200);
        builder.Property(p => p.Email).HasMaxLength(256);
        builder.Property(p => p.Phone).HasMaxLength(32);
    }
}
