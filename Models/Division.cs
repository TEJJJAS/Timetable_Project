// Models/Division.cs
namespace CreditCardProj.Models
{
    public class Division
    {
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public List<User> Students { get; set; } = new List<User>();
    }
}