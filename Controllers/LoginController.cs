using CreditCardProj.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace CreditCardProj.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            // Pre-fill username from cookie if exists
            var cookie = Request.Cookies["UserLogin"];
            if (!string.IsNullOrEmpty(cookie))
            {
                var usersList = JsonSerializer.Deserialize<List<UserCookieInfo>>(cookie);
                if (usersList != null && usersList.Count > 0)
                {
                    ViewBag.UserName = usersList.Last().UserName; // pre-fill last registered user
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            if (!string.IsNullOrEmpty(user.UserName) && !string.IsNullOrEmpty(user.Password))
            {
                var cookie = Request.Cookies["UserLogin"];
                if (!string.IsNullOrEmpty(cookie))
                {
                    var usersList = JsonSerializer.Deserialize<List<UserCookieInfo>>(cookie);

                    var matchedUser = usersList?.FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);

                    if (matchedUser != null)
                    {
                        // Store username in session
                        HttpContext.Session.SetString("UserName", matchedUser.UserName);
                        return RedirectToAction("LoginHome", "LoginHome");
                    }
                }

                ViewBag.Message = "Invalid User Name or Password";
                return View();
            }
            return View();
        }


        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Attendance()
        {
            var loggedUser = HttpContext.Session.GetString("UserName");

            if (string.IsNullOrEmpty(loggedUser))
            {
                return RedirectToAction("Login", "Login");
            }

            return View();
        }
    }
}
