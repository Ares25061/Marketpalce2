namespace MarketplaceApi.Contracts.Product
{
    public class CreateProductRequest
    {
        public string ProductName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int SellerId { get; set; }
        public int CreatedBy { get; set; }
    }
}