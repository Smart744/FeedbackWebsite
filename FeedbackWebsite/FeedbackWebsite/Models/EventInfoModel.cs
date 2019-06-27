using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace FeedbackWebsite.Models
{
    public class EventInfoModel
    {
        public int Id { get; set; }

        [DisplayName("Employee Name")]
        public string EmployeeName { get; set; }

        [DisplayName("Department")]
        public string Department { get; set; }

        [DisplayName("Position")]
        public string Position { get; set; }

        [DisplayName("Event Name")]
        public string EventName { get; set; }

        [DisplayName("Event Organizer")]
        public string EventOrg { get; set; }

        [DisplayName("Presenters Name")]
        public string PresentersName { get; set; }

        [DisplayName("Event Location")]
        public string EventLocation { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Event Start Date")]
        public DateTime EventStartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Event End Date")]
        public DateTime EventEndDate { get; set; }

    }
}
