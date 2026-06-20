using Microsoft.EntityFrameworkCore;
using Mtsorella.Api.Domain.Common;

namespace Mtsorella.Api.Persistence.Repositories;

/// <summary>
/// Default <see cref="IRepository{TEntity, TId}"/> implementation over <see cref="AppDbContext"/>.
/// Aggregate-specific repositories inherit this and add their own query methods.
/// </summary>
public class Repository<TEntity, TId> : IRepository<TEntity, TId>
    where TEntity : AggregateRoot<TId>
    where TId : struct
{
    protected AppDbContext DbContext { get; }
    protected DbSet<TEntity> Set { get; }

    public Repository(AppDbContext dbContext)
    {
        DbContext = dbContext;
        Set = dbContext.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await Set.FindAsync([id], cancellationToken);
    }

    public async Task<IReadOnlyList<TEntity>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await Set.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Set.AddAsync(entity, cancellationToken);
    }

    public void Update(TEntity entity)
    {
        Set.Update(entity);
    }

    public void Remove(TEntity entity)
    {
        Set.Remove(entity);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.SaveChangesAsync(cancellationToken);
    }
}
