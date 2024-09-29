using CiscoApplication.Application.Abstractions.Messaging.Query;

namespace CiscoApplication.Application.Features.Item.GetItemById
{
    public record GetItemByIdQuery(Guid Id) : IQuery
    {
    }
}
