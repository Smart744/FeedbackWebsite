using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackWebsite.Models
{
    public class QuestionTextModel
    {
        public int Id { get; set; }

        public bool IsEnum { get; set; }

        public string QuestionText { get; set; }
    }
}
