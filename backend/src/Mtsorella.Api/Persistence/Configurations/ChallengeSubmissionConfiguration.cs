using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mtsorella.Api.Domain.Challenges;

namespace Mtsorella.Api.Persistence.Configurations;

public sealed class ChallengeSubmissionConfiguration : IEntityTypeConfiguration<ChallengeSubmission>
{
    public void Configure(EntityTypeBuilder<ChallengeSubmission> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.ReviewComment).HasMaxLength(1000);

        builder.OwnsMediaRef(s => s.Video);

        // Optional score (set on review). ChallengeScore exposes get-only properties set through its
        // constructor, so map the stored parts explicitly; Total is computed and not stored.
        builder.OwnsOne(s => s.Score, score =>
        {
            score.Property(c => c.Completion);
            score.Property(c => c.OnTimeBonus);
            score.Property(c => c.Quality);
            score.Ignore(c => c.Total);
        });

        // One submission per member per challenge (O1) — enforced here, since it spans aggregates.
        builder.HasIndex(s => new { s.ChallengeId, s.MemberId }).IsUnique();
    }
}
