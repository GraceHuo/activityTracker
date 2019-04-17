using ActivityTracker.Models;

namespace ActivityTracker.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string followerId, string followeeId);
    }
}