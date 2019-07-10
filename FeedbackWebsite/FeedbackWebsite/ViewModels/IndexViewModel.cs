using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedbackWebsite.Models;

namespace FeedbackWebsite.ViewModels
{
    public class IndexViewModel
    {
        //person id
        public int PersonId { get; set; }
        //list
        //   qid  q t a 

        public IEnumerable<QuestionsAnswersModel> QuestionsAnswers { get; set; }
        //public IEnumerable<QuestionTextModel> QuestionsText { get; set; }
    }
}
