namespace MarketplaceApi.Contracts.FilePermission
{
    public class CreateFilePermissionRequest
    {
        public int FileId { get; set; }
        public int UserId { get; set; }
        public string PermissionLevel { get; set; } = null!;
        public int CreatedBy { get; set; }
    }
}