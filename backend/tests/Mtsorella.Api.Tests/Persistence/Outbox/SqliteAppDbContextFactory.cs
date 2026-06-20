using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Mtsorella.Api.Persistence;

namespace Mtsorella.Api.Tests.Persistence.Outbox;

// Builds an AppDbContext backed by a private in-memory SQLite database that lives for as long as the
// returned handle is held. SQLite (relational) faithfully runs the SaveChanges pipeline — including the
// outbox interceptor and a real transaction — which the EF in-memory provider cannot.
internal sealed class SqliteAppDbContext : IDisposable
{
    private readonly SqliteConnection _connection;

    public AppDbContext Context { get; }

    public SqliteAppDbContext(params IInterceptor[] interceptors)
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_connection)
            .AddInterceptors(interceptors)
            .Options;

        Context = new AppDbContext(options);
        Context.Database.EnsureCreated();
    }

    public void Dispose()
    {
        Context.Dispose();
        _connection.Dispose();
    }
}
