using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Message
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

        public virtual Chat? Chat { get; set; }
        public virtual User? User { get; set; }
    }
}
