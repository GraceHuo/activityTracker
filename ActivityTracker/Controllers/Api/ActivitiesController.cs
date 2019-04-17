using ActivityTracker.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace ActivityTracker.Controllers.Api
{
    [Authorize]
    public class ActivitiesController : ApiController
    {
        private ApplicationDbContext _context;

        public ActivitiesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var activity = _context.Activities
                .Include(a => a.Attendances.Select(at => at.Attendee))
                .Single(a => a.Id == id && a.TeacherId == userId);

            if (activity.IsCanceled)
            {
                return NotFound();
            }

            activity.Cancel();

            _context.SaveChanges();
            return Ok();
        }
    }
}
