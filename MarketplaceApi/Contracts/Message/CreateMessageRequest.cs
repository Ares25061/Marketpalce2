namespace MarketplaceApi.Contracts.Message
{
    public class CreateMessageRequest
    {
        public int? ChatId { get; set; }
        public int? UserId { get; set; }
        public string MessageContent { get; set; } = null!;
    }
}