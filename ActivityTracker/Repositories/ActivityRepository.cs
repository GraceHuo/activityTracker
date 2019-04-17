using ActivityTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ActivityTracker.Repositories
{

    public class ActivityRepository : IActivityRepository
    {
        private readonly ApplicationDbContext _context;

        public ActivityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Activity GetActivityWithAttendees(int activityId)
        {
            return _context.Activities
                .Include(a => a.Attendances.Select(at => at.Attendee))
                .SingleOrDefault(a => a.Id == activityId);
        }

        public IEnumerable<Activity> GetActivitiesUserAttending(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Activity)
                .Include(a => a.Teacher)
                .Include(a => a.Category)
                .ToList();
        }

        public Activity GetActivities(int id)
        {
            return _context.Activities
                .Include(a => a.Teacher)
                .Include(a => a.Category)
                .SingleOrDefault(a => a.Id == id);
        }

        public IEnumerable<Activity> GetUpcomingActivitiesByTeacher(string teacherId)
        {
            return _context.Activities
                .Where(a => a.TeacherId == teacherId &&
                            a.DateTime > DateTime.Now &&
                            !a.IsCanceled)
                .Include(a => a.Category)
                .ToList();
        }

        public void Add(Activity activity)
        {
            _context.Activities.Add(activity);
        }
    }
}