using Mtsorella.Api.Common.Endpoints;

namespace Mtsorella.Api.Features.Health;

public sealed class GetHealth : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/health", () => Results.Ok(new { status = "healthy" }))
            .WithName("GetHealth")
            .WithTags("Health");
    }
}
