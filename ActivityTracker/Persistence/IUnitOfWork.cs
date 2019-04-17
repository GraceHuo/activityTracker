using ActivityTracker.Repositories;

namespace GigHub.Persistence
{
    public interface IUnitOfWork
    {
        IActivityRepository Activities { get; }
        IAttendanceRepository Attendances { get; }
        ICategoryRepository Categories { get; }
        IFollowingRepository Followings { get; }
        void Complete();
    }
}