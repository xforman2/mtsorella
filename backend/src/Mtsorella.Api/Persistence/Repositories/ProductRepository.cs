using Mtsorella.Api.Domain;

namespace Mtsorella.Api.Persistence.Repositories;

public sealed class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext dbContext)
        : base(dbContext)
    {
    }
}
