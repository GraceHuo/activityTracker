using ActivityTracker.Controllers;
using ActivityTracker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace ActivityTracker.ViewModels
{
    public class ActivityFormViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }

        public string Location { get; set; }

        [Required]
        public byte Category { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }

        public string Heading { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<ActivitiesController, ActionResult>> update =
                    (c => c.Update(this));
                Expression<Func<ActivitiesController, ActionResult>> create =
                    (c => c.Create(this));

                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;
            }
        }
    }
}