namespace Mtsorella.Api.Domain.Common;

// Identity + identity-based equality. TId is a strongly-typed id (a record struct from Ids.cs).
// Two entities are equal when they are the same concrete type and share the same Id.
public abstract class Entity<TId> : IEquatable<Entity<TId>>
    where TId : struct
{
    public TId Id { get; protected init; }

    public bool Equals(Entity<TId>? other) =>
        other is not null && other.GetType() == GetType() && other.Id.Equals(Id);

    public override bool Equals(object? obj) => obj is Entity<TId> entity && Equals(entity);

    public override int GetHashCode() => HashCode.Combine(GetType(), Id);
}
