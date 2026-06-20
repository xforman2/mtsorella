using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mtsorella.Api.Domain.TeamGoals;

namespace Mtsorella.Api.Persistence.Configurations;

public sealed class TeamGoalConfiguration : IEntityTypeConfiguration<TeamGoal>
{
    public void Configure(EntityTypeBuilder<TeamGoal> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Title).HasMaxLength(200);
        // Target/Progress (Points) convert to int via the global convention.
    }
}
