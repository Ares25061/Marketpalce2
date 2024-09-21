using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class ChatParticipant
    {
        public int ChatParticipantId { get; set; }
        public int? ChatId { get; set; }
        public int? UserId { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual Chat? Chat { get; set; }
        public virtual User? User { get; set; }
    }
}
