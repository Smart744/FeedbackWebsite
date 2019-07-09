using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedbackWebsite.Models;

namespace FeedbackWebsite.ViewModels
{
    public class QuestionsAnswersModel
    {
        public int QuestionId { get; set; }
        
        public string QuestionText { get; set; }

        public bool QuestionType { get; set; }
    }

    public class QuestionsAnswersEnum : QuestionsAnswersModel
    {
        public Answer EnumAnswer { get; set; }
    }

    public class QuestionsAnswersText : QuestionsAnswersModel
    {
        public string TextAnswer { get; set; }
    }
}
