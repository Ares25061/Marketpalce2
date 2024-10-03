namespace MarketplaceApi.Contracts.PriceHistory
{
    public class GetPriceHistoryResponse
    {
        public int PriceHistoryId { get; set; }
        public int? ProductId { get; set; }
        public decimal Price { get; set; }
        public DateTime? ChangeDate { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}