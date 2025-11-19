using CreditCardProj.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace CreditCardProj.Controllers
{
    public class UserRegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registration()
        {
            var viewModel = new RegistrationViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(RegistrationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // 1️⃣ Read existing users from cookie
                    var cookie = Request.Cookies["UserLogin"];
                    List<UserCookieInfo> usersList = new List<UserCookieInfo>();

                    if (!string.IsNullOrEmpty(cookie))
                    {
                        var cookieValue = Uri.UnescapeDataString(cookie); // decode

                        try
                        {
                            using (JsonDocument doc = JsonDocument.Parse(cookieValue))
                            {
                                if (doc.RootElement.ValueKind == JsonValueKind.Array)
                                {
                                    foreach (var item in doc.RootElement.EnumerateArray())
                                    {
                                        usersList.Add(new UserCookieInfo
                                        {
                                            UserName = item.GetProperty("UserName").GetString(),
                                            Password = item.GetProperty("Password").GetString()
                                        });
                                    }
                                }
                                else if (doc.RootElement.ValueKind == JsonValueKind.Object)
                                {
                                    usersList.Add(new UserCookieInfo
                                    {
                                        UserName = doc.RootElement.GetProperty("UserName").GetString(),
                                        Password = doc.RootElement.GetProperty("Password").GetString()
                                    });
                                }
                            }
                        }
                        catch
                        {
                            // ignore errors
                        }
                    }

                    // 2️⃣ Add or update user
                    var existingUser = usersList.FirstOrDefault(u => u.UserName == viewModel.User.UserName);
                    if (existingUser != null)
                    {
                        existingUser.Password = viewModel.User.Password; // update password
                    }
                    else
                    {
                        usersList.Add(new UserCookieInfo
                        {
                            UserName = viewModel.User.UserName,
                            Password = viewModel.User.Password
                        });
                    }

                    // 3️⃣ Save updated list to cookie
                    var usersJson = JsonSerializer.Serialize(usersList);
                    CookieOptions options = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(14),
                        HttpOnly = true,
                        Secure = false // set true for HTTPS
                    };
                    Response.Cookies.Append("UserLogin", usersJson, options);

                    // 4️⃣ Store current user in session
                    HttpContext.Session.SetString("UserName", viewModel.User.UserName);

                    return RedirectToAction("Login", "Login");
                }
                catch (Exception)
                {
                    ViewBag.ErrorMessage = "An error occurred during registration.";
                    return View(viewModel);
                }
            }
            return View(viewModel);
        }
    }
}
