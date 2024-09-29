using Ardalis.Result;
using MediatR;

namespace CiscoApplication.Application.Abstractions.Messaging.Query
{
    public interface IQueryHandler<in TQuery> : IRequestHandler<TQuery, IResult> where TQuery : IQuery
    {
    }
}
