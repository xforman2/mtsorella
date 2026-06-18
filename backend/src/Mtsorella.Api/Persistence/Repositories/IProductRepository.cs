using Mtsorella.Api.Domain;

namespace Mtsorella.Api.Persistence.Repositories;

public interface IProductRepository : IRepository<Product>
{
    // Product-specific queries go here, e.g.:
    // Task<Product?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}
