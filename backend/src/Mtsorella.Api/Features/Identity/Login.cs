using ErrorOr;
using FluentValidation;
using Mediator;
using Microsoft.AspNetCore.Identity;
using Mtsorella.Api.Common.Auth;
using Mtsorella.Api.Common.Endpoints;
using Mtsorella.Api.Common.Results;
using Mtsorella.Api.Domain.Common.ValueObjects;
using Mtsorella.Api.Persistence.Repositories;

namespace Mtsorella.Api.Features.Identity;

// Email + password login. Returns a JWT plus whether the member still has to change a temporary password
// (they get a normal token either way — the frontend routes a must-change user to the change screen).
public sealed class Login : IEndpoint
{
    public sealed record Command(string Email, string Password) : IRequest<ErrorOr<Response>>;

    public sealed record Response(string Token, bool MustChangePassword);

    public sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(command => command.Email).NotEmpty();
            RuleFor(command => command.Password).NotEmpty();
        }
    }

    public sealed class Handler : IRequestHandler<Command, ErrorOr<Response>>
    {
        // Uniform error for every failure mode (unknown email, bad password) so the API can't be used to
        // discover which accounts exist.
        private static Error InvalidCredentials =>
            Error.Unauthorized("Auth.InvalidCredentials", "Invalid email or password.");

        private readonly IUserAccountRepository _accounts;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenIssuer _tokenIssuer;

        public Handler(IUserAccountRepository accounts, IPasswordHasher passwordHasher, IJwtTokenIssuer tokenIssuer)
        {
            _accounts = accounts;
            _passwordHasher = passwordHasher;
            _tokenIssuer = tokenIssuer;
        }

        public async ValueTask<ErrorOr<Response>> Handle(Command command, CancellationToken cancellationToken)
        {
            var email = Email.Create(command.Email);
            if (email.IsError)
            {
                return InvalidCredentials;
            }

            var account = await _accounts.GetByEmailAsync(email.Value, cancellationToken);
            if (account is null)
            {
                return InvalidCredentials;
            }

            var verification = _passwordHasher.Verify(account.PasswordHash, command.Password);
            if (verification == PasswordVerificationResult.Failed)
            {
                return InvalidCredentials;
            }

            if (verification == PasswordVerificationResult.SuccessRehashNeeded)
            {
                account.UpgradePasswordHash(_passwordHasher.Hash(command.Password));
                _accounts.Update(account);
                await _accounts.SaveChangesAsync(cancellationToken);
            }

            return new Response(_tokenIssuer.IssueToken(account), account.MustChangePassword);
        }
    }

    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("/auth/login", async (Command command, ISender sender) =>
            {
                ErrorOr<Response> result = await sender.Send(command);
                return result.ToHttpResult(response => Results.Ok(response));
            })
            .WithName("Login").WithTags("Auth").AllowAnonymous();
}
