using Microsoft.EntityFrameworkCore;
using Mtsorella.Api.IntegrationTests.Infrastructure;

namespace Mtsorella.Api.IntegrationTests;

// The fixture applies the migrations against the empty container on startup; if that failed, every test in
// the collection would error. This asserts the migration set is complete and consistent with the model
// snapshot — both migrations present, nothing left pending.
[Collection(PostgresCollection.Name)]
public sealed class MigrationTests
{
    private readonly PostgresContainerFixture _fixture;

    public MigrationTests(PostgresContainerFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task All_migrations_apply_and_none_remain_pending()
    {
        await using var db = _fixture.CreateDbContext();

        var applied = (await db.Database.GetAppliedMigrationsAsync()).ToList();
        var pending = await db.Database.GetPendingMigrationsAsync();

        Assert.Contains(applied, m => m.EndsWith("InitialCreate"));
        Assert.Contains(applied, m => m.EndsWith("AddOutboxMessages"));
        Assert.Empty(pending);
    }
}
