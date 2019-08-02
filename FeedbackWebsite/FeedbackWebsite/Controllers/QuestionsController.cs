using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedbackWebsite.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FeedbackWebsite.Models;
using FeedbackWebsite.ViewModels;
using Microsoft.AspNetCore.Authorization;


namespace FeedbackWebsite.Controllers
{
    public class QuestionsController : Controller
    {

        private readonly FeedbackWebsiteContext _context;

        public QuestionsController(FeedbackWebsiteContext context)
        {
            _context = context;
        }

        ////// GET: Feedback/Details/5
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Details(int? eventId)
        {
            if (eventId == null)
            {
                return NotFound();
            }

            var questionsTextModels = await _context.QuestionTextModel.ToListAsync();

            var answersInfoModel = _context.AnswersInfoModel.Where(model => model.EventId == eventId).ToDictionary(model => model.QuestionId);

            if (answersInfoModel.Count == 0)
            {
                return NotFound();
            }

            List<QuestionsAnswersModel> questionsAnswers = new List<QuestionsAnswersModel>();

            foreach (var question in questionsTextModels)
            {
                var questionsAnswersModel = question.IsEnum
                    ? (QuestionsAnswersModel)CreateQuestionsAnswersEnum(question, answersInfoModel)
                    : CreateQuestionsAnswersText(question, answersInfoModel);

                questionsAnswers.Add(questionsAnswersModel);
            }

            IndexViewModel ivm = new IndexViewModel { EventId = eventId.Value, QuestionsAnswers = questionsAnswers };

            return View(ivm);
        }

        ////GET: Feedback/CreateOrEdit/5
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> AddOrEdit(int? eventId)
        {
            if (eventId == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Edit";

            var questionsTextModels = await _context.QuestionTextModel.ToListAsync();

            var answersInfoModel = _context.AnswersInfoModel.Where(model => model.EventId == eventId).ToDictionary(model => model.QuestionId);

            if (answersInfoModel.Count == 0)
            {
                ViewData["Title"] = "Add";
            }

            List<QuestionsAnswersModel> questionsAnswers = new List<QuestionsAnswersModel>();

            foreach (var question in questionsTextModels)
            {
                var questionsAnswersModel = question.IsEnum
                    ? (QuestionsAnswersModel)CreateQuestionsAnswersEnum(question, answersInfoModel)
                    : CreateQuestionsAnswersText(question, answersInfoModel);

                questionsAnswers.Add(questionsAnswersModel);
            }

            IndexViewModel ivm = new IndexViewModel { EventId = eventId.Value, QuestionsAnswers = questionsAnswers };

            return View(ivm);
        }

        bool CheckEventExist(int eventId)
        {
            return _context.EventInfoModel.Any(e => e.Id == eventId);
        }

        //// POST: Feedback/CreateOrEdit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> AddOrEdit(int? eventId, Dictionary<int, string> answers)
        {
            if (eventId == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid) return BadRequest();
            

            if (!CheckEventExist( eventId.Value))
            {
                return NotFound();
            }


            var questionsTextModels = await _context.QuestionTextModel.ToListAsync();

            var answerEnums = _context.AnswerEnum.Where(model => model.EventId == eventId).ToDictionary(model => model.QuestionId);
            var answerTexts = _context.AnswerText.Where(model => model.EventId == eventId).ToDictionary(model => model.QuestionId);

            foreach (QuestionTextModel question in questionsTextModels)
            {
                string answer = answers[question.Id];


                if (question.IsEnum)
                {
                    await AddOrUpdateEnumAnswer(eventId.Value, answerEnums, question, answer);
                }
                else
                {
                    await AddOrUpdateTextAnswer(eventId.Value, answerTexts, question, answer);
                }
            }

            return RedirectToAction("Index", "Feedback");
        }
        private static QuestionsAnswersEnum CreateQuestionsAnswersEnum(QuestionTextModel question, Dictionary<int, AnswersInfoModel> answersInfoModel)
        {
            var questionsAnswersEnum = new QuestionsAnswersEnum
            {
                QuestionId = question.Id,
                QuestionText = question.QuestionText,
                QuestionType = question.IsEnum
            };

            var questionId = question.Id;

            if (answersInfoModel.ContainsKey(questionId))
            {
                var answerEnum = (AnswerEnum)answersInfoModel[questionId];
                questionsAnswersEnum.EnumAnswer = answerEnum.QuestionEnumAnswer;
            }

            return questionsAnswersEnum;
        }


        private static QuestionsAnswersText CreateQuestionsAnswersText(QuestionTextModel question, Dictionary<int, AnswersInfoModel> answersInfoModel)
        {
            var questionsAnswersText = new QuestionsAnswersText
            {
                QuestionId = question.Id,
                QuestionText = question.QuestionText,
                QuestionType = question.IsEnum
            };

            var questionId = question.Id;

            if (answersInfoModel.ContainsKey(questionId))
            {
                var answerText = (AnswerText)answersInfoModel[questionId];
                questionsAnswersText.TextAnswer = answerText.AnswerTextAnswer;
            }

            return questionsAnswersText;
        }


        private async Task AddOrUpdateEnumAnswer(int eventId, Dictionary<int, AnswerEnum> answerEnums, QuestionTextModel question, string answer)
        {
            if (!answerEnums.ContainsKey(question.Id))
            {
                var answerEnum = new AnswerEnum
                {
                    QuestionId = question.Id,
                    EventId = eventId,
                    QuestionEnumAnswer = Enum.Parse<Answer>(answer)
                };

                _context.Add(answerEnum);
                await _context.SaveChangesAsync();
            }
            else
            {
                var answerEnum = answerEnums[question.Id];
                answerEnum.QuestionEnumAnswer = Enum.Parse<Answer>(answer);
                _context.Update(answerEnum);
                await _context.SaveChangesAsync();
            }
        }

        private async Task AddOrUpdateTextAnswer(int eventId, Dictionary<int, AnswerText> answerTexts, QuestionTextModel question, string answer)
        {
            if (!answerTexts.ContainsKey(question.Id))
            {
                var answerText = new AnswerText
                {
                    QuestionId = question.Id,
                    EventId = eventId,
                    AnswerTextAnswer = answer
                };

                _context.Add(answerText);
                await _context.SaveChangesAsync();
            }
            else
            {
                var answerText = answerTexts[question.Id];
                answerText.AnswerTextAnswer = answer;
                _context.Update(answerText);
                await _context.SaveChangesAsync();
            }
        }
    }
}