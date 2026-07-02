using Microsoft.EntityFrameworkCore;
using Mtsorella.Api.Common.Auth;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Tests.Persistence.Outbox;

namespace Mtsorella.Api.Tests.Common.Auth;

public class AdminSeederTests
{
    [Fact]
    public async Task Seeds_an_admin_when_none_exists()
    {
        using var sqlite = new SqliteAppDbContext();

        await AdminSeeder.SeedAsync(sqlite.Context, new PasswordHasherAdapter(), "admin@mtsorella.sk", "InitialPass1");

        var admin = await sqlite.Context.UserAccounts.SingleAsync();
        Assert.Equal(Role.Admin, admin.Role);
        Assert.True(admin.MustChangePassword);
    }

    [Fact]
    public async Task Is_idempotent_once_an_admin_exists()
    {
        using var sqlite = new SqliteAppDbContext();
        var hasher = new PasswordHasherAdapter();

        await AdminSeeder.SeedAsync(sqlite.Context, hasher, "admin@mtsorella.sk", "InitialPass1");
        await AdminSeeder.SeedAsync(sqlite.Context, hasher, "second@mtsorella.sk", "InitialPass2");

        Assert.Equal(1, await sqlite.Context.UserAccounts.CountAsync());
    }
}
