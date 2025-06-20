﻿using FluentValidation;
using MediatR;

namespace Bookify.Application.Abstractions.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (Equals(!_validators.Any()))
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);
       
        var validationErrors = _validators
            .Select(v => v.Validate(context))
            .Where(v => v.Errors.Any())
            .SelectMany(result => result.Errors)
            .Select(validationFailure=> new ValidationError(
                    validationFailure.PropertyName,
                    validationFailure.ErrorMessage))
            .ToList();

        if (validationErrors.Any())
        {
            throw new Exceptions.ValidationException(validationErrors);
        }

        return await next();
    }
}