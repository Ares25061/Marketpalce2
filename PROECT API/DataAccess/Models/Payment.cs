using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public string CardNumber { get; set; } = null!;
        public string Cvv { get; set; } = null!;
        public DateTime ExpressionDate { get; set; }

        public virtual PaymentUser? PaymentUser { get; set; }
    }
}
