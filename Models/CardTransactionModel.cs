namespace CreditCardProj.Models
{
    public class CardTransactionModel
    {
        public CreditCard CreditCard { get; set; }
        public CreditCardTransaction CreditCardTransaction { get; set; }

        public int CardNo { get; set; }
        public string? CardholderName { get; set; }
        public string? BankName { get; set; }
        public int? Cvv { get; set; }
        public DateOnly? ExpiryDate { get; set; }
        public int? CardPin { get; set; }
        public decimal? AvailableCreditLimit { get; set; }

        // Property from CreditCardTransaction
        public int? NoOfTransPerDay { get; set; }

        // Constructor to initialize properties
        public CardTransactionModel()
        {
            CreditCard = new CreditCard(); // Initialize CreditCard to avoid null reference
            CreditCardTransaction = new CreditCardTransaction(); // Initialize CreditCardTransaction to avoid null reference
        }

    }
}
