namespace MarketplaceApi.Contracts.ChatParticipant
{
    public class CreateChatParticipantRequest
    {
        public int? ChatId { get; set; }
        public int? UserId { get; set; }
    }
}