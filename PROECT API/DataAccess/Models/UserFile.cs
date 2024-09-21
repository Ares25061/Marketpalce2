using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class UserFile
    {
        public int UserFileId { get; set; }
        public int? UserId { get; set; }
        public int? FileId { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual File? File { get; set; }
        public virtual User? User { get; set; }
    }
}
