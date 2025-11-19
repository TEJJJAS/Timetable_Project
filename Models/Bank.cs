using System;
using System.Collections.Generic;

namespace CreditCardProj.Models;

public partial class Bank
{
    public string? BankName { get; set; }

    public string? BankHolderName { get; set; }

    public string? BankAccount { get; set; }

    public decimal? BankActBalance { get; set; }

    public string? Ifsccode { get; set; }

    public int? UserId { get; set; }

    public int? CardNo { get; set; }

    public int BankId { get; set; }

    public virtual CreditCard? CardNoNavigation { get; set; }

    public virtual User? User { get; set; }
}
