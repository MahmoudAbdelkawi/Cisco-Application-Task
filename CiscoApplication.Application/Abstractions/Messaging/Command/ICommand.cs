using Ardalis.Result;
using MediatR;

namespace CiscoApplication.Application.Abstractions.Messaging.Command
{
    public interface ICommand : IRequest<IResult>
    {
    }
}
