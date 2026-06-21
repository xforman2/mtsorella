using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;
using Mtsorella.Api.Domain.Members;
using Mtsorella.Api.IntegrationTests.Infrastructure;
using Mtsorella.Api.Persistence;

namespace Mtsorella.Api.IntegrationTests;

// End-to-end proof of the outbox against real PostgreSQL: saving an aggregate through the running app's DI
// writes an OutboxMessage in the same transaction (the interceptor), and the hosted OutboxProcessor then
// dispatches it and stamps ProcessedOnUtc. The worker polls on an interval, so dispatch is awaited with a
// bounded poll rather than asserted instantly.
public sealed class OutboxEndToEndTests : IntegrationTestBase
{
    public OutboxEndToEndTests(PostgresContainerFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task Saving_an_aggregate_writes_and_dispatches_an_outbox_message()
    {
        await using var factory = new IntegrationWebAppFactory(ConnectionString);
        factory.CreateClient(); // boots the host so the OutboxProcessor background worker starts polling

        using (var scope = factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var member = Member.Create("Outbox Tester", MemberCategory.Seniors, Email.Create("outbox@example.com").Value).Value;
            db.Members.Add(member);
            await db.SaveChangesAsync(); // interceptor writes MemberCreated to the outbox in the same transaction
        }

        // The row exists immediately, regardless of whether the worker has dispatched it yet.
        await using (var verify = NewDbContext())
        {
            var row = await verify.OutboxMessages.SingleAsync();
            Assert.Contains(nameof(MemberCreated), row.EventType);
        }

        // The hosted worker dispatches and stamps it within a bounded time.
        await WaitUntilDispatchedAsync(TimeSpan.FromSeconds(30));
    }

    private async Task WaitUntilDispatchedAsync(TimeSpan timeout)
    {
        var deadline = DateTime.UtcNow + timeout;
        while (DateTime.UtcNow < deadline)
        {
            await using var db = NewDbContext();
            var row = await db.OutboxMessages.SingleOrDefaultAsync();
            if (row is { ProcessedOnUtc: not null })
            {
                Assert.Null(row.Error);
                return;
            }

            await Task.Delay(TimeSpan.FromMilliseconds(500));
        }

        Assert.Fail("Outbox message was not dispatched within the timeout.");
    }
}
