using ActivityTracker.Models;
using System.Collections.Generic;

namespace ActivityTracker.Repositories
{
    public interface IActivityRepository
    {
        Activity GetActivityWithAttendees(int activityId);
        IEnumerable<Activity> GetActivitiesUserAttending(string userId);
        Activity GetActivities(int id);
        IEnumerable<Activity> GetUpcomingActivitiesByTeacher(string teacherId);
        void Add(Activity activity);
    }
}