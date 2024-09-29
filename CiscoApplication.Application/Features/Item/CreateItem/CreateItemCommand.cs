using CiscoApplication.Application.Abstractions.Messaging.Command;
using CiscoApplication.Application.DTOs;

namespace CiscoApplication.Application.Features.Item.CreateItem
{
    public record CreateItemCommand(CreateItemDto CreateItemDto) : ICommand
    {
    }
}
