using ErrorOr;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;

namespace Mtsorella.Api.Domain.Identity;

// The login credential (aggregate root). Deliberately decoupled from Member/Coach: an account carries
// the email + password hash + role, and links to a Member for member accounts (null for admins). Password
// hashing/verification is infrastructure — the domain only stores the already-hashed value (D10, BE-1/BE-2).
public sealed class UserAccount : AggregateRoot<UserAccountId>
{
    public Email Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public Role Role { get; private set; }
    public MemberId? MemberId { get; private set; }
    public bool MustChangePassword { get; private set; }

    private UserAccount(
        UserAccountId id,
        Email email,
        string passwordHash,
        Role role,
        MemberId? memberId,
        bool mustChangePassword)
    {
        Id = id;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        MemberId = memberId;
        MustChangePassword = mustChangePassword;
    }

    // EF Core materialization ctor; EF populates mapped members after construction.
    private UserAccount()
    {
    }

    public static ErrorOr<UserAccount> Create(
        Email email,
        string passwordHash,
        Role role,
        MemberId? memberId,
        bool mustChangePassword)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            return Error.Validation("UserAccount.PasswordHashRequired", "Password hash is required.");
        }

        // Guest is the absence of an account, never a stored role; only Member/Admin have credentials.
        if (role is not (Role.Member or Role.Admin))
        {
            return Error.Validation("UserAccount.InvalidRole", "An account role must be Member or Admin.");
        }

        var account = new UserAccount(UserAccountId.New(), email, passwordHash, role, memberId, mustChangePassword);
        account.RaiseDomainEvent(new UserAccountCreated(account.Id));
        return account;
    }

    // Sets a new hash and clears the forced-change flag — the "change your temporary password" step.
    public void ChangePassword(string newPasswordHash)
    {
        PasswordHash = newPasswordHash;
        MustChangePassword = false;
        RaiseDomainEvent(new PasswordChanged(Id));
    }

    // Replaces the stored hash with an equivalent, stronger one after a successful verify (hasher upgraded
    // its parameters). Same password, so no flag change and no event — purely a storage upgrade.
    public void UpgradePasswordHash(string newPasswordHash)
    {
        PasswordHash = newPasswordHash;
    }
}
