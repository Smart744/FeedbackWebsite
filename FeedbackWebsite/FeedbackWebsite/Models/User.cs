using Microsoft.AspNetCore.Identity;

namespace FeedbackWebsite.Models
{
    public class User : IdentityUser
    {
        public string EmployeeName { get; set; }

        public string Department { get; set; }

        public string Position { get; set; }
    }
}
