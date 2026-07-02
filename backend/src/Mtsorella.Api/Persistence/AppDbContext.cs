using Microsoft.EntityFrameworkCore;
using Mtsorella.Api.Domain.Achievements;
using Mtsorella.Api.Domain.Announcements;
using Mtsorella.Api.Domain.Badges;
using Mtsorella.Api.Domain.Challenges;
using Mtsorella.Api.Domain.Coaches;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;
using Mtsorella.Api.Domain.Gallery;
using Mtsorella.Api.Domain.Highlights;
using Mtsorella.Api.Domain.Identity;
using Mtsorella.Api.Domain.Inbox;
using Mtsorella.Api.Domain.Members;
using Mtsorella.Api.Domain.Performances;
using Mtsorella.Api.Domain.Sponsors;
using Mtsorella.Api.Domain.TeamGoals;
using Mtsorella.Api.Domain.Trainings;
using Mtsorella.Api.Persistence.Converters;
using Mtsorella.Api.Persistence.Outbox;
// Disambiguate from System.ApplicationId, which ImplicitUsings brings into scope via `using System;`.
using ApplicationId = Mtsorella.Api.Domain.Common.ApplicationId;

namespace Mtsorella.Api.Persistence;

// The single shared database context. Each aggregate root gets a DbSet; per-aggregate
// IEntityTypeConfiguration<T> files in Persistence/Configurations/ map owned children, value objects,
// and constraints. Strongly-typed ids and single-value value objects convert the same way everywhere
// via the conventions below. Owned children (e.g. a member's point history) have no DbSet of their own.
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Member> Members => Set<Member>();
    public DbSet<Coach> Coaches => Set<Coach>();
    public DbSet<Badge> Badges => Set<Badge>();
    public DbSet<Training> Trainings => Set<Training>();
    public DbSet<Challenge> Challenges => Set<Challenge>();
    public DbSet<ChallengeSubmission> ChallengeSubmissions => Set<ChallengeSubmission>();
    public DbSet<Announcement> Announcements => Set<Announcement>();
    public DbSet<Achievement> Achievements => Set<Achievement>();
    public DbSet<Performance> Performances => Set<Performance>();
    public DbSet<TeamGoal> TeamGoals => Set<TeamGoal>();
    public DbSet<MonthlyHighlight> MonthlyHighlights => Set<MonthlyHighlight>();
    public DbSet<GalleryPhoto> GalleryPhotos => Set<GalleryPhoto>();
    public DbSet<Sponsor> Sponsors => Set<Sponsor>();
    public DbSet<Application> Applications => Set<Application>();
    public DbSet<PartnershipInquiry> PartnershipInquiries => Set<PartnershipInquiry>();
    public DbSet<ContactMessage> ContactMessages => Set<ContactMessage>();
    public DbSet<UserAccount> UserAccounts => Set<UserAccount>();

    // Domain events persisted in the same transaction as the aggregate change; drained by OutboxProcessor.
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Per-aggregate IEntityTypeConfiguration<T> files in Persistence/Configurations/ self-register.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        // Single-value value objects -> their primitive column, wherever the type appears.
        configurationBuilder.Properties<Email>().HaveConversion<EmailConverter>();
        configurationBuilder.Properties<PhoneNumber>().HaveConversion<PhoneNumberConverter>();
        configurationBuilder.Properties<Points>().HaveConversion<PointsConverter>();
        configurationBuilder.Properties<DateOfBirth>().HaveConversion<DateOfBirthConverter>();

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
        configurationBuilder.Properties<UserAccountId>().HaveConversion<UserAccountIdConverter>();
    }
}
