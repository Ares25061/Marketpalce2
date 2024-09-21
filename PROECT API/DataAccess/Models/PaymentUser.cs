using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class PaymentUser
    {
        public int PaymentId { get; set; }
        public int? UserId { get; set; }
        public bool? IsActive { get; set; }

        public virtual Payment Payment { get; set; } = null!;
        public virtual User? User { get; set; }
    }
}
