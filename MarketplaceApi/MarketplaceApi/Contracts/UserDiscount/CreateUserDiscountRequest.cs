namespace MarketplaceApi.Contracts.UserDiscount
{
    public class CreateUserDiscountRequest
    {
        public int? UserId { get; set; }
        public int? DiscountId { get; set; }
    }
}