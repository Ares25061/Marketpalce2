using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public int? ProductId { get; set; }
        public int? UserId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual Product? Product { get; set; }
        public virtual User? User { get; set; }
    }
}
