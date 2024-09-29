using Ardalis.Result;
using MediatR;

namespace CiscoApplication.Application.Abstractions.Messaging.Command
{
    internal interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, IResult> where TCommand : ICommand
    {
    }
}
