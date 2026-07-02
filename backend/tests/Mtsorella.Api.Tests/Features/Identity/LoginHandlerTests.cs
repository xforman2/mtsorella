using System.Text;
using ErrorOr;
using Microsoft.IdentityModel.Tokens;
using Mtsorella.Api.Common.Auth;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;
using Mtsorella.Api.Domain.Identity;
using Mtsorella.Api.Features.Identity;
using Mtsorella.Api.Persistence.Repositories;
using Mtsorella.Api.Tests.Persistence.Outbox;

namespace Mtsorella.Api.Tests.Features.Identity;

public class LoginHandlerTests
{
    private static JwtTokenIssuer Issuer() =>
        new(new JwtSettings("mtsorella", "mtsorella", 60,
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("test-signing-key-that-is-long-enough-123"))));

    private static async Task<SqliteAppDbContext> WithMemberAsync(PasswordHasherAdapter hasher, string password)
    {
        var sqlite = new SqliteAppDbContext();
        var account = UserAccount.Create(
            Email.Create("member@example.com").Value, hasher.Hash(password), Role.Member, MemberId.New(),
            mustChangePassword: true).Value;
        sqlite.Context.UserAccounts.Add(account);
        await sqlite.Context.SaveChangesAsync();
        return sqlite;
    }

    [Fact]
    public async Task Valid_credentials_return_a_token_and_the_must_change_flag()
    {
        var hasher = new PasswordHasherAdapter();
        using var sqlite = await WithMemberAsync(hasher, "Temp1234");
        var handler = new Login.Handler(new UserAccountRepository(sqlite.Context), hasher, Issuer());

        var result = await handler.Handle(new Login.Command("member@example.com", "Temp1234"), default);

        Assert.False(result.IsError);
        Assert.False(string.IsNullOrEmpty(result.Value.Token));
        Assert.True(result.Value.MustChangePassword);
    }

    [Fact]
    public async Task Wrong_password_is_unauthorized()
    {
        var hasher = new PasswordHasherAdapter();
        using var sqlite = await WithMemberAsync(hasher, "Temp1234");
        var handler = new Login.Handler(new UserAccountRepository(sqlite.Context), hasher, Issuer());

        var result = await handler.Handle(new Login.Command("member@example.com", "wrong"), default);

        Assert.True(result.IsError);
        Assert.Equal(ErrorType.Unauthorized, result.FirstError.Type);
    }

    [Fact]
    public async Task Unknown_email_is_unauthorized()
    {
        using var sqlite = new SqliteAppDbContext();
        var handler = new Login.Handler(new UserAccountRepository(sqlite.Context), new PasswordHasherAdapter(), Issuer());

        var result = await handler.Handle(new Login.Command("nobody@example.com", "whatever"), default);

        Assert.True(result.IsError);
        Assert.Equal(ErrorType.Unauthorized, result.FirstError.Type);
    }
}
