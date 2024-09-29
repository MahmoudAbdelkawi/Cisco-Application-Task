using Ardalis.Result;
using MediatR;

namespace CiscoApplication.Application.Abstractions.Messaging.Query
{
    public interface IQuery : IRequest<IResult>
    {
    }
}
