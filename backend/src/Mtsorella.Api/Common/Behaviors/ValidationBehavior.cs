using ErrorOr;
using FluentValidation;
using Mediator;

namespace Mtsorella.Api.Common.Behaviors;

/// <summary>
/// Mediator pipeline behavior that runs FluentValidation validators before the handler.
/// On failure it short-circuits and returns the errors AS an <see cref="ErrorOr{T}"/> value
/// (no exceptions for control flow). Only applies to requests whose response is
/// <see cref="IErrorOr"/>, so handlers never contain validation checks.
/// </summary>
public sealed class ValidationBehavior<TMessage, TResponse>
    : IPipelineBehavior<TMessage, TResponse>
    where TMessage : notnull, IMessage
    where TResponse : IErrorOr
{
    private readonly IEnumerable<IValidator<TMessage>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TMessage>> validators)
    {
        _validators = validators;
    }

    public async ValueTask<TResponse> Handle(
        TMessage message,
        MessageHandlerDelegate<TMessage, TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next(message, cancellationToken);
        }

        var context = new ValidationContext<TMessage>(message);

        List<Error> errors = _validators
            .Select(validator => validator.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(failure => failure is not null)
            .Select(failure => Error.Validation(failure.PropertyName, failure.ErrorMessage))
            .ToList();

        if (errors.Count == 0)
        {
            return await next(message, cancellationToken);
        }

        // ErrorOr<T> defines an implicit conversion from List<Error>; the dynamic cast
        // lets us use it even though TResponse is only statically known as IErrorOr.
        return (dynamic)errors;
    }
}
