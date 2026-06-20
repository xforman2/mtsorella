using Mtsorella.Api.Domain.Common;

namespace Mtsorella.Api.Domain.Members;

// Owned entity — one line in a member's points history (FR-M37). Amount is signed; awards are positive.
public sealed class PointTransaction : Entity<PointTransactionId>
{
    public int Amount { get; private init; }
    public PointSource Source { get; private init; }
    public string Reason { get; private init; }
    public DateTime OccurredOn { get; private init; }

    private PointTransaction(PointTransactionId id, int amount, PointSource source, string reason, DateTime occurredOn)
    {
        Id = id;
        Amount = amount;
        Source = source;
        Reason = reason;
        OccurredOn = occurredOn;
    }

    internal static PointTransaction Record(int amount, PointSource source, string reason, DateTime occurredOn) =>
        new(PointTransactionId.New(), amount, source, reason, occurredOn);
}
