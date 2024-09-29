using CiscoApplication.Application.Abstractions.Messaging.Command;
using Microsoft.AspNetCore.Http;

namespace CiscoApplication.Application.Features.Item.ImportFromExcel
{
    public class ImportFromExcelCommand : ICommand
    {
        public required IFormFile ExcelFile { get; set; }
        public required int SheetIndex { get; set; }
    }
}
