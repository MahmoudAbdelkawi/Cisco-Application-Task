using Ardalis.Result;
using CiscoApplication.Application.Abstractions.Contracts;
using CiscoApplication.Application.Abstractions.Messaging.Query;

namespace CiscoApplication.Application.Features.Item.GetItemById
{
    internal sealed class GetItemByIdQueryHandler : IQueryHandler<GetItemByIdQuery>
    {
        private readonly IItemRepository _itemRepository;

        public GetItemByIdQueryHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public async Task<IResult> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _itemRepository.GetItemByIdAsync(request.Id);
            if (item is null)
            {
                return Result.NotFound();
            }
            return Result.Success(item);
        }
    }
}
