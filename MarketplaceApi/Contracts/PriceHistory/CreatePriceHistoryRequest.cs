namespace MarketplaceApi.Contracts.PriceHistory
{
    public class CreatePriceHistoryRequest
    {
        public int? ProductId { get; set; }
        public decimal Price { get; set; }
        public DateTime? ChangeDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}