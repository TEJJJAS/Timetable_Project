using CreditCardProj.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Collections;
using System.Web.WebPages;

namespace CreditCardProj.Controllers
{
    public class ManageUsageController : Controller
    {
        CreditCardContext ctx = new CreditCardContext();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreditCardDetails(int id)
        {

            IEnumerable<CreditCard> lst = ctx.CreditCards.ToList<CreditCard>();
            IEnumerable ans = lst.Where(x => x.CardNo == id);
            return View(ans);
            
        }


        public IActionResult SetAmtLimit1(CreditCard obj)
        {
            if (obj.CardNo == null)
            {
                ViewBag.ErrorMessage = "Card Not Found...";
            }

            if (obj.AvilableCreditLimit == null)
            {
                ViewBag.ErrorMessage = "Available Credit Limit is null";
            }

            CreditCard obj2 = ctx.CreditCards.FirstOrDefault(c => c.CardNo == obj.CardNo);
            obj2.AvilableCreditLimit = obj.AvilableCreditLimit;
            ctx.SaveChanges();

            return View(obj2);
        }



        public string SetNoOfTransDay(CreditCardTransaction obj)
        {
            CreditCardTransaction obj1 = ctx.CreditCardTransactions.FirstOrDefault(c => c.CardNo == obj.CardNo);

            if (obj1 == null)
            {
                return "Card Not Found...";
            }
            obj1.NoOfTransPerDay = obj.NoOfTransPerDay;
            ctx.SaveChanges();

            return "Transaction limit is set and Updated...";
        }

        public IActionResult SetNoOfTransDay1(CreditCardTransaction obj)
        {
            CreditCardTransaction obj1 = ctx.CreditCardTransactions.FirstOrDefault(c => c.CardNo == obj.CardNo);

            if (obj1 == null)
            {
                return NotFound("Card Not Found...");
            }

            obj1.NoOfTransPerDay = obj.NoOfTransPerDay;
            ctx.SaveChanges();

            var creditCard = ctx.CreditCards.FirstOrDefault(c => c.CardNo == obj.CardNo);

            if (creditCard == null)
            {
                return NotFound("Credit Card Not Found...");
            }

            var viewModel = new CardTransactionModel
            {
                CardNo = creditCard.CardNo,
                CardholderName = creditCard.CardholderName,
                BankName = creditCard.BankName,
                Cvv = creditCard.Cvv,
                ExpiryDate = creditCard.ExpiryDate,
                CardPin = creditCard.CardPin,
                AvailableCreditLimit = creditCard.AvilableCreditLimit,
                NoOfTransPerDay = obj1.NoOfTransPerDay
            };

            return View(viewModel);
        }


      


        public IActionResult SetAmtLimit2()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SetAmtLimit2(CreditCardViewModel model)
        {
            if (model.CardNo ==0)  
            {
                ViewBag.ErrorMessage = "Card Not Found...";
                return View(model);
            }

            CreditCard obj = ctx.CreditCards.FirstOrDefault(c => c.CardNo == model.CardNo);

            if (obj == null)
            {
                ViewBag.ErrorMessage = "Card Not Found...";
                return View(model);
            }

            obj.AvilableCreditLimit = model.AvilableCreditLimit;
            ctx.SaveChanges();

            return View("UpdatedCardDetails", obj); 
        }

        public IActionResult UpdatedCardDetails(CreditCard obj)
        {
            return View(obj);
        }



      
    }
}
