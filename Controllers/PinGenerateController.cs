using CreditCardProj.Models;
using Microsoft.AspNetCore.Mvc;

namespace CreditCardProj.Controllers
{
    public class PinGenerateController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        CreditCardContext ctx = new CreditCardContext();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GeneratePin()
        {
            return View();
        }

        public IActionResult VerifyDetails(int cardNo, int cvv, DateOnly expirydate)
        {

            var card = ctx.CreditCards.FirstOrDefault(x => x.CardNo == cardNo && x.Cvv == cvv && x.ExpiryDate == expirydate);
            if (card != null)
            {
                TempData["CardNo"] = card.CardNo;
                return View("ChangePin");
            }
            else
            {
                return View("GeneratePin");
            }
        }

        public IActionResult UpdatePin(int newpin, int confirmpin)
        {
            if (newpin == confirmpin)
            {
                int Cno = Convert.ToInt32(TempData["CardNo"]);
                var card = ctx.CreditCards.FirstOrDefault(x => x.CardNo == Cno);

                if (card != null)
                {
                    card.CardPin = newpin;
                    ctx.CreditCards.Update(card);
                    ctx.SaveChanges();
                    return View("UpdatePinsuccess");
                }
            }

            return View("ChangePin");

        }
    }
}

