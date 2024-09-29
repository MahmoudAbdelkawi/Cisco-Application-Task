using Ardalis.Result;
using CiscoApplication.Application.Abstractions.Contracts;
using CiscoApplication.Application.Abstractions.Messaging.Command;
using CiscoApplication.Application.DTOs;
using CiscoApplication.Application.Helpers;
using CiscoApplication.Application.Specifications.ItemSpecs;
using Microsoft.Extensions.Configuration;

namespace CiscoApplication.Application.Features.Item.ExportToExcel
{
    internal sealed class ExportToExcelCommandHandler : ICommandHandler<ExportToExcelCommand>
    {
        private readonly IFileService _fileService;
        private readonly IItemRepository _itemRepository;
        private readonly IConfiguration _configuration;

        public ExportToExcelCommandHandler(IFileService fileService, IItemRepository itemRepository, IConfiguration configuration)
        {
            _fileService = fileService;
            _itemRepository = itemRepository;
            _configuration = configuration;
        }
        public async Task<IResult> Handle(ExportToExcelCommand request, CancellationToken cancellationToken)
        {
            var spec = new ItemSpec(request.ItemSearchDto, false);
            var items = await _itemRepository.GetItemsWithSpecAsync(spec);

            var UniqueName = Guid.NewGuid().ToString();

            var filePath = _fileService.GetFilePath(DirectoriesEnum.ExportedExcels, UniqueName);

            ExcelHelper.CreateExcelFile(items, filePath);

            var fullpath = Path.Combine(_configuration["MainServerUrl"], DirectoriesEnum.ExportedExcels.ToString(), string.Join(".", UniqueName, "xlsx"));

            return Result.Success<ExportedExcelDto>(new ExportedExcelDto(fullpath, items.Count)
                , "Data Exported Successfully");
        }
    }
}
