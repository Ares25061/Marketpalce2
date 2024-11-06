using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class PriceHistory
    {
        public int PriceHistoryId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public DateTime ChangeDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}