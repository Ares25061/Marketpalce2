﻿namespace MarketplaceApi.Contracts.Notification
{
    public class GetNotificationResponse
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public string NotificationType { get; set; } = null!;
        public string Message { get; set; } = null!;
        public bool IsRead { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}