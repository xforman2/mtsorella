using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mtsorella.Api.Common.Auth;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;
using Mtsorella.Api.Domain.Identity;
using Mtsorella.Api.Features.Identity;
using Mtsorella.Api.Persistence.Repositories;
using Mtsorella.Api.Tests.Persistence.Outbox;

namespace Mtsorella.Api.Tests.Features.Identity;

public class ChangePasswordHandlerTests
{
    private sealed class FakeCurrentUser : ICurrentUser
    {
        public FakeCurrentUser(UserAccountId? userAccountId) => UserAccountId = userAccountId;

        public bool IsAuthenticated => UserAccountId is not null;
        public UserAccountId? UserAccountId { get; }
        public Role? Role => Mtsorella.Api.Domain.Common.Role.Member;
        public MemberId? MemberId => null;
    }

    private static async Task<UserAccount> SeedMemberAsync(SqliteAppDbContext sqlite, PasswordHasherAdapter hasher, string password)
    {
        var account = UserAccount.Create(
            Email.Create("member@example.com").Value, hasher.Hash(password), Role.Member, MemberId.New(),
            mustChangePassword: true).Value;
        sqlite.Context.UserAccounts.Add(account);
        await sqlite.Context.SaveChangesAsync();
        return account;
    }

    [Fact]
    public async Task Correct_current_password_changes_it_and_clears_the_flag()
    {
        using var sqlite = new SqliteAppDbContext();
        var hasher = new PasswordHasherAdapter();
        var account = await SeedMemberAsync(sqlite, hasher, "Temp1234");
        var handler = new ChangePassword.Handler(new FakeCurrentUser(account.Id), new UserAccountRepository(sqlite.Context), hasher);

        var result = await handler.Handle(new ChangePassword.Command("Temp1234", "NewStrongPass1"), default);

        Assert.False(result.IsError);
        var reloaded = await sqlite.Context.UserAccounts.FindAsync(account.Id);
        Assert.False(reloaded!.MustChangePassword);
        Assert.Equal(PasswordVerificationResult.Success, hasher.Verify(reloaded.PasswordHash, "NewStrongPass1"));
    }

    [Fact]
    public async Task Wrong_current_password_is_unauthorized()
    {
        using var sqlite = new SqliteAppDbContext();
        var hasher = new PasswordHasherAdapter();
        var account = await SeedMemberAsync(sqlite, hasher, "Temp1234");
        var handler = new ChangePassword.Handler(new FakeCurrentUser(account.Id), new UserAccountRepository(sqlite.Context), hasher);

        var result = await handler.Handle(new ChangePassword.Command("wrong-current", "NewStrongPass1"), default);

        Assert.True(result.IsError);
        Assert.Equal(ErrorType.Unauthorized, result.FirstError.Type);
    }
}
