using ErrorOr;
using FluentValidation;
using Mediator;
using Mtsorella.Api.Common.Endpoints;
using Mtsorella.Api.Common.Results;
using Mtsorella.Api.Domain;
using Mtsorella.Api.Persistence.Repositories;

namespace Mtsorella.Api.Features.Products;

public sealed class CreateProduct : IEndpoint
{
    public sealed record Command(string Name, decimal Price, int StockQuantity)
        : IRequest<ErrorOr<Guid>>;

    public sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(command => command.Name).NotEmpty().MaximumLength(200);
            RuleFor(command => command.Price).GreaterThan(0);
            RuleFor(command => command.StockQuantity).GreaterThanOrEqualTo(0);
        }
    }

    public sealed class Handler : IRequestHandler<Command, ErrorOr<Guid>>
    {
        private readonly IProductRepository _products;

        public Handler(IProductRepository products)
        {
            _products = products;
        }

        public async ValueTask<ErrorOr<Guid>> Handle(Command command, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Price = command.Price,
                StockQuantity = command.StockQuantity
            };

            await _products.AddAsync(product, cancellationToken);
            await _products.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (Command command, ISender sender) =>
            {
                ErrorOr<Guid> result = await sender.Send(command);
                return result.ToHttpResult(id => Results.Created($"/products/{id}", new { id }));
            })
            .WithName("CreateProduct")
            .WithTags("Products");
    }
}
