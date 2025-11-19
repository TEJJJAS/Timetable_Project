using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CreditCardProj.Models;

public partial class User
{
    [Required]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Please enter a username.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "Please Enter Mobile Number")]
    [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Invalid mobile number. Please enter a 10-digit number.")]
    public string? MobileNo { get; set; }

    [DataType(DataType.Date)]
    [DisplayName("Date Of Birth")]
    [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateOnly? DateOfBirth { get; set; }

    [Required(ErrorMessage = "Please Enter State")]
    [StringLength(50, MinimumLength = 3)]
    [Display(Name = "State")]
    public string? State { get; set; }

    [Required(ErrorMessage = "Please Enter City")]
    [StringLength(50, MinimumLength = 3)]
    [Display(Name = "City")]
    public string? City { get; set; }

    //[Required(ErrorMessage = "Please enter your address.")]
    //[StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
    //public string? Address { get; set; }

    //[Required(ErrorMessage = "Please Enter Pin code")]
    //[RegularExpression(@"^[1-9]{1}[0-9]{2}\\s{0, 1}[0-9]{3}$", ErrorMessage = "Invalid Pin Code. Please Enter valid pin code")]
    public int? PinCode { get; set; }

    //[Required(ErrorMessage = "Please Enter Adhar Number")]
    //public string? AadharNo { get; set; }

    //[Required(ErrorMessage = "Please enter PAN number")]
    //[RegularExpression(@"^[A-Z]{5}[0-9]{4}[A-Z]{1}$", ErrorMessage = "Invalid PAN number. Please enter a valid PAN number")]
   // public string? Panno { get; set; }

    [Required(ErrorMessage = "Please Enter Email Address")]
    [Display(Name = "EmaiId(Email Address)")]
    [RegularExpression(@"^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "Invalid Email Id. Please Enter valid Email Id")]
    public string? EmailId { get; set; }

    [Required(ErrorMessage = "Please Enter Password")]
    [StringLength(50, MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string? Password { get; set; }

    //[Required]
    //[RegularExpression(@"^[0-9]+$", ErrorMessage = "Please your income code")]
  //  public string? Income { get; set; }

  //  public int? CardNo { get; set; }

 //   public virtual ICollection<Bank> Banks { get; set; } = new List<Bank>();

   // public virtual CreditCard? CardNoNavigation { get; set; }
}
