namespace MarketplaceApi.Contracts.Address
{
    public class GetAddressResponse
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public string AddressLine1 { get; set; } = null!;
        public string? AddressLine2 { get; set; }
        public string City { get; set; } = null!;
        public string State { get; set; }
        public string ZipCode { get; set; } = null!;
        public string Country { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}