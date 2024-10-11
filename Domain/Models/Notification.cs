using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Notification
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public string NotificationType { get; set; } = null!;
        public string Message { get; set; } = null!;
        public bool IsRead { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual User User { get; set; } = null!;
    }
}