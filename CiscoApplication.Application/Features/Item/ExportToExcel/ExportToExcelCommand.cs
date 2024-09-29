using CiscoApplication.Application.Abstractions.Messaging.Command;
using CiscoApplication.Application.DTOs;

namespace CiscoApplication.Application.Features.Item.ExportToExcel
{
    public record ExportToExcelCommand(ItemSearchDto ItemSearchDto) : ICommand
    {
    }
}
