namespace MarketplaceApi.Contracts.ChatParticipant
{
    public class GetChatParticipantResponse
    {
        public int ChatParticipantId { get; set; }
        public int? ChatId { get; set; }
        public int? UserId { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}