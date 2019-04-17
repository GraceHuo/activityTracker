using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivityTracker.Models
{
    public class Attendance
    {
        public Activity Activity { get; set; }

        public ApplicationUser Attendee { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ActivityId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string AttendeeId { get; set; }
    }
}