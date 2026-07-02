using Microsoft.EntityFrameworkCore;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;
using Mtsorella.Api.Domain.Identity;

namespace Mtsorella.Api.Persistence.Repositories;

public sealed class UserAccountRepository : Repository<UserAccount, UserAccountId>, IUserAccountRepository
{
    public UserAccountRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<UserAccount?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        return await Set.FirstOrDefaultAsync(account => account.Email == email, cancellationToken);
    }

    public async Task<bool> AnyWithRoleAsync(Role role, CancellationToken cancellationToken = default)
    {
        return await Set.AnyAsync(account => account.Role == role, cancellationToken);
    }
}
