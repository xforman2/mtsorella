using Microsoft.EntityFrameworkCore;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;
using Mtsorella.Api.Domain.Identity;
using Mtsorella.Api.Persistence;

namespace Mtsorella.Api.Common.Auth;

// Bootstraps the first admin (coach) account: there is no prihláška and no existing admin to create it from,
// so one is seeded from configuration on startup. Idempotent — a no-op once any admin exists. Called from
// Program.cs only when Admin:Email/Admin:Password are configured (kept out of source control).
public static class AdminSeeder
{
    public static async Task SeedAsync(
        AppDbContext dbContext,
        IPasswordHasher passwordHasher,
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        if (await dbContext.UserAccounts.AnyAsync(account => account.Role == Role.Admin, cancellationToken))
        {
            return;
        }

        var adminEmail = Email.Create(email);
        if (adminEmail.IsError)
        {
            throw new InvalidOperationException($"Configured admin email '{email}' is not a valid e-mail.");
        }

        var account = UserAccount.Create(
            adminEmail.Value, passwordHasher.Hash(password), Role.Admin, memberId: null, mustChangePassword: true);
        if (account.IsError)
        {
            throw new InvalidOperationException($"Failed to seed admin: {account.FirstError.Description}");
        }

        dbContext.UserAccounts.Add(account.Value);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
