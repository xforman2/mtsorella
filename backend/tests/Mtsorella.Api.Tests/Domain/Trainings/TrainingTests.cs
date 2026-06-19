using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Trainings;

namespace Mtsorella.Api.Tests.Domain.Trainings;

public class TrainingTests
{
    private static Training NewTraining()
    {
        var start = DateTimeOffset.UtcNow;
        return Training.Create(start, start.AddHours(2), "Gym", MemberCategory.Cadets).Value;
    }

    [Fact]
    public void Create_rejects_end_before_start()
    {
        var start = DateTimeOffset.UtcNow;

        var result = Training.Create(start, start.AddHours(-1), "Gym", MemberCategory.Cadets);

        Assert.True(result.IsError);
        Assert.Equal("Training.EndsBeforeStart", result.FirstError.Code);
    }

    [Fact]
    public void Create_defaults_attendance_points_and_raises_scheduled_event()
    {
        var training = NewTraining();

        Assert.Equal(Training.DefaultAttendancePoints, training.AttendancePoints);
        Assert.Contains(training.DomainEvents, domainEvent => domainEvent is TrainingScheduled);
    }

    [Fact]
    public void ConfirmAttendance_attending_adds_one_row_and_awards_once()
    {
        var training = NewTraining();
        training.ClearDomainEvents();
        var memberId = MemberId.New();

        training.ConfirmAttendance(memberId, AttendanceStatus.Attending, DateTime.UtcNow);
        training.ConfirmAttendance(memberId, AttendanceStatus.Attending, DateTime.UtcNow);

        Assert.Single(training.Attendances);
        Assert.Equal(1, training.DomainEvents.Count(domainEvent => domainEvent is TrainingAttendanceConfirmed));
    }

    [Fact]
    public void ConfirmAttendance_not_attending_does_not_award()
    {
        var training = NewTraining();
        training.ClearDomainEvents();

        training.ConfirmAttendance(MemberId.New(), AttendanceStatus.NotAttending, DateTime.UtcNow);

        Assert.DoesNotContain(training.DomainEvents, domainEvent => domainEvent is TrainingAttendanceConfirmed);
    }

    [Fact]
    public void ConfirmAttendance_toggling_back_to_attending_does_not_double_award()
    {
        var training = NewTraining();
        var memberId = MemberId.New();

        training.ConfirmAttendance(memberId, AttendanceStatus.Attending, DateTime.UtcNow);
        training.ConfirmAttendance(memberId, AttendanceStatus.NotAttending, DateTime.UtcNow);
        training.ClearDomainEvents();
        training.ConfirmAttendance(memberId, AttendanceStatus.Attending, DateTime.UtcNow);

        Assert.DoesNotContain(training.DomainEvents, domainEvent => domainEvent is TrainingAttendanceConfirmed);
    }

    [Fact]
    public void ConfirmAttendance_rejects_unknown_status()
    {
        var training = NewTraining();

        var result = training.ConfirmAttendance(MemberId.New(), AttendanceStatus.Unknown, DateTime.UtcNow);

        Assert.True(result.IsError);
    }
}
