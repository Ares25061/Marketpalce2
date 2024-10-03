namespace MarketplaceApi.Contracts.UserDiscount
{
    public class GetUserDiscountResponse
    {
        public int UserDiscountId { get; set; }
        public int? UserId { get; set; }
        public int? DiscountId { get; set; }
    }
}