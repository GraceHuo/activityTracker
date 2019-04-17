using ActivityTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ActivityTracker.Repositories
{

    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Activity.DateTime > DateTime.Now)
                .ToList();
        }

        public Attendance GetAttendance(int activityId, string userId)
        {
            return _context.Attendances
                .SingleOrDefault(a => a.ActivityId == activityId && a.AttendeeId == userId);
        }
    }
}