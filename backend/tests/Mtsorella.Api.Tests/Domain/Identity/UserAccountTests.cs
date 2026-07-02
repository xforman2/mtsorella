using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;
using Mtsorella.Api.Domain.Identity;

namespace Mtsorella.Api.Tests.Domain.Identity;

public class UserAccountTests
{
    private static UserAccount NewAccount() =>
        UserAccount.Create(Email.Create("member@example.com").Value, "hashed", Role.Member, MemberId.New(),
            mustChangePassword: true).Value;

    [Fact]
    public void Create_valid_sets_flag_and_raises_event()
    {
        var account = NewAccount();

        Assert.Equal(Role.Member, account.Role);
        Assert.True(account.MustChangePassword);
        Assert.Contains(account.DomainEvents, domainEvent => domainEvent is UserAccountCreated);
    }

    [Fact]
    public void Create_empty_hash_is_error()
    {
        var result = UserAccount.Create(Email.Create("a@b.com").Value, "  ", Role.Admin, null, true);

        Assert.True(result.IsError);
        Assert.Equal("UserAccount.PasswordHashRequired", result.FirstError.Code);
    }

    [Fact]
    public void Create_guest_role_is_error()
    {
        var result = UserAccount.Create(Email.Create("a@b.com").Value, "hash", Role.Guest, null, true);

        Assert.True(result.IsError);
        Assert.Equal("UserAccount.InvalidRole", result.FirstError.Code);
    }

    [Fact]
    public void ChangePassword_clears_flag_and_raises_event()
    {
        var account = NewAccount();
        account.ClearDomainEvents();

        account.ChangePassword("new-hash");

        Assert.Equal("new-hash", account.PasswordHash);
        Assert.False(account.MustChangePassword);
        Assert.Contains(account.DomainEvents, domainEvent => domainEvent is PasswordChanged);
    }

    [Fact]
    public void UpgradePasswordHash_replaces_hash_without_touching_flag_or_events()
    {
        var account = NewAccount();
        account.ClearDomainEvents();

        account.UpgradePasswordHash("upgraded-hash");

        Assert.Equal("upgraded-hash", account.PasswordHash);
        Assert.True(account.MustChangePassword);
        Assert.Empty(account.DomainEvents);
    }
}
