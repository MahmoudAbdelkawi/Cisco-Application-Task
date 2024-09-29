using CiscoApplication.Application.Abstractions.Messaging.Query;
using CiscoApplication.Application.DTOs;

namespace CiscoApplication.Application.Features.Item.GetItems
{
    public record GetItemsQuery(ItemSearchDto ItemSearchDto) : IQuery
    {
    }
}
