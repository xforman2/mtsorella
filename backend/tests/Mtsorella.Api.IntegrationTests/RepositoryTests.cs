using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;
using Mtsorella.Api.Domain.Members;
using Mtsorella.Api.IntegrationTests.Infrastructure;
using Mtsorella.Api.Persistence.Repositories;

namespace Mtsorella.Api.IntegrationTests;

// Exercises the generic Repository<,> CRUD path against real PostgreSQL — the one piece of the persistence
// layer with no coverage before this issue.
public sealed class RepositoryTests : IntegrationTestBase
{
    public RepositoryTests(PostgresContainerFixture fixture) : base(fixture)
    {
    }

    private static Member NewMember() =>
        Member.Create("Repo Tester", MemberCategory.Cadets, Email.Create("repo@example.com").Value).Value;

    [Fact]
    public async Task Add_get_list_and_remove_round_trip_through_the_repository()
    {
        var member = NewMember();

        await using (var db = NewDbContext())
        {
            var repository = new Repository<Member, MemberId>(db);
            await repository.AddAsync(member);
            await repository.SaveChangesAsync();
        }

        await using (var db = NewDbContext())
        {
            var repository = new Repository<Member, MemberId>(db);

            var byId = await repository.GetByIdAsync(member.Id);
            Assert.NotNull(byId);

            var all = await repository.ListAsync();
            Assert.Single(all);

            repository.Remove(byId!);
            await repository.SaveChangesAsync();
        }

        await using (var db = NewDbContext())
        {
            var repository = new Repository<Member, MemberId>(db);
            Assert.Null(await repository.GetByIdAsync(member.Id));
        }
    }
}
