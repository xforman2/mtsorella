using Mtsorella.Api.Domain.Common;

namespace Mtsorella.Api.Domain.Trainings;

public sealed record TrainingScheduled(TrainingId TrainingId, DateTimeOffset StartsAt) : IDomainEvent;

public sealed record TrainingAttendanceConfirmed(TrainingId TrainingId, MemberId MemberId, int AttendancePoints) : IDomainEvent;
