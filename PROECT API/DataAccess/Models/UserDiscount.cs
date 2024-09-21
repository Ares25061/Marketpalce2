using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class UserDiscount
    {
        public int UserDiscountId { get; set; }
        public int? UserId { get; set; }
        public int? DiscountId { get; set; }

        public virtual Discount? Discount { get; set; }
        public virtual User? User { get; set; }
    }
}
