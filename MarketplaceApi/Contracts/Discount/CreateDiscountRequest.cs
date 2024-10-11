namespace MarketplaceApi.Contracts.Discount
{
    public class CreateDiscountRequest
    {
        public string DiscountCode { get; set; } = null!;
        public decimal DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int CreatedBy { get; set; }

    }
}