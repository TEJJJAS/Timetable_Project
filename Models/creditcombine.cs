namespace CreditCardProj.Models
{
    public class creditcombine
    {
        public int? CardNo { get; set; }

        //public Bank Bank { get; set; }

        //public MonthlyInvoice Monthly { get; set; }
        public decimal? OutstandingAmt { get; set; }

        public string? AccountHolderName { get; set; }

        public DateTime? DueDate { get; set; }

        //public creditcombine()
        //{
        //    Bank = new Bank();
        //    Monthly = new MonthlyInvoice();
        //}
    }
}
