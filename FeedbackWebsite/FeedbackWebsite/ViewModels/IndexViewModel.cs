using System.Collections.Generic;

namespace FeedbackWebsite.ViewModels
{
    public class IndexViewModel
    {
        public int EventId { get; set; }
        public IEnumerable<QuestionsAnswersModel> QuestionsAnswers { get; set; }
    }
}
