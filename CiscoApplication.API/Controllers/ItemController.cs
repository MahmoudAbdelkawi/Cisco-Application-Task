using CiscoApplication.Application.DTOs;
using CiscoApplication.Application.Features.Item.CreateItem;
using CiscoApplication.Application.Features.Item.ExportToExcel;
using CiscoApplication.Application.Features.Item.GetItemById;
using CiscoApplication.Application.Features.Item.GetItems;
using CiscoApplication.Application.Features.Item.ImportFromExcel;
using Microsoft.AspNetCore.Mvc;

namespace CiscoApplication.API.Controllers
{
    [Route("api/[controller]")]
    public class ItemController : ApiBaseController
    {
        // GET: api/Item
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ItemSearchDto itemSearchDto)
        {
            var response = await Mediator.Send(new GetItemsQuery(itemSearchDto));
            return Result(response);
        }
        [HttpPost("Import-Excel")]
        public async Task<IActionResult> ImportExcel([FromForm] ImportFromExcelCommand command)
        {
            var response = await Mediator.Send(command);
            return Result(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateItme([FromBody] CreateItemDto command)
        {
            var response = await Mediator.Send(new CreateItemCommand(command));
            return Result(response);
        }
        [HttpGet("Export-Excel")]
        public async Task<IActionResult> ExportExcel([FromQuery] ItemSearchDto itemSearchDto)
        {
            var response = await Mediator.Send(new ExportToExcelCommand(itemSearchDto));
            return Result(response);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetItemById(Guid id)
        {
            var response = await Mediator.Send(new GetItemByIdQuery(id));
            return Result(response);
        }
    }
}
