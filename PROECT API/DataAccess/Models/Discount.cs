using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Discount
    {
        public Discount()
        {
            UserDiscounts = new HashSet<UserDiscount>();
        }

        public int DiscountId { get; set; }
        public string DiscountCode { get; set; } = null!;
        public decimal DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual ICollection<UserDiscount> UserDiscounts { get; set; }
    }
}
