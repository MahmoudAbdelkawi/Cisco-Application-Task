namespace CiscoApplication.Application.DTOs
{
    public class CreateItemDto
    {
        public int Band { get; set; }
        public string CategoryCode { get; set; }
        public string Manufacturer { get; set; }
        public string PartSKU { get; set; }
        public string ItemDescription { get; set; }
        public decimal ListPrice { get; set; }
        public float MinimumDiscount { get; set; }
    }
}
