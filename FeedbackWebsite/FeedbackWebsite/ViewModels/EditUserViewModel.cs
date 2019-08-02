using System.ComponentModel.DataAnnotations;

namespace FeedbackWebsite.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required, RegularExpression(@"[A-Za-z ]*")]
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [Required]
        [Display(Name = "Department")]
        public string Department { get; set; }

        [Required]
        [Display(Name = "Position")]
        public string Position { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
