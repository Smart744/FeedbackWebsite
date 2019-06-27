using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FeedbackWebsite.Models
{
    public class QuestionsInfoModel
    {
        public int Id { get; set; }

        [DisplayName("Question 1")]
        public string Question1 { get; set; }

        [DisplayName("Question 2")]
        public string Question2 { get; set; }

        [DisplayName("Question 3")]
        public string Question3 { get; set; }

        [DisplayName("Question 4")]
        public string Question4 { get; set; }

        [DisplayName("Question 5")]
        public string Question5 { get; set; }

        [DisplayName("Question 6")]
        public string Question6 { get; set; }

        [DisplayName("Question 7")]
        public string Question7 { get; set; }

        [DisplayName("Question 8")]
        public string Question8 { get; set; }

        [DisplayName("Question 9")]
        public string Question9 { get; set; }

        [DisplayName("Comment")]
        public string Comment { get; set; }
    }
}
