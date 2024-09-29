using Microsoft.AspNetCore.Diagnostics;
using Ardalis.Result;
using FluentValidation;
using Newtonsoft.Json;
using FluentValidation.Results;

namespace CiscoApplication.API.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(
                exception, "Exception occurred: {Message}", exception.Message);

            if (exception is ValidationException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                var validationFailures = JsonConvert.DeserializeObject<List<ValidationFailure>>(exception.Message);
                var validationErrors = validationFailures?.Select(x => new ValidationError(x.PropertyName, x.ErrorMessage, x.ErrorCode, ValidationSeverity.Error)).ToList();
                var validationMessage = Result.Invalid(validationErrors);

                await httpContext.Response.WriteAsJsonAsync(validationMessage, cancellationToken: cancellationToken);
                return true;
            }

            var result = Result.Error(exception.Message);
            await httpContext.Response.WriteAsJsonAsync(result, cancellationToken: cancellationToken);

            return true;
        }
    }
}
