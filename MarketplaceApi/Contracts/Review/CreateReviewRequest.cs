namespace MarketplaceApi.Contracts.Review
{
    public class CreateReviewRequest
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
    }
}