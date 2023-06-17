namespace BankAPI.Models.Entities
{
    public class CardInfo
    {
        public int ID { get; set; }
        public string CardUserName { get; set; } = null!;
        public string SecurityNumber { get; set; } = null!;
        public string CardNumber { get; set; } = null!;
        public int CardExpiryMounth { get; set; }
        public int CardExpiryYear { get; set; }
        public decimal Limit { get; set; }//Card's limit
        public decimal Balance { get; set; }//its decremented when you shopping
    }
}
