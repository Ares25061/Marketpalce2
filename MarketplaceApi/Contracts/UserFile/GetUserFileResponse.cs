namespace MarketplaceApi.Contracts.UserFile
{
    public class GetUserFileResponse
    {
        public int UserFileId { get; set; }
        public int UserId { get; set; }
        public int FileId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}