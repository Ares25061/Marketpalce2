namespace MarketplaceApi.Contracts.SearchHistory
{
    public class CreateSearchHistoryRequest
    {
        public int UserId { get; set; }
        public string SearchTerm { get; set; } = null!;
        public int CreatedBy { get; set; }
    }
}