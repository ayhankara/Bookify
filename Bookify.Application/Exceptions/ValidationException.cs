using Bookify.Application.Abstractions.Behaviors;

namespace Bookify.Application.Exceptions;

public class ValidationException : Exception
{
    public IEnumerable<ValidationError> Errors { get; }

    public ValidationException(IReadOnlyList<ValidationError> errors) 
    {
        Errors = errors ?? throw new ArgumentNullException(nameof(errors));
    }
}