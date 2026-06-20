using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mtsorella.Api.Domain.Trainings;

namespace Mtsorella.Api.Persistence.Configurations;

public sealed class TrainingConfiguration : IEntityTypeConfiguration<Training>
{
    public void Configure(EntityTypeBuilder<Training> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Location).HasMaxLength(200);
        builder.Property(t => t.WhatToBring).HasMaxLength(1000);

        // Recurrence carries a DayOfWeek set, so store the whole value object as one JSON column.
        builder.OwnsOne(t => t.Recurrence, recurrence => recurrence.ToJson());

        builder.OwnsMany(t => t.Attendances, attendance => attendance.HasKey(a => a.Id));
        builder.Navigation(t => t.Attendances).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
