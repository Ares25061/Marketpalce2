﻿namespace MarketplaceApi.Contracts.FilePermission
{
    public class GetFilePermissionResponse
    {
        public int FilePermissionId { get; set; }
        public int FileId { get; set; }
        public int UserId { get; set; }
        public string PermissionLevel { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}