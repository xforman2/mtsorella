using System.Text.Json;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Mtsorella.Api.Persistence.Outbox;

// The read half of the outbox pattern (issue #29). A background worker that polls for unprocessed
// OutboxMessage rows, rehydrates each event from its stored type + JSON, and publishes it through
// Mediator. Successful rows are stamped ProcessedOnUtc; a failed publish records the error and leaves
// the row unprocessed so it is retried on the next pass (at-least-once delivery).
// BackgroundService is a singleton, so each pass opens its own scope to use the scoped DbContext/IPublisher.
public sealed class OutboxProcessor : BackgroundService
{
    private static readonly TimeSpan PollInterval = TimeSpan.FromSeconds(5);
    private const int BatchSize = 20;

    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<OutboxProcessor> _logger;

    public OutboxProcessor(IServiceScopeFactory scopeFactory, ILogger<OutboxProcessor> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();

                await ProcessAvailableAsync(dbContext, publisher, _logger, BatchSize, stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                // Keep the loop alive across transient faults (e.g. the database being briefly unavailable).
                _logger.LogError(ex, "Outbox processing pass failed; retrying after the poll interval.");
            }

            try
            {
                await Task.Delay(PollInterval, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }
        }
    }

    // One pass over the outbox. Public so it can be exercised directly in tests without the hosting loop.
    public static async Task<int> ProcessAvailableAsync(
        AppDbContext dbContext,
        IPublisher publisher,
        ILogger logger,
        int batchSize,
        CancellationToken cancellationToken)
    {
        var messages = await dbContext.OutboxMessages
            .Where(m => m.ProcessedOnUtc == null)
            .OrderBy(m => m.OccurredOnUtc)
            .Take(batchSize)
            .ToListAsync(cancellationToken);

        foreach (var message in messages)
        {
            try
            {
                var eventType = Type.GetType(message.EventType)
                    ?? throw new InvalidOperationException($"Unknown outbox event type '{message.EventType}'.");
                var domainEvent = JsonSerializer.Deserialize(message.Payload, eventType, OutboxSerializer.Options)
                    ?? throw new InvalidOperationException($"Outbox payload for '{message.EventType}' deserialized to null.");

                await publisher.Publish(domainEvent, cancellationToken);

                message.ProcessedOnUtc = DateTime.UtcNow;
                message.Error = null;
            }
            catch (Exception ex)
            {
                // Leave ProcessedOnUtc null so the row is retried next pass; record why it failed.
                message.Error = ex.ToString();
                logger.LogError(ex, "Failed to publish outbox message {OutboxMessageId} ({EventType}).",
                    message.Id, message.EventType);
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        return messages.Count;
    }
}
