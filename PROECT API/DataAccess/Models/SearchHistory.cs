using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class SearchHistory
    {
        public int SearchHistoryId { get; set; }
        public int? UserId { get; set; }
        public string SearchTerm { get; set; } = null!;
        public DateTime? SearchDate { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual User? User { get; set; }
    }
}
