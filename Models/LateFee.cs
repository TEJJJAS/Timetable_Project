using System;
using System.Collections.Generic;

namespace CreditCardProj.Models;

public partial class LateFee
{
    public string? FeeDescription { get; set; }

    public string? FeeType { get; set; }

    public decimal? Amount { get; set; }

    public int? CardNo { get; set; }

    public int Id { get; set; }

    public virtual CreditCard? CardNoNavigation { get; set; }
}
