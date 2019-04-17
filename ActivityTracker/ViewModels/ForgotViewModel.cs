using System.ComponentModel.DataAnnotations;

namespace ActivityTracker.ViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
