using Ardalis.Result;
using CiscoApplication.Application.Abstractions.Contracts;
using CiscoApplication.Application.Abstractions.Messaging.Command;
using CiscoApplication.Application.Helpers;

namespace CiscoApplication.Application.Features.Item.ImportFromExcel
{
    internal sealed class ImportFromExcelCommandHandler : ICommandHandler<ImportFromExcelCommand>
    {
        private readonly IFileService _fileService;
        private readonly IItemRepository _itemRepository;

        public ImportFromExcelCommandHandler(IFileService fileService, IItemRepository itemRepository)
        {
            _fileService = fileService;
            _itemRepository = itemRepository;
        }
        public async Task<IResult> Handle(ImportFromExcelCommand request, CancellationToken cancellationToken)
        {
            // Check The Uploaded File Is Excel File
            if (request.ExcelFile.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                return Result.Conflict("The uploaded file is not an excel file.");
            }
            // Check The Uploaded File Is Not Empty
            if (request.ExcelFile.Length == 0)
            {
                return Result.Conflict("The uploaded file is empty.");
            }
            // upload the file
            var (fileName, extension) = _fileService.UploadFile(request.ExcelFile, DirectoriesEnum.ImportedExcels);

            // Read The Excel File
            var itemsRangeResult = ExcelHelper.ReadExcelFiles(_fileService.GetFilePath(DirectoriesEnum.ImportedExcels, fileName),request.SheetIndex);
            if (!itemsRangeResult.IsSuccess)
            {
                return itemsRangeResult;
            }
            if (itemsRangeResult.Value.Rows.Count < 3 || itemsRangeResult.Value.Columns.Count != 9)
            {
                return Result.Conflict("The uploaded file is not in the correct format.");
            }

            // Convert The Excel File To List Of Items
            var items = ExcelHelper.ConvertExcelToItems(itemsRangeResult.Value);

            await _itemRepository.AddItemsRangeAsync(items);

            var res = await _itemRepository.SaveChangesAsync();

            return res >= 1 ? Result.SuccessWithMessage("Data Created Successfully") : Result.Error("An Error Occured During Add Data");
        }
    }
}
