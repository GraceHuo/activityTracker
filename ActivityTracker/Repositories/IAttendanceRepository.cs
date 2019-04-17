using ActivityTracker.Models;
using System.Collections.Generic;

namespace ActivityTracker.Repositories
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetFutureAttendances(string userId);
        Attendance GetAttendance(int activityId, string userId);
    }
}