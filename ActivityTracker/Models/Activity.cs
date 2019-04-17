using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ActivityTracker.Models
{
    public class Activity
    {
        public int Id { get; set; }

        public bool IsCanceled { get; private set; }

        public ApplicationUser Teacher { get; set; }

        [Required]
        public string TeacherId { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [StringLength(255)]
        public string Location { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        public Category Category { get; set; }

        [Required]
        public byte CategoryId { get; set; }
        public ICollection<Attendance> Attendances { get; private set; }

        public Activity()
        {
            Attendances = new Collection<Attendance>();
        }

        public void Cancel()
        {
            IsCanceled = true;

            var notification = Notification.ActivityCanceled(this);

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }

        internal void Modify(DateTime dateTime, string description, byte category, string location)
        {
            var notification = Notification.ActivityUpdated(this, DateTime, Description, Location);

            DateTime = dateTime;
            Location = location;
            Description = description;
            CategoryId = category;


            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }
    }
}