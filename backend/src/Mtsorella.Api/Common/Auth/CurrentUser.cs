using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Mtsorella.Api.Domain.Common;

namespace Mtsorella.Api.Common.Auth;

public sealed class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ClaimsPrincipal? Principal => _httpContextAccessor.HttpContext?.User;

    public bool IsAuthenticated => Principal?.Identity?.IsAuthenticated ?? false;

    public UserAccountId? UserAccountId =>
        Guid.TryParse(Principal?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value, out var id)
            ? new UserAccountId(id)
            : null;

    public Role? Role =>
        Enum.TryParse<Role>(Principal?.FindFirst(JwtTokenIssuer.RoleClaim)?.Value, out var role)
            ? role
            : null;

    public MemberId? MemberId =>
        Guid.TryParse(Principal?.FindFirst(JwtTokenIssuer.MemberIdClaim)?.Value, out var id)
            ? new MemberId(id)
            : null;
}
