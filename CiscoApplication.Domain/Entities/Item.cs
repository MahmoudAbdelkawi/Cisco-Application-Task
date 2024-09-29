using Ardalis.Result;
using CiscoApplication.Domain.Primitives;

namespace CiscoApplication.Domain.Entities
{
    public class Item : AggregateRoot
    {
        private Item(Guid id, int band, string categoryCode, string manufacturer, string partSKU, string itemDescription, decimal listPrice, float minimumDiscount) : base(id)
        {
            Band = band;
            CategoryCode = categoryCode;
            Manufacturer = manufacturer;
            PartSKU = partSKU;
            ItemDescription = itemDescription;
            ListPrice = listPrice;
            MinimumDiscount = minimumDiscount;
        }
        public int Band { get; private set; }
        public string CategoryCode { get; private set; }
        public string Manufacturer { get; private set; }
        public string PartSKU { get; private set; }
        public string ItemDescription { get; private set; }
        public decimal ListPrice { get; private set; }
        public float MinimumDiscount { get; private set; }
        public decimal DiscountedPrice
        {
            get
            {
                return ListPrice - (ListPrice * (decimal)MinimumDiscount);
            }
        }
        public static Result<Item> Create(Guid id, int band, string categoryCode, string manufacturer, string partSKU, string itemDescription, decimal listPrice, float minimumDiscount)
        {
            var minmumValidateResult = ValidateminimumDiscount(minimumDiscount);
            if (!minmumValidateResult.IsSuccess)
            {
                return minmumValidateResult;
            }
            return new Item(id, band, categoryCode, manufacturer, partSKU, itemDescription, listPrice, minimumDiscount);
        }
        private static Result ValidateminimumDiscount(float minimumDiscount)
        {
            if (minimumDiscount > 1)
            {
                return Result.Conflict("Minimum discount cannot be greater than 1.");
            }
            return Result.Success();
        }
    }
}
