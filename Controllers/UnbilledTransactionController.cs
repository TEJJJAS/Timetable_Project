using CreditCardProj.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace CreditCardProj.Controllers
{
    public class UnbilledTransactionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        CreditCardContext ctx = new CreditCardContext();

        public IActionResult EnterCard()
        {
            return View();
        }

        public IActionResult GetCardDetails(int CardNo)
        {
            TempData["CardNo"] = CardNo;
            IEnumerable<CreditCardTransaction> lst = ctx.CreditCardTransactions.ToList<CreditCardTransaction>();
            IEnumerable ans = lst.Where(x => x.CardNo.Equals(CardNo)).OrderByDescending(x => x.TransactionId);

            return View(ans);
        }


        public IActionResult UnbilledTrans()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UnbilledTrans(CreditCardTransaction obj)
        {
            
            ctx.SaveChanges();
            return RedirectToAction("SearchTransaction");
        }

        public IActionResult SearchTransaction()
        {

            //string mon = (string)TempData["Month"];
            //string yr = (string)TempData["Year"];
            int Cno = Convert.ToInt32(TempData["CardNo"]);

            IEnumerable<CreditCardTransaction> transactions = ctx.CreditCardTransactions
           .Where(x => x.BillingStatus == "PAID" && x.CardNo == Cno).OrderByDescending(x => x.TransactionId).ToList();

            decimal totalAmount = (decimal)transactions.Sum(x => x.TransactionAmt);

            ViewBag.TotalAmount = totalAmount;

            return View(transactions);
        }

    }
}
