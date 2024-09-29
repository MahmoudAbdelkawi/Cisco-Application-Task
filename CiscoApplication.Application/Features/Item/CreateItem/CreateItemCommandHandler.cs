using Ardalis.Result;
using CiscoApplication.Application.Abstractions.Contracts;
using CiscoApplication.Application.Abstractions.Messaging.Command;

namespace CiscoApplication.Application.Features.Item.CreateItem
{
    internal class CreateItemCommandHandler : ICommandHandler<CreateItemCommand>
    {
        private readonly IItemRepository _itemRepository;

        public CreateItemCommandHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public async Task<IResult> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            // chechk if SKU is Exists
            var existedItem = await _itemRepository.GetItemWithSKU(request.CreateItemDto.PartSKU);
            if (existedItem is not null)
            {
                return Result.CriticalError("SKU already exists");
            }

            var item = Domain.Entities.Item.Create(
                Guid.NewGuid(),
                request.CreateItemDto.Band,
                request.CreateItemDto.CategoryCode,
                request.CreateItemDto.Manufacturer,
                request.CreateItemDto.PartSKU,
                request.CreateItemDto.ItemDescription,
                request.CreateItemDto.ListPrice,
                request.CreateItemDto.MinimumDiscount
                );

            await _itemRepository.AddItemAsync(item);
            var res = await _itemRepository.SaveChangesAsync();

            return res > 0 ? Result.SuccessWithMessage("Item Is Added Successfully") : Result.Error("Failed to create item");


        }
    }
}
