using CreditCardProj.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CreditCardProj.Controllers
{
    public class _2StepVerifyController : Controller
    {
        CreditCardContext ctx = new CreditCardContext();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult verify()
        {
            return View();
        }

       
        public IActionResult BlockCard(User obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = ctx.Users.FirstOrDefault(u => u.UserName == obj.UserName);

            if (user == null || user.Password != obj.Password)
            {
                return BadRequest(new { message = "Invalid username or password." });
            }

          //  CreditCard card = ctx.CreditCards.FirstOrDefault(c => c.CardNo == obj.CardNo);

            //if (card == null)
            //{
            //    return NotFound(new { message = "Card not found." });
            //}

            //card.IsBlocked = "true";
            //ctx.SaveChanges();

            return Ok(new { message = "Card blocked successfully." });
        }


        //https://localhost:7039/_2StepVerify/UnBlockCard?CardNo=1&UserName=Tejas&Password=1234
        public IActionResult UnblockCard(User obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Retrieve user from database by username
            User user = ctx.Users.FirstOrDefault(u => u.UserName == obj.UserName);

            if (user == null || user.Password != obj.Password)
            {
                return BadRequest(new { message = "Invalid username or password." });
            }

            // Simulate unblocking card (update database)
            //CreditCard card = ctx.CreditCards.FirstOrDefault(c => c.CardNo == obj.CardNo);

            //if (card == null)
            //{
            //    return NotFound(new { message = "Card not found." });
            //}

            //card.IsBlocked = "false";
            //ctx.SaveChanges();

            return Ok(new { message = "Card unblocked successfully." });
        }

        public IActionResult EnterDetails()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BlockCard1(User obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = ctx.Users.FirstOrDefault(u => u.UserName == obj.UserName);

            if (user == null || user.Password != obj.Password)
            {
                ViewBag.ErrorMessage = "Invalid username or password.";
            }

            //CreditCard card = ctx.CreditCards.FirstOrDefault(c => c.CardNo == obj.CardNo);

            //if (card == null)
            //{
            //    ViewBag.ErrorMessage = "Card not found.";
            //}

            //card.IsBlocked = "true";
            //ctx.SaveChanges();

            ViewBag.SuccessMessage = "Card Blocked successfully.";
            return View("Success"); 
        }


        public IActionResult UnblockCard1(User obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = ctx.Users.FirstOrDefault(u => u.UserName == obj.UserName);

            if (user == null || user.Password != obj.Password)
            {
                ViewBag.ErrorMessage = "Invalid username or password.";
            }

            //CreditCard card = ctx.CreditCards.FirstOrDefault(c => c.CardNo == obj.CardNo);

            //if (card == null)
            //{
            //    ViewBag.ErrorMessage = "Card not found.";
            //}

            //card.IsBlocked = "false";
            //ctx.SaveChanges();

            ViewBag.SuccessMessage = "Card UNBlocked successfully.";
            return View("Success"); 
        }

        public IActionResult BlockCard2()
        {
            var model = new UserViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult BlockCard2(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            User user = ctx.Users.FirstOrDefault(u => u.UserName == model.UserName && u.Password == model.Password);

            if (user == null)
            {
                ViewBag.ErrorMessage = "Invalid username or password.";
                return View(model);
            }

            CreditCard card = ctx.CreditCards.FirstOrDefault(c => c.CardNo == model.CardNo);

            if (card == null)
            {
                ViewBag.ErrorMessage = "Card not found.";
                return View(model);
            }

            card.IsBlocked = "true"; 
            ctx.SaveChanges();

            ViewBag.SuccessMessage = "Card Blocked successfully.";
            return View("Success");
        }

        public IActionResult UnblockCard2()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UnblockCard2(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); 
            }

            User user = ctx.Users.FirstOrDefault(u => u.UserName == model.UserName && u.Password == model.Password);

            if (user == null)
            {
                ViewBag.ErrorMessage = "Invalid username or password.";
                return View(model);
            }

            CreditCard card = ctx.CreditCards.FirstOrDefault(c => c.CardNo == model.CardNo);

            if (card == null)
            {
                ViewBag.ErrorMessage = "Card not found.";
                return View(model);
            }

            card.IsBlocked = "false";
            ctx.SaveChanges();

            ViewBag.SuccessMessage = "Card Unblocked successfully.";
            return View("Success");
        }

    }
}
