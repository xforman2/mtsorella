namespace Mtsorella.Api.Domain.Common;

// Strongly-typed ids (decision D4) — one per aggregate and per owned entity that needs identity.
// Each wraps a Guid so signatures are self-documenting and "pass the wrong Guid" bugs can't compile.

public readonly record struct MemberId(Guid Value)
{
    public static MemberId New() => new(Guid.NewGuid());
}

public readonly record struct PointTransactionId(Guid Value)
{
    public static PointTransactionId New() => new(Guid.NewGuid());
}

public readonly record struct CoachId(Guid Value)
{
    public static CoachId New() => new(Guid.NewGuid());
}

public readonly record struct BadgeId(Guid Value)
{
    public static BadgeId New() => new(Guid.NewGuid());
}

public readonly record struct TrainingId(Guid Value)
{
    public static TrainingId New() => new(Guid.NewGuid());
}

public readonly record struct TrainingAttendanceId(Guid Value)
{
    public static TrainingAttendanceId New() => new(Guid.NewGuid());
}

public readonly record struct ChallengeId(Guid Value)
{
    public static ChallengeId New() => new(Guid.NewGuid());
}

public readonly record struct ChallengeSubmissionId(Guid Value)
{
    public static ChallengeSubmissionId New() => new(Guid.NewGuid());
}

public readonly record struct AnnouncementId(Guid Value)
{
    public static AnnouncementId New() => new(Guid.NewGuid());
}

public readonly record struct AchievementId(Guid Value)
{
    public static AchievementId New() => new(Guid.NewGuid());
}

public readonly record struct PerformanceId(Guid Value)
{
    public static PerformanceId New() => new(Guid.NewGuid());
}

public readonly record struct TeamGoalId(Guid Value)
{
    public static TeamGoalId New() => new(Guid.NewGuid());
}

public readonly record struct MonthlyHighlightId(Guid Value)
{
    public static MonthlyHighlightId New() => new(Guid.NewGuid());
}

public readonly record struct GalleryPhotoId(Guid Value)
{
    public static GalleryPhotoId New() => new(Guid.NewGuid());
}

public readonly record struct SponsorId(Guid Value)
{
    public static SponsorId New() => new(Guid.NewGuid());
}

public readonly record struct ApplicationId(Guid Value)
{
    public static ApplicationId New() => new(Guid.NewGuid());
}

public readonly record struct PartnershipInquiryId(Guid Value)
{
    public static PartnershipInquiryId New() => new(Guid.NewGuid());
}

public readonly record struct ContactMessageId(Guid Value)
{
    public static ContactMessageId New() => new(Guid.NewGuid());
}

public readonly record struct UserAccountId(Guid Value)
{
    public static UserAccountId New() => new(Guid.NewGuid());
}
