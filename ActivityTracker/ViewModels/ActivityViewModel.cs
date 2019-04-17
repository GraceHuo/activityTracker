using ActivityTracker.Models;
using System.Collections.Generic;
using System.Linq;

namespace ActivityTracker.ViewModels
{
    public class ActivityViewModel
    {
        public IEnumerable<Activity> UpcommingActivities { get; set; }
        public bool ShowActions { get; set; }
        public string Heading { get; set; }
        public string SearchTerm { get; set; }
        public ILookup<int, Attendance> Attendances { get; set; }
    }
}