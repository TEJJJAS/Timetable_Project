namespace CreditCardProj.Models
{
    public class RegistrationViewModel
    {
        public User User { get; set; }
        //public CreditCard CreditCard { get; set; }

        public RegistrationViewModel()
        {
            User = new User();
            //CreditCard = new CreditCard();
        }
    }
}
