using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Image
    {
        public int ImageId { get; set; }
        public int? ProductId { get; set; }
        public string ImageUrl { get; set; } = null!;
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual Product? Product { get; set; }
    }
}
