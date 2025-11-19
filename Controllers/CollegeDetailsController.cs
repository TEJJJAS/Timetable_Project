using Microsoft.AspNetCore.Mvc;
using CreditCardProj.Models;

namespace CreditCardProj.Controllers
{
    public class CollegeDetailsController : Controller
    {
        CreditCardContext ctx = new CreditCardContext();

        // GET: CollegeDetails/CollegeDetails
        public IActionResult CollegeDetails()
        {
            // Example: fetch division info (replace with actual DB data)
            var divisions = new List<Division>
            {
                new Division { DivisionId = 1, DivisionName = "Division A" },
                new Division { DivisionId = 2, DivisionName = "Division B" },
                new Division { DivisionId = 3, DivisionName = "Division C" }
            };

            return View(divisions);
        }

        [HttpPost]
        public IActionResult GenerateTimetable(string divisionName)
        {
            // Example timetable (replace with DB logic)
            var timetable = new List<Timetable>
            {
                new Timetable { DivisionName = divisionName, Day = "Monday", Subject = "Math", Time = "10:00-11:00" },
                new Timetable { DivisionName = divisionName, Day = "Monday", Subject = "Physics", Time = "11:00-12:00" },
                new Timetable { DivisionName = divisionName, Day = "Tuesday", Subject = "Chemistry", Time = "10:00-11:00" }
            };

            ViewBag.Division = divisionName;
            return View("Timetable", timetable);
        }

        [HttpGet]
        public IActionResult Timetable(string batch)
        {
            // You can later fetch timetable from DB for 'batch'
            ViewBag.Batch = batch;
            return PartialView("_TimetablePartial");
        }
    }
}
