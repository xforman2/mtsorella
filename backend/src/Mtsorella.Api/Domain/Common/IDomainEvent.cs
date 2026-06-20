namespace Mtsorella.Api.Domain.Common;

// Marker for something that happened in the domain. Aggregates raise these while handling behaviour;
// dispatch to handlers (Mediator IPublisher) is deferred to a later issue (decision D6).
public interface IDomainEvent;
