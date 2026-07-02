using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mtsorella.Api.Domain.Common;
// Disambiguate from System.ApplicationId, which ImplicitUsings brings into scope via `using System;`.
using ApplicationId = Mtsorella.Api.Domain.Common.ApplicationId;

namespace Mtsorella.Api.Persistence.Converters;

// One ValueConverter per strongly-typed id (Ids.cs) — store the wrapped Guid, rebuild the id on read.
// Deliberately boilerplate: each is explicit and greppable, and is registered by an explicit line in
// AppDbContext.ConfigureConventions. Adding an id means adding its converter here plus that one line.

public sealed class MemberIdConverter : ValueConverter<MemberId, Guid>
{
    public MemberIdConverter() : base(id => id.Value, value => new MemberId(value)) { }
}

public sealed class PointTransactionIdConverter : ValueConverter<PointTransactionId, Guid>
{
    public PointTransactionIdConverter() : base(id => id.Value, value => new PointTransactionId(value)) { }
}

public sealed class CoachIdConverter : ValueConverter<CoachId, Guid>
{
    public CoachIdConverter() : base(id => id.Value, value => new CoachId(value)) { }
}

public sealed class BadgeIdConverter : ValueConverter<BadgeId, Guid>
{
    public BadgeIdConverter() : base(id => id.Value, value => new BadgeId(value)) { }
}

public sealed class TrainingIdConverter : ValueConverter<TrainingId, Guid>
{
    public TrainingIdConverter() : base(id => id.Value, value => new TrainingId(value)) { }
}

public sealed class TrainingAttendanceIdConverter : ValueConverter<TrainingAttendanceId, Guid>
{
    public TrainingAttendanceIdConverter() : base(id => id.Value, value => new TrainingAttendanceId(value)) { }
}

public sealed class ChallengeIdConverter : ValueConverter<ChallengeId, Guid>
{
    public ChallengeIdConverter() : base(id => id.Value, value => new ChallengeId(value)) { }
}

public sealed class ChallengeSubmissionIdConverter : ValueConverter<ChallengeSubmissionId, Guid>
{
    public ChallengeSubmissionIdConverter() : base(id => id.Value, value => new ChallengeSubmissionId(value)) { }
}

public sealed class AnnouncementIdConverter : ValueConverter<AnnouncementId, Guid>
{
    public AnnouncementIdConverter() : base(id => id.Value, value => new AnnouncementId(value)) { }
}

public sealed class AchievementIdConverter : ValueConverter<AchievementId, Guid>
{
    public AchievementIdConverter() : base(id => id.Value, value => new AchievementId(value)) { }
}

public sealed class PerformanceIdConverter : ValueConverter<PerformanceId, Guid>
{
    public PerformanceIdConverter() : base(id => id.Value, value => new PerformanceId(value)) { }
}

public sealed class TeamGoalIdConverter : ValueConverter<TeamGoalId, Guid>
{
    public TeamGoalIdConverter() : base(id => id.Value, value => new TeamGoalId(value)) { }
}

public sealed class MonthlyHighlightIdConverter : ValueConverter<MonthlyHighlightId, Guid>
{
    public MonthlyHighlightIdConverter() : base(id => id.Value, value => new MonthlyHighlightId(value)) { }
}

public sealed class GalleryPhotoIdConverter : ValueConverter<GalleryPhotoId, Guid>
{
    public GalleryPhotoIdConverter() : base(id => id.Value, value => new GalleryPhotoId(value)) { }
}

public sealed class SponsorIdConverter : ValueConverter<SponsorId, Guid>
{
    public SponsorIdConverter() : base(id => id.Value, value => new SponsorId(value)) { }
}

public sealed class ApplicationIdConverter : ValueConverter<ApplicationId, Guid>
{
    public ApplicationIdConverter() : base(id => id.Value, value => new ApplicationId(value)) { }
}

public sealed class PartnershipInquiryIdConverter : ValueConverter<PartnershipInquiryId, Guid>
{
    public PartnershipInquiryIdConverter() : base(id => id.Value, value => new PartnershipInquiryId(value)) { }
}

public sealed class ContactMessageIdConverter : ValueConverter<ContactMessageId, Guid>
{
    public ContactMessageIdConverter() : base(id => id.Value, value => new ContactMessageId(value)) { }
}

public sealed class UserAccountIdConverter : ValueConverter<UserAccountId, Guid>
{
    public UserAccountIdConverter() : base(id => id.Value, value => new UserAccountId(value)) { }
}
