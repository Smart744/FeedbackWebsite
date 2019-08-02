using FeedbackWebsite.Models;
using FeedbackWebsite.Utils;

namespace FeedbackWebsite.ViewModels
{
    public abstract class QuestionsAnswersModel
    {
        public int QuestionId { get; set; }
        
        public string QuestionText { get; set; }

        public bool QuestionType { get; set; }

        public abstract string GetAnswer();


    }

    public class QuestionsAnswersEnum : QuestionsAnswersModel
    {
        public Answer EnumAnswer { get; set; }

        public override string GetAnswer()
        {
            return EnumAnswer.DisplayName();
        }
    }

    public class QuestionsAnswersText : QuestionsAnswersModel
    {
        public string TextAnswer { get; set; }

        public override string GetAnswer()
        {
            return TextAnswer;
        }
    }
    
}
