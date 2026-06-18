using ErrorOr;
// Aliased because this namespace ends in ".Results", which would otherwise shadow
// the static Microsoft.AspNetCore.Http.Results class.
using Http = Microsoft.AspNetCore.Http;

namespace Mtsorella.Api.Common.Results;

/// <summary>
/// Maps an <see cref="ErrorOr{T}"/> returned by a handler onto an HTTP <see cref="IResult"/>,
/// so slice endpoints stay one-liners. Validation errors become RFC7807 validation problems;
/// other error types map to their conventional status codes.
/// </summary>
public static class ErrorOrResults
{
    public static IResult ToHttpResult<TValue>(
        this ErrorOr<TValue> result,
        Func<TValue, IResult> onSuccess)
    {
        return result.Match(onSuccess, Problem);
    }

    private static IResult Problem(List<Error> errors)
    {
        if (errors.Count == 0)
        {
            return Http.Results.Problem();
        }

        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            Dictionary<string, string[]> failures = errors
                .GroupBy(error => error.Code)
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(error => error.Description).ToArray());

            return Http.Results.ValidationProblem(failures);
        }

        Error first = errors[0];

        int statusCode = first.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError
        };

        return Http.Results.Problem(statusCode: statusCode, detail: first.Description);
    }
}
