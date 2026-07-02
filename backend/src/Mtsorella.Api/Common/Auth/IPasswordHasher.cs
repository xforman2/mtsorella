using Microsoft.AspNetCore.Identity;

namespace Mtsorella.Api.Common.Auth;

// Hashing/verification is infrastructure, kept out of the domain (UserAccount only stores the hash).
// Returns Identity's PasswordVerificationResult so callers can react to SuccessRehashNeeded and upgrade
// an old hash on a successful login.
public interface IPasswordHasher
{
    string Hash(string password);

    PasswordVerificationResult Verify(string hash, string password);
}
