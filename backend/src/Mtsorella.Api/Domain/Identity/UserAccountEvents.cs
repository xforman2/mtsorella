using Mtsorella.Api.Domain.Common;

namespace Mtsorella.Api.Domain.Identity;

public sealed record UserAccountCreated(UserAccountId UserAccountId) : IDomainEvent;

public sealed record PasswordChanged(UserAccountId UserAccountId) : IDomainEvent;
