namespace CreditCardProj.Models
{
    public class BillTransactionModel
    {
        public string? CardholderName { get; set; }
        public decimal? AvilableCreditLimit { get; set; }
        public decimal? OutstandingAmt { get; set; }
        public int? BankId { get; set; }
        public string BankName { get; set; }
        public string BankHolderName { get; set; }
        public string BankAccount { get; set; }
        public decimal? BankActBalance { get; set; }
        public string Ifsccode { get; set; }
    }
}
