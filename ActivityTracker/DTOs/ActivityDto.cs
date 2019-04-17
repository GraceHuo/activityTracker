using System;
using ActivityTracker.Controllers.Api;

namespace ActivityTracker.DTOs
{
    public class ActivityDto
    {
        public int Id { get; set; }

        public bool IsCanceled { get; set; }

        public UserDto Teacher { get; set; }

        public DateTime DateTime { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public CategoryDto Category { get; set; }
    }
}