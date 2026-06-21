using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Mtsorella.Api.Persistence;
using Mtsorella.Api.Persistence.Outbox;
using Npgsql;
using Respawn;
using Respawn.Graph;
using Testcontainers.PostgreSql;

namespace Mtsorella.Api.IntegrationTests.Infrastructure;

// Starts a single PostgreSQL container for the whole test assembly, applies the EF Core migrations once,
// and exposes a Respawn checkpoint so each test can reset the data without paying for a fresh container.
// Shared via the [Collection] below; xUnit runs everything in that collection sequentially, which keeps
// the Respawn resets race-free.
public sealed class PostgresContainerFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _container = new PostgreSqlBuilder("postgres:16-alpine")
        .Build();

    private DbConnection _respawnConnection = null!;
    private Respawner _respawner = null!;

    public string ConnectionString => _container.GetConnectionString();

    public async Task InitializeAsync()
    {
        await _container.StartAsync();

        // Deliberately Migrate (not EnsureCreated) so the real migrations are exercised against Postgres.
        await using var db = CreateDbContext();
        await db.Database.MigrateAsync();

        _respawnConnection = new NpgsqlConnection(ConnectionString);
        await _respawnConnection.OpenAsync();
        _respawner = await Respawner.CreateAsync(_respawnConnection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = ["public"],
            // Keep the schema and migration history; only the aggregate/outbox data is wiped between tests.
            TablesToIgnore = [new Table("__EFMigrationsHistory")],
        });
    }

    // Truncates every data table back to empty. Call from a test's InitializeAsync for isolation.
    public Task ResetAsync() => _respawner.ResetAsync(_respawnConnection);

    // An AppDbContext wired exactly like Program.cs (Npgsql + outbox interceptor), pointed at the container.
    public AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(ConnectionString)
            .AddInterceptors(new ConvertDomainEventsToOutboxInterceptor())
            .Options;

        return new AppDbContext(options);
    }

    public async Task DisposeAsync()
    {
        await _respawnConnection.DisposeAsync();
        await _container.DisposeAsync();
    }
}

// The collection that shares the one container across every integration test class.
[CollectionDefinition(Name)]
public sealed class PostgresCollection : ICollectionFixture<PostgresContainerFixture>
{
    public const string Name = "postgres";
}
