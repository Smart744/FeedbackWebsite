using System.ComponentModel.DataAnnotations;

namespace FeedbackWebsite.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
