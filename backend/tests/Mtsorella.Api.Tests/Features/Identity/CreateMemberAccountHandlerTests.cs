using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mtsorella.Api.Common.Auth;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;
using Mtsorella.Api.Domain.Inbox;
using Mtsorella.Api.Domain.Members;
using Mtsorella.Api.Features.Identity;
using Mtsorella.Api.Persistence.Repositories;
using Mtsorella.Api.Tests.Persistence.Outbox;
using ApplicationId = Mtsorella.Api.Domain.Common.ApplicationId;

namespace Mtsorella.Api.Tests.Features.Identity;

public class CreateMemberAccountHandlerTests
{
    private static Application NewApplication() =>
        Application.Submit(
            "Kid Name", new DateOfBirth(new DateOnly(2014, 5, 1)), MemberCategory.Juniors,
            "Parent Name", Email.Create("parent@example.com").Value, PhoneNumber.Create("+421900123456").Value,
            consentGiven: true, submittedOn: DateTimeOffset.UtcNow).Value;

    private static CreateMemberAccount.Handler HandlerFor(SqliteAppDbContext sqlite, PasswordHasherAdapter hasher) =>
        new(
            new Repository<Application, ApplicationId>(sqlite.Context),
            new Repository<Member, MemberId>(sqlite.Context),
            new UserAccountRepository(sqlite.Context),
            hasher);

    [Fact]
    public async Task Creates_member_and_account_and_marks_application_accepted()
    {
        using var sqlite = new SqliteAppDbContext();
        var hasher = new PasswordHasherAdapter();
        var application = NewApplication();
        sqlite.Context.Applications.Add(application);
        await sqlite.Context.SaveChangesAsync();

        var result = await HandlerFor(sqlite, hasher)
            .Handle(new CreateMemberAccount.Command(application.Id.Value, "kid@mtsorella.sk"), default);

        Assert.False(result.IsError);
        Assert.Equal("kid@mtsorella.sk", result.Value.Email);
        Assert.False(string.IsNullOrEmpty(result.Value.TemporaryPassword));

        var reloadedApplication = await sqlite.Context.Applications.FindAsync(application.Id);
        Assert.Equal(ApplicationStatus.Accepted, reloadedApplication!.Status);

        Assert.Equal(1, await sqlite.Context.Members.CountAsync());
        var account = await sqlite.Context.UserAccounts.SingleAsync();
        Assert.Equal(Role.Member, account.Role);
        Assert.True(account.MustChangePassword);
        Assert.NotNull(account.MemberId);
        // The returned temporary password actually verifies against the stored hash.
        Assert.Equal(PasswordVerificationResult.Success, hasher.Verify(account.PasswordHash, result.Value.TemporaryPassword));
    }

    [Fact]
    public async Task Missing_application_is_not_found()
    {
        using var sqlite = new SqliteAppDbContext();

        var result = await HandlerFor(sqlite, new PasswordHasherAdapter())
            .Handle(new CreateMemberAccount.Command(Guid.NewGuid(), "x@mtsorella.sk"), default);

        Assert.True(result.IsError);
        Assert.Equal(ErrorType.NotFound, result.FirstError.Type);
    }

    [Fact]
    public async Task Second_call_on_the_same_application_conflicts()
    {
        using var sqlite = new SqliteAppDbContext();
        var hasher = new PasswordHasherAdapter();
        var application = NewApplication();
        sqlite.Context.Applications.Add(application);
        await sqlite.Context.SaveChangesAsync();

        await HandlerFor(sqlite, hasher).Handle(new CreateMemberAccount.Command(application.Id.Value, "first@mtsorella.sk"), default);
        var second = await HandlerFor(sqlite, hasher).Handle(new CreateMemberAccount.Command(application.Id.Value, "second@mtsorella.sk"), default);

        Assert.True(second.IsError);
        Assert.Equal(ErrorType.Conflict, second.FirstError.Type);
    }
}
