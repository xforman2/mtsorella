using System.Text.Json;
using Mediator;
using Microsoft.Extensions.Logging.Abstractions;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Members;
using Mtsorella.Api.Persistence.Outbox;

namespace Mtsorella.Api.Tests.Persistence.Outbox;

public class OutboxProcessorTests
{
    private static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web);

    private static OutboxMessage Pending(IDomainEvent domainEvent) => new()
    {
        Id = Guid.NewGuid(),
        EventType = domainEvent.GetType().AssemblyQualifiedName!,
        Payload = JsonSerializer.Serialize(domainEvent, domainEvent.GetType(), Options),
        OccurredOnUtc = DateTime.UtcNow,
    };

    [Fact]
    public async Task Publishes_pending_events_and_stamps_them_processed()
    {
        using var db = new SqliteAppDbContext();
        var publisher = new RecordingPublisher();
        var message = Pending(new MemberCreated(MemberId.New()));
        db.Context.OutboxMessages.Add(message);
        await db.Context.SaveChangesAsync(CancellationToken.None);

        var processed = await OutboxProcessor.ProcessAvailableAsync(
            db.Context, publisher, NullLogger.Instance, batchSize: 20, CancellationToken.None);

        Assert.Equal(1, processed);
        Assert.Single(publisher.Published);
        Assert.IsType<MemberCreated>(publisher.Published[0]);
        Assert.NotNull(db.Context.OutboxMessages.Single().ProcessedOnUtc);
        Assert.Null(db.Context.OutboxMessages.Single().Error);
    }

    [Fact]
    public async Task Failed_publish_records_error_and_leaves_row_for_retry()
    {
        using var db = new SqliteAppDbContext();
        var publisher = new RecordingPublisher { ThrowOnPublish = new InvalidOperationException("boom") };
        db.Context.OutboxMessages.Add(Pending(new MemberCreated(MemberId.New())));
        await db.Context.SaveChangesAsync(CancellationToken.None);

        await OutboxProcessor.ProcessAvailableAsync(
            db.Context, publisher, NullLogger.Instance, batchSize: 20, CancellationToken.None);

        var row = db.Context.OutboxMessages.Single();
        Assert.Null(row.ProcessedOnUtc);
        Assert.NotNull(row.Error);
        Assert.Contains("boom", row.Error);
    }

    [Fact]
    public async Task Already_processed_rows_are_skipped()
    {
        using var db = new SqliteAppDbContext();
        var publisher = new RecordingPublisher();
        var done = Pending(new MemberCreated(MemberId.New()));
        done.ProcessedOnUtc = DateTime.UtcNow;
        db.Context.OutboxMessages.Add(done);
        db.Context.OutboxMessages.Add(Pending(new MemberCreated(MemberId.New())));
        await db.Context.SaveChangesAsync(CancellationToken.None);

        var processed = await OutboxProcessor.ProcessAvailableAsync(
            db.Context, publisher, NullLogger.Instance, batchSize: 20, CancellationToken.None);

        Assert.Equal(1, processed);
        Assert.Single(publisher.Published);
    }

    private sealed class RecordingPublisher : IPublisher
    {
        public List<object> Published { get; } = [];
        public Exception? ThrowOnPublish { get; init; }

        public ValueTask Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
            where TNotification : INotification
            => Publish((object)notification!, cancellationToken);

        public ValueTask Publish(object notification, CancellationToken cancellationToken = default)
        {
            if (ThrowOnPublish is not null)
            {
                throw ThrowOnPublish;
            }

            Published.Add(notification);
            return ValueTask.CompletedTask;
        }
    }
}
