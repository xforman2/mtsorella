using Mediator;

namespace Mtsorella.Api.Domain.Common;

// Marker for something that happened in the domain. Aggregates raise these while handling behaviour;
// the outbox interceptor persists them and the OutboxProcessor publishes them via Mediator. Extending
// INotification makes every event directly dispatchable through IPublisher (decision D6 / issue #29).
public interface IDomainEvent : INotification;
