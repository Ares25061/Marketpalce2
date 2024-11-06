namespace MarketplaceApi.Contracts.Payment
{
    public class GetPaymentResponse
    {
        public int PaymentId { get; set; }
        public string CardNumber { get; set; } = null!;
        public string Cvv { get; set; } = null!;
        public DateTime ExpressionDate { get; set; }
    }
}