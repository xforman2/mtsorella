using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace Mtsorella.Api.IntegrationTests.Infrastructure;

// Boots the real API (Program.cs DI: AppDbContext + outbox interceptor + OutboxProcessor background worker)
// against the test container by overriding the "Default" connection string. Used by the outbox end-to-end
// test, which needs the actual hosted worker; the mapping round-trips use a plain AppDbContext instead.
public sealed class IntegrationWebAppFactory : WebApplicationFactory<Program>
{
    private readonly string _connectionString;

    public IntegrationWebAppFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Added after appsettings, so this in-memory value wins over the localhost default.
        builder.ConfigureAppConfiguration((_, config) =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:Default"] = _connectionString,
            });
        });
    }
}
