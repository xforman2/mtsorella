namespace Mtsorella.Api.Persistence.Repositories;

/// <summary>
/// Abstraction over data access for a single entity type, so handlers depend on this contract
/// rather than on <c>AppDbContext</c>/EF Core directly. All repositories in a request share the
/// same scoped DbContext, so <see cref="SaveChangesAsync"/> commits everything tracked in that
/// request as one unit of work (call it once, after the writes).
/// </summary>
public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TEntity>> ListAsync(CancellationToken cancellationToken = default);

    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    void Update(TEntity entity);

    void Remove(TEntity entity);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
