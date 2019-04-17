using System;
using System.ComponentModel.DataAnnotations;

namespace ActivityTracker.Models
{
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType NotificationType { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalDescription { get; private set; }
        public string OriginalLocation { get; private set; }

        [Required]
        public Activity Activity { get; private set; }

        protected Notification()
        {

        }

        private Notification(Activity activity, NotificationType notificationType)
        {
            Activity = activity ?? throw new ArgumentNullException(nameof(activity));
            NotificationType = notificationType;
            DateTime = DateTime.Now;
        }

        public static Notification ActivityCreated(Activity activity)
        {
            return new Notification(activity, NotificationType.ActivityCreated);
        }

        public static Notification ActivityUpdated(Activity newActivity, DateTime originalDateTime, string originalDescription, string originalLocation)
        {
            var notification = new Notification(newActivity, NotificationType.ActivityUpdated);

            notification.OriginalDateTime = originalDateTime;
            notification.OriginalDescription = originalDescription;
            notification.OriginalLocation = originalLocation;

            return notification;
        }

        public static Notification ActivityCanceled(Activity activity)
        {
            return new Notification(activity, NotificationType.ActivityCanceled);
        }


    }
}