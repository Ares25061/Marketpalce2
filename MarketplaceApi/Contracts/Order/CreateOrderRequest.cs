namespace MarketplaceApi.Contracts.Order
{
    public class CreateOrderRequest
    {
        public int BuyerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = null!;
        public int CreatedBy { get; set; }
    }
}