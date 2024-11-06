using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class File
    {
        public File()
        {
            FilePermissions = new HashSet<FilePermission>();
            UserFiles = new HashSet<UserFile>();
        }

        public int FileId { get; set; }
        public string FileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public int FileSize { get; set; }
        public string FileType { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual ICollection<FilePermission> FilePermissions { get; set; }
        public virtual ICollection<UserFile> UserFiles { get; set; }
    }
}