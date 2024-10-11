namespace MarketplaceApi.Contracts.Chat
{
    public class CreateChatRequest
    {
        public string Title { get; set; } = null!;
        public int OwnerId { get; set; }
        public int ModificatedBy { get; set; }
    }
}