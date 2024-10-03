namespace MarketplaceApi.Contracts.Order
{
    public class GetOrderResponse
    {
        public int OrderId { get; set; }
        public int? BuyerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string Status { get; set; } = null!;
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}