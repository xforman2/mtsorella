using Microsoft.AspNetCore.Identity;

namespace Mtsorella.Api.Common.Auth;

// Wraps ASP.NET Core Identity's PasswordHasher<T> (PBKDF2, versioned, rehash-aware) without pulling in the
// rest of the Identity stack. The default hasher ignores the TUser argument, so a shared dummy is fine.
public sealed class PasswordHasherAdapter : IPasswordHasher
{
    private static readonly object Dummy = new();
    private readonly PasswordHasher<object> _hasher = new();

    public string Hash(string password) => _hasher.HashPassword(Dummy, password);

    public PasswordVerificationResult Verify(string hash, string password) =>
        _hasher.VerifyHashedPassword(Dummy, hash, password);
}
