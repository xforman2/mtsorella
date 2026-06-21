using Microsoft.EntityFrameworkCore;
using Mtsorella.Api.Domain.Challenges;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;
using Mtsorella.Api.Domain.Inbox;
using Mtsorella.Api.Domain.Members;
using Mtsorella.Api.Domain.Trainings;
using Mtsorella.Api.IntegrationTests.Infrastructure;

namespace Mtsorella.Api.IntegrationTests;

// Saves each aggregate through one AppDbContext and reloads it through a fresh one, proving the EF mappings
// (strongly-typed ids, value-object converters, owned references/collections, the Challenge/Submission
// aggregate split) round-trip against real PostgreSQL — not just in-memory SQLite. A representative subset
// covers every distinct mapping shape rather than repeating one test per aggregate.
public sealed class AggregateRoundTripTests : IntegrationTestBase
{
    public AggregateRoundTripTests(PostgresContainerFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task Member_round_trips_with_value_objects_and_owned_collections()
    {
        var email = Email.Create("parent@example.com").Value;
        var member = Member.Create(
            "Ada Lovelace", MemberCategory.Seniors, email,
            nickname: "Ada",
            photo: new MediaRef(MediaKind.Image, "members/ada.jpg", "Ada portrait")).Value;
        member.AwardPoints(110, PointSource.Manual, "Welcome bonus", DateTime.UtcNow); // owned PointTransaction + level-up
        member.EarnBadge(BadgeId.New(), DateTime.UtcNow);                              // owned EarnedBadge
        var memberId = member.Id;

        await using (var db = NewDbContext())
        {
            db.Members.Add(member);
            await db.SaveChangesAsync();
        }

        await using (var db = NewDbContext())
        {
            var loaded = await db.Members.SingleAsync(m => m.Id == memberId);

            Assert.Equal("Ada Lovelace", loaded.FullName);
            Assert.Equal("Ada", loaded.Nickname);
            Assert.Equal(email, loaded.ParentEmail);
            Assert.Equal(110, loaded.Points.Value);
            Assert.Equal(LevelName.Improver, loaded.Level.Name); // derived, but only correct if Points persisted
            Assert.NotNull(loaded.Photo);
            Assert.Equal(MediaKind.Image, loaded.Photo!.Kind);
            Assert.Equal("members/ada.jpg", loaded.Photo.StorageKey);
            Assert.Single(loaded.PointHistory);
            Assert.Equal(110, loaded.PointHistory[0].Amount);
            Assert.Single(loaded.Badges);
        }
    }

    [Fact]
    public async Task Training_round_trips_with_owned_attendances()
    {
        var startsAt = DateTimeOffset.UtcNow.AddDays(1);
        var recurrence = new Recurrence(
            RecurrenceFrequency.Weekly, [DayOfWeek.Monday, DayOfWeek.Wednesday], new DateOnly(2026, 12, 31));
        var training = Training.Create(
            startsAt, startsAt.AddHours(2), "Main Hall", MemberCategory.Juniors, recurrence: recurrence).Value;
        var attendeeId = MemberId.New();
        training.ConfirmAttendance(attendeeId, AttendanceStatus.Attending, DateTime.UtcNow);
        var trainingId = training.Id;

        await using (var db = NewDbContext())
        {
            db.Trainings.Add(training);
            await db.SaveChangesAsync();
        }

        await using (var db = NewDbContext())
        {
            var loaded = await db.Trainings.SingleAsync(t => t.Id == trainingId);

            Assert.Equal("Main Hall", loaded.Location);

            // Recurrence (incl. its DayOfWeek collection) survives the JSON column round-trip.
            Assert.Equal(RecurrenceFrequency.Weekly, loaded.Recurrence.Frequency);
            Assert.Equal([DayOfWeek.Monday, DayOfWeek.Wednesday], loaded.Recurrence.Days);
            Assert.Equal(new DateOnly(2026, 12, 31), loaded.Recurrence.Until);

            Assert.Single(loaded.Attendances);
            Assert.Equal(attendeeId, loaded.Attendances[0].MemberId);
            Assert.Equal(AttendanceStatus.Attending, loaded.Attendances[0].Status);
            Assert.True(loaded.Attendances[0].HasEarnedPoints);
        }
    }

    [Fact]
    public async Task Challenge_and_submission_round_trip_as_separate_aggregates()
    {
        var coachId = CoachId.New();
        var challenge = Challenge.Create(
            "Spin Challenge", "Perform five clean spins.", DateTimeOffset.UtcNow.AddDays(7),
            new MediaRef(MediaKind.Video, "videos/spin.mp4"), coachId).Value;
        var submission = ChallengeSubmission.Submit(
            challenge.Id, MemberId.New(), new MediaRef(MediaKind.Video, "videos/ada-spin.mp4"),
            challenge.Deadline, DateTimeOffset.UtcNow).Value;
        submission.Review(15, coachId, "Great work", DateTimeOffset.UtcNow); // 10 + 5 on-time + 15 quality

        await using (var db = NewDbContext())
        {
            db.Challenges.Add(challenge);
            db.ChallengeSubmissions.Add(submission);
            await db.SaveChangesAsync();
        }

        await using (var db = NewDbContext())
        {
            var loadedChallenge = await db.Challenges.SingleAsync(c => c.Id == challenge.Id);
            Assert.Equal("Spin Challenge", loadedChallenge.Name);
            Assert.Equal(coachId, loadedChallenge.CreatedBy);
            Assert.Equal(MediaKind.Video, loadedChallenge.InstructionalVideo.Kind);

            var loadedSubmission = await db.ChallengeSubmissions.SingleAsync(s => s.Id == submission.Id);
            Assert.Equal(challenge.Id, loadedSubmission.ChallengeId);
            Assert.Equal(SubmissionStatus.Reviewed, loadedSubmission.Status);
            Assert.NotNull(loadedSubmission.Score);
            Assert.Equal(30, loadedSubmission.Score!.Total);
        }
    }

    [Fact]
    public async Task ContactMessage_round_trips()
    {
        var email = Email.Create("fan@example.com").Value;
        var message = ContactMessage.Submit("Jana Nováková", email, "Kedy máte vystúpenie?", DateTimeOffset.UtcNow).Value;
        var messageId = message.Id;

        await using (var db = NewDbContext())
        {
            db.ContactMessages.Add(message);
            await db.SaveChangesAsync();
        }

        await using (var db = NewDbContext())
        {
            var loaded = await db.ContactMessages.SingleAsync(c => c.Id == messageId);

            Assert.Equal("Jana Nováková", loaded.Name);
            Assert.Equal(email, loaded.Email);
            Assert.Equal(InquiryStatus.New, loaded.Status);
        }
    }
}
