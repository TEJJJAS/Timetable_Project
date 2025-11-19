using Microsoft.AspNetCore.Mvc;

namespace CreditCardProj.Controllers
{
    public class LoginHomeController : Controller
    {
        public IActionResult LoginHome()
        {
            var username = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login", "Login");

            ViewBag.UserName = username;

            // Example batches
            ViewBag.Batches = new List<string> { "BPharm-A", "BPharm-B", "MPharm-A", "Diploma-A" };

            // Example courses summary
            ViewBag.Courses = new List<dynamic>
    {
        new { Name="B.Pharm", NumBatches=2, NumStudents=120, NumSubjects=10, LabCount=5 },
        new { Name="M.Pharm", NumBatches=1, NumStudents=40, NumSubjects=8, LabCount=3 },
        new { Name="Diploma", NumBatches=1, NumStudents=30, NumSubjects=6, LabCount=2 }
    };

            return View();
        }



        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
