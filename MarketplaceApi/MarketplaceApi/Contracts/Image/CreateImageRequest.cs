namespace MarketplaceApi.Contracts.Image
{
    public class CreateImageRequest
    {
        public int? ProductId { get; set; }
        public string ImageUrl { get; set; } = null!;
        public int? CreatedBy { get; set; }
    }
}