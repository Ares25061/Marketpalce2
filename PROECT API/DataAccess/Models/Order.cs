using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int OrderId { get; set; }
        public int? BuyerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string Status { get; set; } = null!;
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual User? Buyer { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
