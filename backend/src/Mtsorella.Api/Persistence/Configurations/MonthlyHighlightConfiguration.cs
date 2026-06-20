using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mtsorella.Api.Domain.Highlights;

namespace Mtsorella.Api.Persistence.Configurations;

public sealed class MonthlyHighlightConfiguration : IEntityTypeConfiguration<MonthlyHighlight>
{
    public void Configure(EntityTypeBuilder<MonthlyHighlight> builder)
    {
        builder.HasKey(h => h.Id);

        builder.Property(h => h.Reason).HasMaxLength(1000);

        // YearMonth is a value-type value object → inline columns (Month_Year, Month_Month).
        builder.ComplexProperty(h => h.Month);

        builder.OwnsMediaRef(h => h.Photo);

        builder.HasIndex(h => h.MemberId);
    }
}
