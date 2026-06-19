namespace Mtsorella.Api.Domain.Common;

// Language-neutral enums (decision D7). The prototype's Slovak labels are a frontend/localization
// concern; each value is tagged with the requirements.md ID it derives from.

public enum Role
{
    Guest,
    Member,
    Admin
}

public enum MemberCategory
{
    Juniors,
    Cadets,
    Seniors
}

public enum CompetitionType
{
    Regional,
    National,
    International
}

public enum Medal
{
    None,
    Bronze,
    Silver,
    Gold
}

public enum PhotoCategory
{
    Competition,
    Training,
    Performance,
    BehindTheScenes
}

public enum AttendanceStatus
{
    Unknown,
    Attending,
    NotAttending
}

public enum SubmissionStatus
{
    Submitted,
    UnderReview,
    Reviewed
}

public enum AnnouncementReactionType
{
    Like,
    Heart
}

public enum PointSource
{
    TrainingAttendance,
    ChallengeCompletion,
    ChallengeQuality,
    Bonus,
    Manual
}

// O2 (assumption, confirm against the partnership form): Financial / Material / Media / Other.
public enum CooperationType
{
    Financial,
    Material,
    Media,
    Other
}

// Visual tier of a level (Bronz / Bronz+ / Striebro / Zlato / Zlato+ / Diamant in the prototype).
public enum LevelTier
{
    Bronze,
    BronzePlus,
    Silver,
    Gold,
    GoldPlus,
    Diamond
}

// Neutral level name (Začiatočník → Sorella in the prototype). Display text is localized in the
// frontend; the domain keeps a stable, language-neutral identifier (FR-M40/M41).
public enum LevelName
{
    Beginner,
    Improver,
    Advanced,
    Professional,
    Star,
    Sorella
}
