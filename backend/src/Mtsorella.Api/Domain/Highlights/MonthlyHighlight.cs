using ErrorOr;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;

namespace Mtsorella.Api.Domain.Highlights;

// "Majorette of the month" (aggregate root). References the highlighted Member by id (D5).
// Uniqueness per month is enforced at the application/persistence boundary. FR-P5 / FR-A12.
public sealed class MonthlyHighlight : AggregateRoot<MonthlyHighlightId>
{
    public YearMonth Month { get; private set; }
    public MemberId MemberId { get; private set; }
    public string Reason { get; private set; } = null!;
    public MediaRef? Photo { get; private set; }

    private MonthlyHighlight(MonthlyHighlightId id, YearMonth month, MemberId memberId, string reason, MediaRef? photo)
    {
        Id = id;
        Month = month;
        MemberId = memberId;
        Reason = reason;
        Photo = photo;
    }

    // EF Core materialization ctor; EF populates mapped members after construction.
    private MonthlyHighlight()
    {
    }

    public static ErrorOr<MonthlyHighlight> Create(YearMonth month, MemberId member, string reason, MediaRef? photo = null)
    {
        if (month.Month is < 1 or > 12)
        {
            return Error.Validation("MonthlyHighlight.InvalidMonth", "Month must be between 1 and 12.");
        }

        if (string.IsNullOrWhiteSpace(reason))
        {
            return Error.Validation("MonthlyHighlight.ReasonRequired", "Reason is required.");
        }

        return new MonthlyHighlight(MonthlyHighlightId.New(), month, member, reason.Trim(), photo);
    }
}
