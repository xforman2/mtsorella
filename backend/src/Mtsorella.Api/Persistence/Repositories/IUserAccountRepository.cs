using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;
using Mtsorella.Api.Domain.Identity;

namespace Mtsorella.Api.Persistence.Repositories;

public interface IUserAccountRepository : IRepository<UserAccount, UserAccountId>
{
    Task<UserAccount?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);

    Task<bool> AnyWithRoleAsync(Role role, CancellationToken cancellationToken = default);
}
