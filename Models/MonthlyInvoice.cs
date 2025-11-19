using System;
using System.Collections.Generic;

namespace CreditCardProj.Models;

public partial class MonthlyInvoice
{
    public int InvoiceId { get; set; }

    public string? InvoiceMonth { get; set; }

    public int? InvoiceYear { get; set; }

    public DateOnly? InvoiceDate { get; set; }

    public DateTime? DueDate { get; set; }

    public decimal? OutstandingAmt { get; set; }

    public decimal? AmtPaid { get; set; }

    public int? CardNo { get; set; }

    public DateTime? PaymentDate { get; set; }

    public virtual CreditCard? CardNoNavigation { get; set; }
}
