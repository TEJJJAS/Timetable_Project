using CreditCardProj.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CreditCardProj.Controllers
{
    public class BilledTransactionController : Controller
    {
        CreditCardContext ctx = new CreditCardContext();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EnterCardNo()
        {
            return View();
        }

        public IActionResult BillTransaction(int CardNo)
      {
              var qry = from creditCard in ctx.CreditCards
              join monthlyInvoice in ctx.MonthlyInvoices on creditCard.CardNo equals monthlyInvoice.CardNo
              join bank in ctx.Banks on creditCard.CardNo equals bank.CardNo
              where creditCard.CardNo == CardNo
              select new BillTransactionModel
              {
                  CardholderName = creditCard.CardholderName,
                  AvilableCreditLimit = creditCard.AvilableCreditLimit,
                  OutstandingAmt = monthlyInvoice.OutstandingAmt,
                  BankId = bank.BankId,
                  BankName = bank.BankName,
                  BankHolderName = bank.BankHolderName,
                  BankAccount = bank.BankAccount,
                  BankActBalance = bank.BankActBalance,
                  Ifsccode = bank.Ifsccode
              };

                return View(qry.ToList());
}




        public IActionResult Read(int CardNo)
        {
            IEnumerable<CreditCard> lst = ctx.CreditCards.ToList<CreditCard>();
            IEnumerable ans = lst.Where(x => x.CardNo == CardNo);
            return View(ans);
        }

        public IActionResult ReadTransact(int CardNo)
        {
            IEnumerable<CreditCardTransaction> lst = ctx.CreditCardTransactions.ToList<CreditCardTransaction>();
            IEnumerable ans = lst.Where(x => x.CardNo == CardNo);
            return View(ans);
        }


        public JsonResult BillTransactions(int CardNo)
        {
            IEnumerable<CreditCard> lst = ctx.CreditCards.ToList<CreditCard>();
            var qry = from CreditCard in ctx.CreditCards
                      join MonthlyInvoice in ctx.MonthlyInvoices on CreditCard.CardNo equals MonthlyInvoice.InvoiceId
                      where CreditCard.CardNo == CardNo
                      join Bank in ctx.Banks on CreditCard.CardNo equals Bank.CardNo
                      where CreditCard.CardNo == CardNo
                      select new
                      {
                          CreditCard.CardholderName,
                          CreditCard.AvilableCreditLimit,

                          MonthlyInvoice.OutstandingAmt,

                          Bank.BankId,
                          Bank.BankName,
                          Bank.BankHolderName,
                          Bank.BankAccount,
                          Bank.BankActBalance,
                          Bank.Ifsccode

                      };
            return Json(qry);
        }

       


    }
}
