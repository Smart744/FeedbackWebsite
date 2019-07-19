using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FeedbackWebsite.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FeedbackWebsite.Models;
using FeedbackWebsite.ViewModels;


namespace FeedbackWebsite.Controllers
{
    public class QuestionsController : Controller
    {

        private readonly FeedbackWebsiteContext _context;

        public QuestionsController(FeedbackWebsiteContext context)
        {
            _context = context;
        }

        // GET: Feedback
        //public async Task<IActionResult> Index()
        //{
        //    return View(new List(new ));
        //}

        //// GET: Feedback/Details/5
        public async Task<IActionResult> Details(int? personId)
        {
            if (personId == null)
            {
                return NotFound();
            }

            var questionsTextModels = await _context.QuestionTextModel.ToListAsync();

            var answersInfoModel = _context.AnswersInfoModel.Where(model => model.PersonId == personId).ToDictionary(model => model.QuestionId);

            if (answersInfoModel.Count == 0)
            {
                return NotFound();
            }

            List<QuestionsAnswersModel> questionsAnswers = new List<QuestionsAnswersModel>();

            foreach (var question in questionsTextModels)
            {
                var questionsAnswersModel = question.IsEnum
                    ? (QuestionsAnswersModel) CreateQuestionsAnswersEnum(question, answersInfoModel)
                    : CreateQuestionsAnswersText(question, answersInfoModel);

                questionsAnswers.Add(questionsAnswersModel);
            }

            IndexViewModel ivm = new IndexViewModel { PersonId = personId.Value, QuestionsAnswers = questionsAnswers };

            return View(ivm);
        }

        //GET: Feedback/CreateOrEdit/5
        public async Task<IActionResult> AddOrEdit(int? personId)
        {
            if (personId == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Edit";

            var questionsTextModels = await _context.QuestionTextModel.ToListAsync();

            var answersInfoModel = _context.AnswersInfoModel.Where(model => model.PersonId == personId).ToDictionary(model => model.QuestionId);

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

            IndexViewModel ivm = new IndexViewModel {PersonId = personId.Value, QuestionsAnswers = questionsAnswers};

            return View(ivm);
        }


        // POST: Feedback/CreateOrEdit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int? personId, Dictionary<int, string> answers)
        {
            if (personId == null)
            {
                return NotFound();
            }

            if (_context.EventInfoModel.Any(e => e.Id == personId) == false)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return BadRequest();

            var questionsTextModels = await _context.QuestionTextModel.ToListAsync();

            var answerEnums = _context.AnswerEnum.Where(model => model.PersonId == personId).ToDictionary(model => model.QuestionId);
            var answerTexts = _context.AnswerText.Where(model => model.PersonId == personId).ToDictionary(model => model.QuestionId);

            foreach (QuestionTextModel question in questionsTextModels)
            {
                string answer = answers[question.Id];


                if (question.IsEnum)
                {
                    await AddOrUpdateEnumAnswer(personId.Value, answerEnums, question, answer);
                }
                else
                {
                    await AddOrUpdateTextAnswer(personId.Value, answerTexts, question, answer);
                }
            }

            return RedirectToAction("Index", "Feedback");
        }


        // GET: Feedback/Delete/5
        public async Task<IActionResult> Delete(int? personId)
        {
            if (personId == null)
            {
                return NotFound();
            }

            return View();
        }

        //// POST: Feedback/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int personId)
        {
            var answersInfoModels = _context.AnswersInfoModel.Where(model => model.PersonId == personId);

            foreach (var answerInfoModel in answersInfoModels)
            {
                _context.AnswersInfoModel.Remove(answerInfoModel);
            }

            await _context.SaveChangesAsync();
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


        private async Task AddOrUpdateEnumAnswer(int personId, Dictionary<int, AnswerEnum> answerEnums, QuestionTextModel question, string answer)
        {
            try
            {
                if (!answerEnums.ContainsKey(question.Id))
                {
                    var answerEnum = new AnswerEnum
                    {
                        QuestionId = question.Id,
                        PersonId = personId,
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
            catch (DbUpdateConcurrencyException)
            {
                //if (!AnswersInfoModelExists(answerEnum.Id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                throw;
                //}
            }
        }

        private async Task AddOrUpdateTextAnswer(int personId, Dictionary<int, AnswerText> answerTexts, QuestionTextModel question, string answer)
        {
            try
            {
                if (!answerTexts.ContainsKey(question.Id))
                {
                    var answerText = new AnswerText
                    {
                        QuestionId = question.Id,
                        PersonId = personId,
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
            catch (DbUpdateConcurrencyException)
            {
                //if (!AnswersInfoModelExists(answerText.Id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                throw;
                //}
            }
        }
    }
}