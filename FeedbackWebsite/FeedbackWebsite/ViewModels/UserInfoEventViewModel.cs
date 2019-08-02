using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FeedbackWebsite.Models;

namespace FeedbackWebsite.ViewModels
{
    public class UserInfoEventViewModel
    {
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [Display(Name = "Department")]
        public string Department { get; set; }

        [Display(Name = "Position")]
        public string Position { get; set; }

        public IEnumerable<EventInfoModel> Event { get; set; }
    }
}
