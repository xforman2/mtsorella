using Mtsorella.Api.Persistence;

namespace Mtsorella.Api.IntegrationTests.Infrastructure;

// Base for container-backed tests: joins the shared-container collection and resets the data before each
// test so cases stay independent. NewDbContext() hands out a fresh context per save/load step, which is how
// the round-trip tests prove a value really hit the database rather than a tracked in-memory instance.
[Collection(PostgresCollection.Name)]
public abstract class IntegrationTestBase : IAsyncLifetime
{
    private readonly PostgresContainerFixture _fixture;

    protected IntegrationTestBase(PostgresContainerFixture fixture)
    {
        _fixture = fixture;
    }

    protected string ConnectionString => _fixture.ConnectionString;

    protected AppDbContext NewDbContext() => _fixture.CreateDbContext();

    public Task InitializeAsync() => _fixture.ResetAsync();

    public Task DisposeAsync() => Task.CompletedTask;
}
