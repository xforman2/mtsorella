using Mtsorella.Api.Domain.Common;

namespace Mtsorella.Api.Common.Auth;

// The authenticated caller, read from the validated JWT claims. Injected into handlers that need the
// current identity (e.g. change-password, and future member-scoped slices) instead of touching HttpContext.
public interface ICurrentUser
{
    bool IsAuthenticated { get; }

    UserAccountId? UserAccountId { get; }

    Role? Role { get; }

    MemberId? MemberId { get; }
}
