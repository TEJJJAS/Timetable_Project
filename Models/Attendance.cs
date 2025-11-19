namespace CreditCardProj.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } // Present / Absent / Leave
        public TimeSpan? InTime { get; set; }
        public TimeSpan? OutTime { get; set; }
        public string Remarks { get; set; }


        public string DayName => Date.DayOfWeek.ToString();
        public double? TotalHours => (InTime.HasValue && OutTime.HasValue) ? (OutTime - InTime)?.TotalHours : (double?)null;
    }
}
