using System;
using ActivityTracker.Models;

namespace ActivityTracker.DTOs
{
    public class NotificationDto
    {
        public DateTime DateTime { get; set; }
        public NotificationType NotificationType { get; set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalDescription { get; set; }
        public string OriginalLocation { get; set; }
        public ActivityDto Activity { get; set; }
    }
}