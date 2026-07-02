using Microsoft.IdentityModel.Tokens;

namespace Mtsorella.Api.Common.Auth;

// Resolved JWT configuration shared by the token issuer and the bearer validation, so both sign/validate
// with the same key. Built once in Program.cs (the signing key comes from Jwt:Secret, or a random per-process
// key when unset — see Program.cs). Registered as a singleton.
public sealed class JwtSettings
{
    public JwtSettings(string issuer, string audience, int accessTokenMinutes, SecurityKey signingKey)
    {
        Issuer = issuer;
        Audience = audience;
        AccessTokenMinutes = accessTokenMinutes;
        SigningKey = signingKey;
    }

    public string Issuer { get; }
    public string Audience { get; }
    public int AccessTokenMinutes { get; }
    public SecurityKey SigningKey { get; }
}
