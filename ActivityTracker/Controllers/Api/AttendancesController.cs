using ActivityTracker.DTOs;
using ActivityTracker.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace ActivityTracker.Controllers.Api
{

    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {

            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDTO dto)
        {
            var userId = User.Identity.GetUserId();
            var exists = _context.Attendances.Any(a =>
                a.AttendeeId == userId && a.ActivityId == dto.ActivityId);

            if (exists)
            {
                return BadRequest("The attendence already exists");
            }
            var attendance = new Attendance
            {
                ActivityId = dto.ActivityId,
                AttendeeId = userId
                // For debug: AttendeeId = "2c2af16a-9161-4a06-9253-5fb3f08e253f"
            };
            _context.Attendances.Add(attendance);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteAttendence(int id)
        {
            var userId = User.Identity.GetUserId();
            var attendence = _context.Attendances
                .Single(a => a.AttendeeId == userId && a.ActivityId == id);

            if (attendence == null)
            {
                return NotFound();
            }

            _context.Attendances.Remove(attendence);

            _context.SaveChanges();
            return Ok(id);
        }
    }
}
