using Ardalis.Result;
using CiscoApplication.Application.Abstractions.Contracts;
using CiscoApplication.Application.Abstractions.Messaging.Query;
using CiscoApplication.Application.Specifications.ItemSpecs;

namespace CiscoApplication.Application.Features.Item.GetItems
{
    internal sealed class GetItemsQueryHandler : IQueryHandler<GetItemsQuery>
    {
        private readonly IItemRepository _itemRepository;

        public GetItemsQueryHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<IResult> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            var spec = new ItemSpec(request.ItemSearchDto,true);

            var items = await _itemRepository.GetItemsWithSpecAsync(spec);
            var itemscount = await _itemRepository.GetItemsCountWithSpecAsync(new ItemSpec(request.ItemSearchDto, false));


            var pagedInfo = new PagedInfo(request.ItemSearchDto.PageIndex.Value,
                request.ItemSearchDto.PageSize.Value,
                itemscount / request.ItemSearchDto.PageSize.Value,
                
                itemscount);

            return Result.Success(items).ToPagedResult(pagedInfo);
        }
    }
}
