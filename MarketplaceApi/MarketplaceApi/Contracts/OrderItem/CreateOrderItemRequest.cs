namespace MarketplaceApi.Contracts.OrderItem
{
    public class CreateOrderItemRequest
    {
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}