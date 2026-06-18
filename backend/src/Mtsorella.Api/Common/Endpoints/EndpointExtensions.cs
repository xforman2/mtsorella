using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Mtsorella.Api.Common.Endpoints;

public static class EndpointExtensions
{
    /// <summary>
    /// Registers every <see cref="IEndpoint"/> in the given assembly with DI so they
    /// can be resolved and mapped at startup.
    /// </summary>
    public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
    {
        ServiceDescriptor[] descriptors = assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false }
                           && type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(descriptors);

        return services;
    }

    /// <summary>
    /// Asks every registered <see cref="IEndpoint"/> to map its route(s).
    /// </summary>
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        IEnumerable<IEndpoint> endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        foreach (IEndpoint endpoint in endpoints)
        {
            endpoint.MapEndpoint(app);
        }

        return app;
    }
}
