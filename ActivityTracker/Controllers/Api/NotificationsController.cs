using ActivityTracker.DTOs;
using ActivityTracker.Models;
using AutoMapper;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace ActivityTracker.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }
        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Activity.Teacher)
                .ToList();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ApplicationUser, UserDto>();
                cfg.CreateMap<Activity, ActivityDto>();
                cfg.CreateMap<Notification, NotificationDto>();
            });

            // TODO: auto-mapping
            return notifications.Select(n => new NotificationDto()
            {
                DateTime = n.DateTime,
                Activity = new ActivityDto
                {
                    Teacher = new UserDto
                    {
                        Id = n.Activity.Teacher.Id,
                        Name = n.Activity.Teacher.Name
                    },
                    DateTime = n.DateTime,
                    Id = n.Activity.Id,
                    IsCanceled = n.Activity.IsCanceled,
                    Description = n.Activity.Description,
                    Location = n.Activity.Location
                },
                OriginalDateTime = n.OriginalDateTime,
                OriginalDescription = n.OriginalDescription,
                OriginalLocation = n.OriginalLocation,
                NotificationType = n.NotificationType
            });
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .ToList();

            notifications.ForEach(n => n.Read());

            _context.SaveChanges();

            return Ok();
        }
    }
}
