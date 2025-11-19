using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CreditCardProj.Models;

public partial class CreditCard
{
    [Required(ErrorMessage = "Please enter credit card number")]
    public int CardNo { get; set; }

    [Required(ErrorMessage = "Please enter a username.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Card Holder Name must be between 3 and 50 characters.")]
    public string? CardholderName { get; set; }

    [Required(ErrorMessage = "Please enter bank name.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Bank Name must be between 3 and 50 characters.")]
    public string? BankName { get; set; }

    [Required(ErrorMessage = "Please enter card Cvv.")]
    [RegularExpression(@"^[0-9]{3}$", ErrorMessage = "Please enter a 3-digit number.")]
    public int? Cvv { get; set; }

    [Display(Name = "Card Expiry Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateOnly? ExpiryDate { get; set; }

    [Required(ErrorMessage = "Please enter card pin.")]
    [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Please enter a 4-digit card pin.")]
    public int? CardPin { get; set; }


    [Required(ErrorMessage = "Please enter card limit.")]
    public decimal? AvilableCreditLimit { get; set; }

    public string? IsBlocked { get; set; }

    public virtual ICollection<Bank> Banks { get; set; } = new List<Bank>();

    public virtual ICollection<CreditCardTransaction> CreditCardTransactions { get; set; } = new List<CreditCardTransaction>();

    public virtual ICollection<LateFee> LateFees { get; set; } = new List<LateFee>();

    public virtual ICollection<MonthlyInvoice> MonthlyInvoices { get; set; } = new List<MonthlyInvoice>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
