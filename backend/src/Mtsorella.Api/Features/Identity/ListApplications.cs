using ErrorOr;
using Mediator;
using Mtsorella.Api.Common.Endpoints;
using Mtsorella.Api.Common.Results;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Inbox;
using Mtsorella.Api.Persistence.Repositories;
// Disambiguate from System.ApplicationId, which ImplicitUsings brings into scope via `using System;`.
using ApplicationId = Mtsorella.Api.Domain.Common.ApplicationId;

namespace Mtsorella.Api.Features.Identity;

// The admin "prihlášky" list — read-side over submitted applications, optionally filtered by status. This is
// what an admin acts from before calling CreateMemberAccount. Filtering in memory is fine at this volume.
public sealed class ListApplications : IEndpoint
{
    public sealed record Query(ApplicationStatus? Status) : IRequest<ErrorOr<IReadOnlyList<Response>>>;

    public sealed record Response(
        Guid Id,
        string ChildName,
        DateOnly ChildDateOfBirth,
        MemberCategory Category,
        string ParentName,
        string ParentEmail,
        string ParentPhone,
        string? PreviousExperience,
        ApplicationStatus Status,
        DateTimeOffset SubmittedOn);

    public sealed class Handler : IRequestHandler<Query, ErrorOr<IReadOnlyList<Response>>>
    {
        private readonly IRepository<Application, ApplicationId> _applications;

        public Handler(IRepository<Application, ApplicationId> applications)
        {
            _applications = applications;
        }

        public async ValueTask<ErrorOr<IReadOnlyList<Response>>> Handle(Query query, CancellationToken cancellationToken)
        {
            var applications = await _applications.ListAsync(cancellationToken);

            IReadOnlyList<Response> response = applications
                .Where(application => query.Status is null || application.Status == query.Status)
                .Select(application => new Response(
                    application.Id.Value,
                    application.ChildName,
                    application.ChildDateOfBirth.Value,
                    application.CategoryOfInterest,
                    application.ParentName,
                    application.ParentEmail.Value,
                    application.ParentPhone.Value,
                    application.PreviousExperience,
                    application.Status,
                    application.SubmittedOn))
                .ToList();

            return ErrorOrFactory.From(response);
        }
    }

    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/admin/applications", async (string? status, ISender sender) =>
            {
                ApplicationStatus? filter = Enum.TryParse<ApplicationStatus>(status, ignoreCase: true, out var parsed)
                    ? parsed
                    : null;

                ErrorOr<IReadOnlyList<Response>> result = await sender.Send(new Query(filter));
                return result.ToHttpResult(response => Results.Ok(response));
            })
            .WithName("ListApplications").WithTags("Admin").RequireAuthorization("Admin");
}
