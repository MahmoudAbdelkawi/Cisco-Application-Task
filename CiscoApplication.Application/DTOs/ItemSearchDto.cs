namespace CiscoApplication.Application.DTOs
{
    public class ItemSearchDto
    {
        public int? Band { get; set; }
        public string? CategoryCode { get; set; }
        public string? Manufacturer { get; set; }
        public string? PartSKU { get; set; }
        public string? ItemDescription { get; set; }
        public decimal? ListPriceFrom { get; set; }
        public decimal? ListPriceTo { get; set; }
        public float? MinimumDiscountFrom { get; set; }
        public float? MinimumDiscountTo { get; set; }
        public ItemOrderEnum? ItemOrderBy { get; set; } = ItemOrderEnum.None;
        public int? PageIndex { get; set; } = 1;
        public int? PageSize { get; set; } = 5;
    }
}
