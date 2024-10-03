namespace MarketplaceApi.Contracts.PaymentUser
{
    public class GetPaymentUserResponse
    {
        public int PaymentId { get; set; }
        public int? UserId { get; set; }
        public bool? IsActive { get; set; }
    }
}