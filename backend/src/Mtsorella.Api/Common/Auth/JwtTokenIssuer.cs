using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Mtsorella.Api.Domain.Identity;

namespace Mtsorella.Api.Common.Auth;

// Issues a signed access token. Claims are added directly (not via a ClaimsIdentity) so their short names
// survive verbatim: `sub` (account id), `role` (Admin/Member — matched by RequireRole), and `member_id`
// for member accounts. Bearer validation in Program.cs mirrors these with NameClaimType/RoleClaimType.
public sealed class JwtTokenIssuer : IJwtTokenIssuer
{
    public const string RoleClaim = "role";
    public const string MemberIdClaim = "member_id";

    private readonly JwtSettings _settings;

    public JwtTokenIssuer(JwtSettings settings)
    {
        _settings = settings;
    }

    public string IssueToken(UserAccount account)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, account.Id.Value.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(RoleClaim, account.Role.ToString()),
        };

        if (account.MemberId is { } memberId)
        {
            claims.Add(new Claim(MemberIdClaim, memberId.Value.ToString()));
        }

        var credentials = new SigningCredentials(_settings.SigningKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(_settings.AccessTokenMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
