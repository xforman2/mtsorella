using Microsoft.EntityFrameworkCore;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Persistence.Converters;
// Disambiguate from System.ApplicationId, which ImplicitUsings brings into scope via `using System;`.
using ApplicationId = Mtsorella.Api.Domain.Common.ApplicationId;

namespace Mtsorella.Api.Persistence;

// No aggregates are mapped yet — DbSets and their IEntityTypeConfiguration<T> files land with the
// aggregate-mapping issue (#28). This foundation only registers the shared conventions every mapping
// relies on: strongly-typed ids and single-value value objects always persist the same way.
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Per-aggregate IEntityTypeConfiguration<T> files in Persistence/Configurations/ self-register.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        // Every strongly-typed id -> Guid. One explicit line per id (converters in IdConverters.cs).
        configurationBuilder.Properties<MemberId>().HaveConversion<MemberIdConverter>();
        configurationBuilder.Properties<PointTransactionId>().HaveConversion<PointTransactionIdConverter>();
        configurationBuilder.Properties<CoachId>().HaveConversion<CoachIdConverter>();
        configurationBuilder.Properties<BadgeId>().HaveConversion<BadgeIdConverter>();
        configurationBuilder.Properties<TrainingId>().HaveConversion<TrainingIdConverter>();
        configurationBuilder.Properties<TrainingAttendanceId>().HaveConversion<TrainingAttendanceIdConverter>();
        configurationBuilder.Properties<ChallengeId>().HaveConversion<ChallengeIdConverter>();
        configurationBuilder.Properties<ChallengeSubmissionId>().HaveConversion<ChallengeSubmissionIdConverter>();
        configurationBuilder.Properties<AnnouncementId>().HaveConversion<AnnouncementIdConverter>();
        configurationBuilder.Properties<AchievementId>().HaveConversion<AchievementIdConverter>();
        configurationBuilder.Properties<PerformanceId>().HaveConversion<PerformanceIdConverter>();
        configurationBuilder.Properties<TeamGoalId>().HaveConversion<TeamGoalIdConverter>();
        configurationBuilder.Properties<MonthlyHighlightId>().HaveConversion<MonthlyHighlightIdConverter>();
        configurationBuilder.Properties<GalleryPhotoId>().HaveConversion<GalleryPhotoIdConverter>();
        configurationBuilder.Properties<SponsorId>().HaveConversion<SponsorIdConverter>();
        configurationBuilder.Properties<ApplicationId>().HaveConversion<ApplicationIdConverter>();
        configurationBuilder.Properties<PartnershipInquiryId>().HaveConversion<PartnershipInquiryIdConverter>();
        configurationBuilder.Properties<ContactMessageId>().HaveConversion<ContactMessageIdConverter>();
    }
}
