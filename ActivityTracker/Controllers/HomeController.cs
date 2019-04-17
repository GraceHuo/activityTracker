using ActivityTracker.Models;
using ActivityTracker.Repositories;
using ActivityTracker.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace ActivityTracker.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        private readonly AttendanceRepository _unitOfWork.Attendances;

        public HomeController()
        {
            _context = new ApplicationDbContext();
            _unitOfWork.Attendances = new AttendanceRepository(_context);
        }
        public ActionResult Index(string query = null)
        {
            var upcommingActivities = _context.Activities
                .Include(a => a.Teacher)
                .Include(a => a.Category)
                .Where(a => a.DateTime > DateTime.Now && !a.IsCanceled);

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcommingActivities = upcommingActivities
                    .Where(a =>
                        a.Teacher.Name.Contains(query) ||
                        a.Category.Name.Contains(query) ||
                        a.Description.Contains(query) ||
                        a.Location.Contains(query));
            }

            var userId = User.Identity.GetUserId();
            var attendances = _unitOfWork.Attendances.GetFutureAttendances(userId)
                .ToLookup(a => a.ActivityId);

            var viewModel = new ActivityViewModel
            {
                UpcommingActivities = upcommingActivities,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "All Activities",
                SearchTerm = query,
                Attendances = attendances
            };
            return View("Activities", viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}