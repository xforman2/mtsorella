using Microsoft.EntityFrameworkCore;

namespace Mtsorella.Api.Persistence.Repositories;

/// <summary>
/// Default <see cref="IRepository{TEntity}"/> implementation over <see cref="AppDbContext"/>.
/// Entity-specific repositories inherit this and add their own query methods.
/// </summary>
public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    protected AppDbContext DbContext { get; }
    protected DbSet<TEntity> Set { get; }

    public Repository(AppDbContext dbContext)
    {
        DbContext = dbContext;
        Set = dbContext.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Set.FindAsync(new object[] { id }, cancellationToken);
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
