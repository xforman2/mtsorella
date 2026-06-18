using Microsoft.EntityFrameworkCore;
using Mtsorella.Api.Domain;

namespace Mtsorella.Api.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(product => product.Id);
            entity.Property(product => product.Name).HasMaxLength(200).IsRequired();
            entity.Property(product => product.Price).HasPrecision(18, 2);
        });

        base.OnModelCreating(modelBuilder);
    }
}
