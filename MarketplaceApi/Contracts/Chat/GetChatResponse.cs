namespace MarketplaceApi.Contracts.Chat
{
    public class GetChatResponse
    {
        public int ChatId { get; set; }
        public int OwnerId { get; set; }
        public string Title { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}