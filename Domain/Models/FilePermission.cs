using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class FilePermission
    {
        public int FilePermissionId { get; set; }
        public int FileId { get; set; }
        public int UserId { get; set; }
        public string PermissionLevel { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual File File { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}