using ErrorOr;
using FluentValidation;
using Mediator;
using Mtsorella.Api.Common.Auth;
using Mtsorella.Api.Common.Endpoints;
using Mtsorella.Api.Common.Results;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;
using Mtsorella.Api.Domain.Identity;
using Mtsorella.Api.Domain.Inbox;
using Mtsorella.Api.Domain.Members;
using Mtsorella.Api.Persistence.Repositories;
// Disambiguate from System.ApplicationId, which ImplicitUsings brings into scope via `using System;`.
using ApplicationId = Mtsorella.Api.Domain.Common.ApplicationId;

namespace Mtsorella.Api.Features.Identity;

// Admin turns an accepted prihláška into a real account: creates the Member (gamification profile) from the
// application data, the linked UserAccount with a generated temporary password, and marks the application
// Accepted — all in one transaction. The plaintext temp password is returned once for the admin to hand over.
public sealed class CreateMemberAccount : IEndpoint
{
    public sealed record Command(Guid ApplicationId, string LoginEmail) : IRequest<ErrorOr<Response>>;

    public sealed record Response(Guid MemberId, string Email, string TemporaryPassword);

    public sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(command => command.ApplicationId).NotEmpty();
            RuleFor(command => command.LoginEmail).NotEmpty().EmailAddress();
        }
    }

    public sealed class Handler : IRequestHandler<Command, ErrorOr<Response>>
    {
        private readonly IRepository<Application, ApplicationId> _applications;
        private readonly IRepository<Member, MemberId> _members;
        private readonly IUserAccountRepository _accounts;
        private readonly IPasswordHasher _passwordHasher;

        public Handler(
            IRepository<Application, ApplicationId> applications,
            IRepository<Member, MemberId> members,
            IUserAccountRepository accounts,
            IPasswordHasher passwordHasher)
        {
            _applications = applications;
            _members = members;
            _accounts = accounts;
            _passwordHasher = passwordHasher;
        }

        public async ValueTask<ErrorOr<Response>> Handle(Command command, CancellationToken cancellationToken)
        {
            var application = await _applications.GetByIdAsync(new ApplicationId(command.ApplicationId), cancellationToken);
            if (application is null)
            {
                return Error.NotFound("Application.NotFound", "Application not found.");
            }

            if (application.Status == ApplicationStatus.Accepted)
            {
                return Error.Conflict("Application.AlreadyAccepted", "This application already has an account.");
            }

            var loginEmail = Email.Create(command.LoginEmail);
            if (loginEmail.IsError)
            {
                return loginEmail.Errors;
            }

            if (await _accounts.GetByEmailAsync(loginEmail.Value, cancellationToken) is not null)
            {
                return Error.Conflict("UserAccount.EmailTaken", "An account with this email already exists.");
            }

            var member = Member.Create(application.ChildName, application.CategoryOfInterest, application.ParentEmail);
            if (member.IsError)
            {
                return member.Errors;
            }

            var temporaryPassword = PasswordGenerator.Generate();
            var account = UserAccount.Create(
                loginEmail.Value,
                _passwordHasher.Hash(temporaryPassword),
                Role.Member,
                member.Value.Id,
                mustChangePassword: true);
            if (account.IsError)
            {
                return account.Errors;
            }

            application.Accept();

            await _members.AddAsync(member.Value, cancellationToken);
            await _accounts.AddAsync(account.Value, cancellationToken);
            // All three repositories share the request's DbContext, so one save commits Member + UserAccount +
            // the application status change atomically.
            await _accounts.SaveChangesAsync(cancellationToken);

            return new Response(member.Value.Id.Value, loginEmail.Value.Value, temporaryPassword);
        }
    }

    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("/admin/accounts", async (Command command, ISender sender) =>
            {
                ErrorOr<Response> result = await sender.Send(command);
                return result.ToHttpResult(response => Results.Ok(response));
            })
            .WithName("CreateMemberAccount").WithTags("Admin").RequireAuthorization("Admin");
}
