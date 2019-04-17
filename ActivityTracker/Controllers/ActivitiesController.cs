using ActivityTracker.Models;
using ActivityTracker.ViewModels;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace ActivityTracker.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActivitiesController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }

        public ActionResult Details(int id)
        {
            var activity = _unitOfWork.Activities.GetActivities(id);

            if (activity == null)
                return HttpNotFound();

            var viewModel = new ActivityDetailsViewModel { Activity = activity };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                viewModel.IsAttending =
                    _unitOfWork.Attendances.GetAttendance(activity.Id, userId) != null;

                viewModel.IsFollowing =
                    _unitOfWork.Followings.GetFollowing(userId, activity.TeacherId) != null;
            }

            return View("Details", viewModel);
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var activities = _unitOfWork.Activities.GetUpcomingActivitiesByTeacher(userId);
            return View(activities);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var viewModel = new ActivityViewModel
            {
                UpcommingActivities = _unitOfWork.Activities.GetActivitiesUserAttending(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Activities I'm Attending",
                Attendances = _unitOfWork.Attendances.GetFutureAttendances(userId).ToLookup(a => a.ActivityId)
            };

            return View("Activities", viewModel);
        }

        [HttpPost]
        public ActionResult Search(ActivityViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new ActivityFormViewModel
            {
                Categories = _unitOfWork.Categories.GetCategories(),
                Heading = "Add an Activity"
            };
            return View("ActivityForm", viewModel);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var activity = _unitOfWork.Activities.GetActivities(id);

            if (activity == null)
            {
                return HttpNotFound();
            }

            if (activity.TeacherId != User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }

            var viewModel = new ActivityFormViewModel
            {
                Id = activity.Id,
                Heading = "Edit an Activity",
                Categories = _unitOfWork.Categories.GetCategories(),
                Date = activity.DateTime.ToString("d MMM yyyy"),
                Time = activity.DateTime.ToString("HH:mm"),
                Category = activity.CategoryId,
                Location = activity.Location,
                Description = activity.Description
            };
            return View("ActivityForm", viewModel);
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ActivityFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _unitOfWork.Categories.GetCategories();
                return View("ActivityForm", viewModel);
            }

            var activity = new Activity
            {
                TeacherId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                CategoryId = viewModel.Category,
                Description = viewModel.Description,
                Location = viewModel.Location
            };

            _unitOfWork.Activities.Add(activity);
            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Activities");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ActivityFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _unitOfWork.Categories.GetCategories();
                return View("ActivityForm", viewModel);
            }

            var activity = _unitOfWork.Activities.GetActivityWithAttendees(viewModel.Id);

            if (activity == null)
            {
                return HttpNotFound();
            }

            if (activity.TeacherId != User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }

            activity.Modify(viewModel.GetDateTime(), viewModel.Description, viewModel.Category, viewModel.Location);

            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Activities");
        }
    }
}