using Mtsorella.Api.Domain.Common;

namespace Mtsorella.Api.Domain.Trainings;

// Owned entity — a member's attendance row for one training (FR-M16). HasEarnedPoints guards the
// "points awarded once per member" invariant even if a member toggles their status.
public sealed class TrainingAttendance : Entity<TrainingAttendanceId>
{
    public MemberId MemberId { get; private init; }
    public AttendanceStatus Status { get; private set; }
    public DateTime? ConfirmedOn { get; private set; }
    public bool HasEarnedPoints { get; private set; }

    private TrainingAttendance(TrainingAttendanceId id, MemberId memberId, AttendanceStatus status, DateTime confirmedOn)
    {
        Id = id;
        MemberId = memberId;
        Status = status;
        ConfirmedOn = confirmedOn;
    }

    internal static TrainingAttendance For(MemberId memberId, AttendanceStatus status, DateTime confirmedOn) =>
        new(TrainingAttendanceId.New(), memberId, status, confirmedOn);

    internal void Update(AttendanceStatus status, DateTime confirmedOn)
    {
        Status = status;
        ConfirmedOn = confirmedOn;
    }

    internal void MarkPointsEarned() => HasEarnedPoints = true;
}
