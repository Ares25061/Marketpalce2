using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Chat
    {
        public Chat()
        {
            ChatParticipants = new HashSet<ChatParticipant>();
            Messages = new HashSet<Message>();
        }

        public int ChatId { get; set; }
        public int OwnerId { get; set; }
        public string Title { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual ICollection<ChatParticipant> ChatParticipants { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}