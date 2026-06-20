namespace Mtsorella.Api.Persistence.Outbox;

// A persisted domain event awaiting dispatch (outbox pattern, issue #29). Written in the same
// transaction as the aggregate change by ConvertDomainEventsToOutboxInterceptor, then published by
// OutboxProcessor. This is infrastructure, not a domain aggregate — a plain Guid id, no strongly-typed id.
public sealed class OutboxMessage
{
    public Guid Id { get; set; }

    // Assembly-qualified type name of the IDomainEvent, used to rehydrate Payload at dispatch time.
    public required string EventType { get; set; }

    // The event serialized as JSON (System.Text.Json, default options).
    public required string Payload { get; set; }

    public DateTime OccurredOnUtc { get; set; }

    // Null until the processor has published the event. Set once it is dispatched successfully.
    public DateTime? ProcessedOnUtc { get; set; }

    // Last failure detail; set when a publish attempt threw, leaving the row for retry.
    public string? Error { get; set; }
}
