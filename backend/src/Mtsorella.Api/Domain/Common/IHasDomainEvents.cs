namespace Mtsorella.Api.Domain.Common;

// Non-generic view over an aggregate's raised events. AggregateRoot<TId> is generic, so infrastructure
// (the outbox interceptor) needs this id-agnostic surface to drain events from every tracked aggregate.
public interface IHasDomainEvents
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    void ClearDomainEvents();
}
