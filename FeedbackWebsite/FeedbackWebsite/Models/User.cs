using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FeedbackWebsite.Models
{
    public class User : IdentityUser
    {
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [Display(Name = "Department")]
        public string Department { get; set; }

        [Display(Name = "Position")]
        public string Position { get; set; }
    }
}
