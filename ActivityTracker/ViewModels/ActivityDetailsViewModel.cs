using ActivityTracker.Models;

namespace ActivityTracker.ViewModels
{
    public class ActivityDetailsViewModel
    {
        public Activity Activity { get; set; }
        public bool IsAttending { get; set; }
        public bool IsFollowing { get; set; }
    }
}