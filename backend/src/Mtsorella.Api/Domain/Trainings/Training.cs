using ErrorOr;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;

namespace Mtsorella.Api.Domain.Trainings;

// A scheduled training (aggregate root). Members confirm attendance and earn points for showing up.
// Owns its TrainingAttendance rows (D5); attendance points are awarded via a domain event rather than
// reaching into Member (D6). FR-M14–M18 / FR-A8 / BE-9 / BE-16.
public sealed class Training : AggregateRoot<TrainingId>
{
    // O4 default: a fixed per-training award unless an admin sets another value.
    public const int DefaultAttendancePoints = 10;

    public DateTimeOffset StartsAt { get; private set; }
    public DateTimeOffset EndsAt { get; private set; }
    public string Location { get; private set; }
    public MemberCategory Category { get; private set; }
    public string? WhatToBring { get; private set; }
    public Recurrence Recurrence { get; private set; }
    public int AttendancePoints { get; private set; }

    private readonly List<TrainingAttendance> _attendances = new();
    public IReadOnlyList<TrainingAttendance> Attendances => _attendances;

    private Training(
        TrainingId id,
        DateTimeOffset startsAt,
        DateTimeOffset endsAt,
        string location,
        MemberCategory category,
        string? whatToBring,
        Recurrence recurrence,
        int attendancePoints)
    {
        Id = id;
        StartsAt = startsAt;
        EndsAt = endsAt;
        Location = location;
        Category = category;
        WhatToBring = whatToBring;
        Recurrence = recurrence;
        AttendancePoints = attendancePoints;
    }

    public static ErrorOr<Training> Create(
        DateTimeOffset startsAt,
        DateTimeOffset endsAt,
        string location,
        MemberCategory category,
        string? whatToBring = null,
        Recurrence? recurrence = null,
        int attendancePoints = DefaultAttendancePoints)
    {
        if (endsAt <= startsAt)
        {
            return Error.Validation("Training.EndsBeforeStart", "Training must end after it starts.");
        }

        if (string.IsNullOrWhiteSpace(location))
        {
            return Error.Validation("Training.LocationRequired", "Location is required.");
        }

        if (attendancePoints < 0)
        {
            return Error.Validation("Training.AttendancePointsNegative", "Attendance points cannot be negative.");
        }

        var training = new Training(
            TrainingId.New(), startsAt, endsAt, location.Trim(), category,
            whatToBring, recurrence ?? Recurrence.None, attendancePoints);
        training.RaiseDomainEvent(new TrainingScheduled(training.Id, startsAt));
        return training;
    }

    public ErrorOr<Updated> Reschedule(DateTimeOffset startsAt, DateTimeOffset endsAt)
    {
        if (endsAt <= startsAt)
        {
            return Error.Validation("Training.EndsBeforeStart", "Training must end after it starts.");
        }

        StartsAt = startsAt;
        EndsAt = endsAt;
        return Result.Updated;
    }

    // Upserts the member's attendance row. The first time a member is confirmed Attending, raises
    // TrainingAttendanceConfirmed (carrying AttendancePoints) — once per member, even across toggles.
    public ErrorOr<Success> ConfirmAttendance(MemberId memberId, AttendanceStatus status, DateTime confirmedOn)
    {
        if (status == AttendanceStatus.Unknown)
        {
            return Error.Validation("Training.InvalidAttendance", "Attendance status must be Attending or NotAttending.");
        }

        TrainingAttendance? attendance = _attendances.FirstOrDefault(row => row.MemberId == memberId);
        if (attendance is null)
        {
            attendance = TrainingAttendance.For(memberId, status, confirmedOn);
            _attendances.Add(attendance);
        }
        else
        {
            attendance.Update(status, confirmedOn);
        }

        if (status == AttendanceStatus.Attending && !attendance.HasEarnedPoints)
        {
            attendance.MarkPointsEarned();
            RaiseDomainEvent(new TrainingAttendanceConfirmed(Id, memberId, AttendancePoints));
        }

        return Result.Success;
    }
}
