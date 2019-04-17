using ActivityTracker.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace ActivityTracker.Controllers
{
    public class FolloweesController : Controller
    {
        private ApplicationDbContext _context;

        public FolloweesController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var teachers = _context.Followings
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Followee)
                .ToList();

            return View(teachers);
        }
    }
}