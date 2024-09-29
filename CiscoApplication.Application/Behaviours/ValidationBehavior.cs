﻿using FluentValidation;
using MediatR;
using Newtonsoft.Json;

namespace CiscoApplication.Application.Behaviours
{
    internal class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : class
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> _validators)
        {
            validators = _validators;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                {
                    var failuresAsString = JsonConvert.SerializeObject(failures);
                    //var message = failures.Select(x => $"{x.PropertyName}" + ": " + x.ErrorMessage + " + "+x.);
                    throw new ValidationException(failuresAsString);
                }
            }
            return await next();
        }
    }
}
