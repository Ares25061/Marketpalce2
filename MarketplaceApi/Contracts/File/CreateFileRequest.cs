namespace MarketplaceApi.Contracts.File
{
    public class CreateFileRequest
    {
        public int FileId { get; set; }
        public string FileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public int FileSize { get; set; }
        public string FileType { get; set; } = null!;
        public int? CreatedBy { get; set; }
    }
}