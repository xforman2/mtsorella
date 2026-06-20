using Mtsorella.Api.Domain.Common;

namespace Mtsorella.Api.Persistence.Repositories;

/// <summary>
/// Abstraction over data access for a single aggregate, so handlers depend on this contract rather than
/// on <c>AppDbContext</c>/EF Core directly. <typeparamref name="TId"/> is the aggregate's strongly-typed
/// id, so <see cref="GetByIdAsync"/> can't be handed the wrong kind of id by mistake. All repositories in
/// a request share the same scoped DbContext, so <see cref="SaveChangesAsync"/> commits everything tracked
/// in that request as one unit of work (call it once, after the writes).
/// </summary>
public interface IRepository<TEntity, TId>
    where TEntity : AggregateRoot<TId>
    where TId : struct
{
    Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TEntity>> ListAsync(CancellationToken cancellationToken = default);

    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    void Update(TEntity entity);

    void Remove(TEntity entity);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
