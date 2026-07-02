using Mtsorella.Api.Domain.Identity;

namespace Mtsorella.Api.Common.Auth;

public interface IJwtTokenIssuer
{
    string IssueToken(UserAccount account);
}
