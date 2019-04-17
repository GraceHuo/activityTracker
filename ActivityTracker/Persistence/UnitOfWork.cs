using ActivityTracker.Models;
using ActivityTracker.Repositories;

namespace GigHub.Persistence
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IActivityRepository Activities { get; private set; }
        public IAttendanceRepository Attendances { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IFollowingRepository Followings { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Activities = new ActivityRepository(context);
            Attendances = new AttendanceRepository(context);
            Categories = new CategoryRepository(context);
            Followings = new FollowingRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}