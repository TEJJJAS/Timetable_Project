using CreditCardProj.Models;
using Microsoft.AspNetCore.Mvc;

namespace CreditCardProj.Controllers
{
    public class BankPaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult EnterCardNo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EnterCardNo(int CardNo)
        {
            var exists = ctx.MonthlyInvoices.Any(p => p.CardNo == CardNo);
            if(!exists)
            {
                ModelState.AddModelError("CardNo","Card number not found");
                return View();
            }

            return RedirectToAction("BankDetailsGet",new {CardNo});

        }

        CreditCardContext ctx = new CreditCardContext();
        creditcombine ccb = new creditcombine();

        [HttpGet]
        public IActionResult BankDetailsGet(creditcombine obj)
        {
            var m1 = ctx.MonthlyInvoices.FirstOrDefault(p => p.CardNo == obj.CardNo);
            var b1 = ctx.Banks.FirstOrDefault(q => q.CardNo==obj.CardNo);
            if (m1 == null)
            {
                return NotFound();
            }

            var model = new creditcombine
            {
                CardNo = m1.CardNo,
                OutstandingAmt = m1.OutstandingAmt,
                DueDate=m1.DueDate,
                AccountHolderName=b1.BankHolderName
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult BankDetails(creditcombine obj)
        {
            return RedirectToAction("DebitGet", obj);
        }

        [HttpGet]
        public IActionResult DebitGet(int CardNo)
        {
            string ans = "";
            creditcombine cc= new creditcombine();
            cc.CardNo=CardNo;
            IEnumerable<MonthlyInvoice> minvoice = ctx.MonthlyInvoices.Where(x => x.CardNo == CardNo);
            MonthlyInvoice mi = minvoice.Last();
            cc.Monthly = mi;

            Bank bank1 = ctx.Banks.FirstOrDefault(b => b.CardNo == CardNo);

            cc.Bank = bank1;

            return View(cc);
        }

        [HttpPost]
        public IActionResult DebitGet(creditcombine obj)
        {
            string ans = "";
            if (obj.Monthly.OutstandingAmt > obj.Bank.BankActBalance)
            {
                ans = ans + "Insufficient Balance";
                ModelState.AddModelError("k1", ans);

            }
            else if(obj.Monthly.OutstandingAmt==0)
            {
                ans = ans + "Balance Already Paid";
                ModelState.AddModelError("k1", ans);
            }

            if (ModelState.IsValid)
                return View("PinEntry", obj);

            return View(obj);
        }

        [HttpPost]
        public IActionResult ProcessDebit(creditcombine obj, string pin)
        {
           // var user1 = ctx.Users.FirstOrDefault(u => u.CardNo == obj.CardNo);
            Bank bank = ctx.Banks.FirstOrDefault(u => u.CardNo == obj.CardNo);


            //if (user1.Password != pin)
            //{
            //    ModelState.AddModelError("pin","PIN required to Make Payment");
            //    return View("PinEntry",obj);

            //}

           

            var monthlyInvoice = ctx.MonthlyInvoices
                                    .Where(b => b.CardNo == obj.CardNo)
                                    .FirstOrDefault();

            if (monthlyInvoice == null)
            {
                return NotFound();
            }

            if (monthlyInvoice.OutstandingAmt < bank.BankActBalance)
            {
                CalculateLateFee(monthlyInvoice, bank);

                var CreditTrans = new CreditCardTransaction();
                {
                    CreditTrans.CardNo = bank.CardNo;
                    CreditTrans.TransactionDate = DateOnly.FromDateTime(DateTime.UtcNow);
                    CreditTrans.TransactionAmt = monthlyInvoice.OutstandingAmt;
                    CreditTrans.BillerName = bank.BankHolderName;
                    CreditTrans.BillingStatus = "PAID";

                    var currentRewardPoints = ctx.CreditCardTransactions
                           .Where(ct => ct.CardNo == bank.CardNo)
                           .OrderByDescending(ct => ct.RewardPoints)
                           .Select(ct => ct.RewardPoints)
                           .FirstOrDefault();


                    int newRewardPoints = Convert.ToInt32(monthlyInvoice.OutstandingAmt * 0.04m);


                    var sum = currentRewardPoints + newRewardPoints;

                    CreditTrans.RewardPoints = sum;

                    var nooftrans = ctx.CreditCardTransactions
                                                   .Where(ct => ct.CardNo == bank.CardNo)
                                                   .Select(ct => ct.RewardPoints)
                                                   .Count();
                    var sum1 = nooftrans + 1;
                    CreditTrans.NoOfTransPerDay = sum1;



                    monthlyInvoice.AmtPaid = monthlyInvoice.OutstandingAmt;
                    var b = monthlyInvoice.OutstandingAmt - monthlyInvoice.OutstandingAmt;
                    monthlyInvoice.OutstandingAmt = b;


                    ctx.CreditCardTransactions.Add(CreditTrans);

                    ctx.MonthlyInvoices.Update(monthlyInvoice);
                    ctx.SaveChanges();

                    return RedirectToAction("PaymentConfirmation");
                }
            }
            else
            {
                return View("Error");
            }



        }
        public IActionResult PaymentConfirmation()
        {
            return View();
        }

        public void CalculateLateFee(MonthlyInvoice monthly, Bank bank)
        {
            DateTime currentDate = DateTime.UtcNow.Date;
            DateTime dueDate = Convert.ToDateTime(monthly.DueDate);
            monthly.PaymentDate = currentDate;

            if (currentDate > dueDate)
            {
                decimal lateChargePerDay = 5;
                decimal outstandingAmount = Convert.ToDecimal(monthly.OutstandingAmt);
                TimeSpan lateDuration = currentDate - dueDate;
                decimal lateFee = lateChargePerDay * Convert.ToDecimal(lateDuration.TotalDays);

                decimal totalDeduction = outstandingAmount + lateFee;
                bank.BankActBalance -= totalDeduction;

                ctx.Banks.Update(bank);

                ctx.SaveChanges();

                LateFee lateFeeObj = new LateFee
                {

                    Amount = lateFee,
                    CardNo = monthly.CardNo,
                    FeeType = "Latefee",
                    FeeDescription = "Latefee Penalty Applied."

                };
                ctx.LateFees.Add(lateFeeObj);
            }
            else
            {
                bank.BankActBalance -= Convert.ToDecimal(monthly.OutstandingAmt);
                ctx.Banks.Update(bank);
                ctx.SaveChanges();
            }
        }

    }

}
