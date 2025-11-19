using CreditCardProj.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CreditCardProj.Controllers
{
    public class AttendanceController : Controller
    {
        public IActionResult Index()
        {
            // Load Username for header display
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            // Load last selected month/year from cookie (if any)
            var cookie = Request.Cookies["AttendanceFilter"];
            if (!string.IsNullOrEmpty(cookie))
            {
                var filter = JsonSerializer.Deserialize<AttendanceFilterCookie>(cookie);
                if (filter != null)
                {
                    ViewBag.Month = filter.Month;
                    ViewBag.Year = filter.Year;
                }
            }

            return View();  // returns Attendance.cshtml directly
        }

        [HttpPost]
        public IActionResult SaveFilter(int month, int year)
        {
            var filter = new AttendanceFilterCookie
            {
                Month = month,
                Year = year
            };

            Response.Cookies.Append(
                "AttendanceFilter",
                JsonSerializer.Serialize(filter),
                new CookieOptions { Expires = DateTime.Now.AddDays(30) }
            );

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SaveAttendance(List<Attendance> records)
        {
            if (records != null)
            {
                Response.Cookies.Append(
                    "AttendanceData",
                    JsonSerializer.Serialize(records),
                    new CookieOptions { Expires = DateTime.Now.AddDays(30) }
                );
            }

            return Json(new { success = true });
        }

        public IActionResult LoadAttendance(int month, int year)
        {
            var cookie = Request.Cookies["AttendanceData"];
            List<Attendance> all = new();

            if (!string.IsNullOrEmpty(cookie))
            {
                all = JsonSerializer.Deserialize<List<Attendance>>(cookie);
            }

            var filtered = all
                .Where(x => x.Date.Month == month && x.Date.Year == year)
                .ToList();

            return Json(filtered);
        }
    }

    public class AttendanceFilterCookie
    {
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
