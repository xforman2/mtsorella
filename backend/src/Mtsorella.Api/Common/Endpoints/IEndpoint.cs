namespace Mtsorella.Api.Common.Endpoints;

/// <summary>
/// Implemented by each vertical slice so it can register its own route(s).
/// Discovered and mapped automatically by <see cref="EndpointExtensions"/>,
/// which keeps Program.cs free of per-feature route registrations.
/// </summary>
public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
