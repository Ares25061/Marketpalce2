namespace MarketplaceApi.Contracts.Payment
{
    public class CreatePaymentRequest
    {
        public string CardNumber { get; set; } = null!;
        public string Cvv { get; set; } = null!;
        public DateTime ExpressionDate { get; set; }
    }
}