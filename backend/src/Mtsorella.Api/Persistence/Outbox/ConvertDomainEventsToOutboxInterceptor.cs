using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Mtsorella.Api.Domain.Common;

namespace Mtsorella.Api.Persistence.Outbox;

// The write half of the outbox pattern (issue #29). Just before EF saves, this drains the domain events
// from every tracked aggregate and adds them as OutboxMessage rows. Because the rows are added while the
// same SaveChanges is in flight, they commit in the SAME transaction as the aggregate change — so an event
// can never be persisted without its state change, nor lost if the process dies before dispatch.
// Stateless, so a single instance is shared across all DbContext instances.
public sealed class ConvertDomainEventsToOutboxInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        AddOutboxMessages(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        AddOutboxMessages(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void AddOutboxMessages(DbContext? context)
    {
        if (context is null)
        {
            return;
        }

        var aggregates = context.ChangeTracker
            .Entries<IHasDomainEvents>()
            .Where(entry => entry.Entity.DomainEvents.Count > 0)
            .Select(entry => entry.Entity)
            .ToList();

        foreach (var aggregate in aggregates)
        {
            foreach (var domainEvent in aggregate.DomainEvents)
            {
                context.Set<OutboxMessage>().Add(new OutboxMessage
                {
                    Id = Guid.NewGuid(),
                    EventType = domainEvent.GetType().AssemblyQualifiedName!,
                    Payload = JsonSerializer.Serialize(domainEvent, domainEvent.GetType(), OutboxSerializer.Options),
                    OccurredOnUtc = DateTime.UtcNow,
                });
            }

            // Cleared now so a later save of the same instance doesn't re-enqueue already-captured events.
            aggregate.ClearDomainEvents();
        }
    }
}
