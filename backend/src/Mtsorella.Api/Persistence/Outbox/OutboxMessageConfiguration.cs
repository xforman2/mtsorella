using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mtsorella.Api.Persistence.Outbox;

public sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.EventType).HasMaxLength(500);

        // Filtered index so the processor's "unprocessed" scan stays cheap as processed rows accumulate.
        builder.HasIndex(m => m.ProcessedOnUtc).HasFilter("\"ProcessedOnUtc\" IS NULL");
    }
}
