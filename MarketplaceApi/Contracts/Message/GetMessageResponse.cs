namespace MarketplaceApi.Contracts.Message
{
    public class GetMessageResponse
    {
        public int MessageId { get; set; }
        public int? ChatId { get; set; }
        public int? UserId { get; set; }
        public string MessageContent { get; set; } = null!;
        public bool? IsRead { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}