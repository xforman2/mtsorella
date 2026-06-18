using ErrorOr;
using Mediator;
using Mtsorella.Api.Common.Endpoints;
using Mtsorella.Api.Common.Results;
using Mtsorella.Api.Domain;
using Mtsorella.Api.Persistence.Repositories;

namespace Mtsorella.Api.Features.Products;

public sealed class GetProduct : IEndpoint
{
    public sealed record Query(Guid Id) : IRequest<ErrorOr<Response>>;

    public sealed record Response(Guid Id, string Name, decimal Price, int StockQuantity);

    public sealed class Handler : IRequestHandler<Query, ErrorOr<Response>>
    {
        private readonly IProductRepository _products;

        public Handler(IProductRepository products)
        {
            _products = products;
        }

        public async ValueTask<ErrorOr<Response>> Handle(Query query, CancellationToken cancellationToken)
        {
            Product? product = await _products.GetByIdAsync(query.Id, cancellationToken);

            if (product is null)
            {
                return Error.NotFound(description: $"Product '{query.Id}' was not found.");
            }

            return new Response(product.Id, product.Name, product.Price, product.StockQuantity);
        }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id:guid}", async (Guid id, ISender sender) =>
            {
                ErrorOr<Response> result = await sender.Send(new Query(id));
                return result.ToHttpResult(response => Results.Ok(response));
            })
            .WithName("GetProduct")
            .WithTags("Products");
    }
}
