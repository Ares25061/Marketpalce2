namespace MarketplaceApi.Contracts.Notification
{
    public class CreateNotificationRequest
    {
        public int? UserId { get; set; }
        public string NotificationType { get; set; } = null!;
        public string Message { get; set; } = null!;
        public int? CreatedBy { get; set; }
    }
}