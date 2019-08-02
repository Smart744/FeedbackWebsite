using System;
using System.ComponentModel.DataAnnotations;

namespace FeedbackWebsite.ViewModels
{
    public class UserEventDetailsViewModel
    {
        public int EventId { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [Display(Name = "Department")]
        public string Department { get; set; }

        [Display(Name = "Position")]
        public string Position { get; set; }

        [Display(Name = "Event Name")]
        public string EventName { get; set; }

        [Display(Name = "Event Organizer")]
        public string EventOrg { get; set; }

        [Display(Name = "Presenters Name")]
        public string PresentersName { get; set; }

        [Display(Name = "Event Location")]
        public string EventLocation { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Event Start Date")]
        public DateTime EventStartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Event End Date")]
        public DateTime EventEndDate { get; set; }
    }
}
