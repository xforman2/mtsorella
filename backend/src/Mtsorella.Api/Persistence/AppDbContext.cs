using Microsoft.EntityFrameworkCore;

namespace Mtsorella.Api.Persistence;

// No entities are mapped yet — the domain layer is persistence-free for now. Aggregates and their
// configurations land with the persistence issue.
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}
