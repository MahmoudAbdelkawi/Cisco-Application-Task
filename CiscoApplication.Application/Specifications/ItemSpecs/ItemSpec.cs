using CiscoApplication.Application.DTOs;
using CiscoApplication.Domain.Entities;

namespace CiscoApplication.Application.Specifications.ItemSpecs
{
    public class ItemSpec : BaseSpecification<Item>
    {
        public ItemSpec(ItemSearchDto itemSearchDto, bool applyPagination = true)
        {
            if (itemSearchDto.Band.HasValue)
                AddCriteria(i => i.Band == itemSearchDto.Band);

            if (!string.IsNullOrEmpty(itemSearchDto.CategoryCode))
                AddCriteria(i => i.CategoryCode == itemSearchDto.CategoryCode);

            if (!string.IsNullOrEmpty(itemSearchDto.Manufacturer))
                AddCriteria(i => i.Manufacturer.Contains(itemSearchDto.Manufacturer));

            if (!string.IsNullOrEmpty(itemSearchDto.PartSKU))
                AddCriteria(i => i.PartSKU.Contains(itemSearchDto.PartSKU));

            if (!string.IsNullOrEmpty(itemSearchDto.ItemDescription))
                AddCriteria(i => i.ItemDescription.Contains(itemSearchDto.ItemDescription));

            if (itemSearchDto.ListPriceFrom.HasValue)
                AddCriteria(i => i.ListPrice >= itemSearchDto.ListPriceFrom);

            if (itemSearchDto.ListPriceTo.HasValue)
                AddCriteria(i => i.ListPrice <= itemSearchDto.ListPriceTo);

            if (itemSearchDto.MinimumDiscountFrom.HasValue)
                AddCriteria(i => i.MinimumDiscount >= itemSearchDto.MinimumDiscountFrom);

            if (itemSearchDto.MinimumDiscountTo.HasValue)
                AddCriteria(i => i.MinimumDiscount <= itemSearchDto.MinimumDiscountTo);

            if (itemSearchDto.ItemOrderBy != ItemOrderEnum.None)
            {
                if (itemSearchDto.ItemOrderBy == ItemOrderEnum.PartSKUAscending)
                    AddOrderByAsc(i => i.PartSKU);
                else if (itemSearchDto.ItemOrderBy == ItemOrderEnum.PartSKUDescending)
                    AddOrderByDesc(i => i.PartSKU);
                else if (itemSearchDto.ItemOrderBy == ItemOrderEnum.DiscountedPriceAscending)
                    AddOrderByAsc(i => i.ListPrice);
                else if (itemSearchDto.ItemOrderBy == ItemOrderEnum.DiscountedPriceAscending)
                    AddOrderByDesc(i => i.ListPrice);
            }
            if (applyPagination)
            {
                ApplyPagination(itemSearchDto.PageSize.Value * (itemSearchDto.PageIndex.Value - 1), itemSearchDto.PageSize.Value);
            }
        }
    }
}
