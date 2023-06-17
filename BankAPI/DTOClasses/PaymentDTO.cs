namespace BankAPI.DTOClasses
{
    public class PaymentDTO
    {
        public int ID { get; set; }
        public string CardUserName { get; set; } = null!;
        public string SecurityNumber { get; set; } = null!;
        public string CardNumber { get; set; } = null!;
        public int CardExpiryMounth { get; set; }
        public int CardExpiryYear { get; set; }
        public decimal ShoppingPrice { get; set; }
    }
}
