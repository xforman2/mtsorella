using ErrorOr;
using FluentValidation;
using Mediator;
using Microsoft.AspNetCore.Identity;
using Mtsorella.Api.Common.Auth;
using Mtsorella.Api.Common.Endpoints;
using Mtsorella.Api.Common.Results;
using Mtsorella.Api.Persistence.Repositories;

namespace Mtsorella.Api.Features.Identity;

// The authenticated caller changes their own password (clears MustChangePassword). Verifies the current
// password first; the new one is validated for strength by the validator.
public sealed class ChangePassword : IEndpoint
{
    public sealed record Command(string CurrentPassword, string NewPassword) : IRequest<ErrorOr<Updated>>;

    public sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(command => command.CurrentPassword).NotEmpty();
            RuleFor(command => command.NewPassword).NotEmpty().MinimumLength(8)
                .NotEqual(command => command.CurrentPassword)
                .WithMessage("The new password must be different from the current one.");
        }
    }

    public sealed class Handler : IRequestHandler<Command, ErrorOr<Updated>>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IUserAccountRepository _accounts;
        private readonly IPasswordHasher _passwordHasher;

        public Handler(ICurrentUser currentUser, IUserAccountRepository accounts, IPasswordHasher passwordHasher)
        {
            _currentUser = currentUser;
            _accounts = accounts;
            _passwordHasher = passwordHasher;
        }

        public async ValueTask<ErrorOr<Updated>> Handle(Command command, CancellationToken cancellationToken)
        {
            if (_currentUser.UserAccountId is not { } userId)
            {
                return Error.Unauthorized("Auth.NotAuthenticated", "Not authenticated.");
            }

            var account = await _accounts.GetByIdAsync(userId, cancellationToken);
            if (account is null)
            {
                return Error.Unauthorized("Auth.NotAuthenticated", "Not authenticated.");
            }

            if (_passwordHasher.Verify(account.PasswordHash, command.CurrentPassword) == PasswordVerificationResult.Failed)
            {
                return Error.Unauthorized("Auth.InvalidCredentials", "Current password is incorrect.");
            }

            account.ChangePassword(_passwordHasher.Hash(command.NewPassword));
            _accounts.Update(account);
            await _accounts.SaveChangesAsync(cancellationToken);

            return Result.Updated;
        }
    }

    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("/auth/change-password", async (Command command, ISender sender) =>
            {
                ErrorOr<Updated> result = await sender.Send(command);
                return result.ToHttpResult(_ => Results.NoContent());
            })
            .WithName("ChangePassword").WithTags("Auth").RequireAuthorization("Member");
}
