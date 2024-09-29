using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Ardalis.Result;
using IResult = Ardalis.Result.IResult;

namespace CiscoApplication.API.Controllers
{
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        private IMediator? _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected IActionResult Result(IResult response)
        {
            // edit response value to add Created message
            if (response.Status == ResultStatus.Created)
            {
                // Set a specific message for "Created" status
                response.GetType().GetProperty("SuccessMessage")!.SetValue(response, "The resource has been successfully created.");
            }
            else if (response.Status == ResultStatus.Unauthorized)
            {
                var errorsMessages = response.Errors.ToList();
                // Add a specific message for "Unauthorized" status
                errorsMessages.Add("You are not authorized to perform this action.");
                response.GetType().GetProperty(nameof(response.Errors))!.SetValue(response, errorsMessages);
            }
            else if (response.Status == ResultStatus.NotFound)
            {
                var errorsMessages = response.Errors.ToList();
                // Add a specific message for "Not Found" status
                errorsMessages.Add("The requested resource could not be found.");
                response.GetType().GetProperty(nameof(response.Errors))!.SetValue(response, errorsMessages);
            }
            else if (response.Status == ResultStatus.Forbidden)
            {
                var errorsMessages = response.Errors.ToList();
                // Add a specific message for "Forbidden" status
                errorsMessages.Add("You do not have permission to access this resource.");
                response.GetType().GetProperty(nameof(response.Errors))!.SetValue(response, errorsMessages);
            }

            return response.Status switch
            {
                ResultStatus.Ok => new OkObjectResult(response),
                ResultStatus.Created => new CreatedResult(string.Empty, response),
                ResultStatus.Unauthorized => new UnauthorizedObjectResult(response),
                ResultStatus.Invalid => new BadRequestObjectResult(response),
                ResultStatus.NotFound => new NotFoundObjectResult(response),
                ResultStatus.Error or ResultStatus.CriticalError => new UnprocessableEntityObjectResult(response),
                ResultStatus.Conflict => new ConflictObjectResult(response),
                ResultStatus.Forbidden => GenerateCustomObjectResult(HttpStatusCode.Forbidden, response),
                ResultStatus.NoContent => new NoContentResult(),
                ResultStatus.Unavailable => GenerateCustomObjectResult(HttpStatusCode.ServiceUnavailable, response),
                _ => new OkObjectResult(response)
            };
        }

        private static ObjectResult GenerateCustomObjectResult(HttpStatusCode statusCode, IResult response)
        {
            var badRequestObjectResult = new ObjectResult(response)
            {
                StatusCode = (int)statusCode
            };
            return badRequestObjectResult;
        }
    }
}
