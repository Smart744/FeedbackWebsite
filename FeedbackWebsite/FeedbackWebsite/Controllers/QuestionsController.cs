using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    if (!AnswersInfoModelExists(id.Value))
        //    {
        //        return NotFound();
        //    }


        //    return View(new List(new ));
        //}

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
                if (question.IsEnum)
                {
                    var questionsAnswersEnum = new QuestionsAnswersEnum
                    {
                        QuestionId = question.Id,
                        QuestionText = question.QuestionText,
                        QuestionType = question.IsEnum
                    };

                    var questionId = question.Id;

                    try
                    {
                        var answerEnum = (AnswerEnum)answersInfoModel[questionId];
                        questionsAnswersEnum.EnumAnswer = answerEnum.QuestionEnumAnswer;
                    }
                    catch (KeyNotFoundException)
                    {
                       // throw;
                    }

                    questionsAnswers.Add(questionsAnswersEnum);
                }
                else
                {
                    var questionsAnswersText = new QuestionsAnswersText
                    {
                        QuestionId = question.Id,
                        QuestionText = question.QuestionText,
                        QuestionType = question.IsEnum
                    };

                    var questionId = question.Id;

                    try
                    {
                        var answerText = (AnswerText)answersInfoModel[questionId];
                        questionsAnswersText.TextAnswer = answerText.AnswerTextAnswer;
                    }
                    catch (KeyNotFoundException)
                    {
                        //throw;
                    }

                    questionsAnswers.Add(questionsAnswersText);
                }
            }

            //if (answersInfoModel == null)
            //{
            //    //return NotFound();
            //    answersInfoModel = await _context.AnswersInfoModel
            //        .FirstOrDefaultAsync(m => m.Id == id);

            //    ViewData["Title"] = "Create";
            //}

            IndexViewModel ivm = new IndexViewModel {personId = personId.Value, qiestionsAnswers = questionsAnswers};

            return View(ivm);
        }

        // POST: Feedback/CreateOrEdit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int? personId, Dictionary<int, string> items)
        {

            if (personId != answersInfoModel.PersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (!AnswersInfoModelExists(answersInfoModel.Id))
                    {
                        _context.Add(answersInfoModel);
                        await _context.SaveChangesAsync();

                        // return RedirectToAction("Index", "Feedback");
                        // return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        _context.Update(answersInfoModel);
                        await _context.SaveChangesAsync();

                        // return RedirectToAction("Index", "Feedback");
                        //return RedirectToAction(nameof(Index));
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswersInfoModelExists(answersInfoModel.Id))
                    {
                        return NotFound();
                        //return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(answersInfoModel);
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
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int personId)
        //{
        //    var answersInfoModels =  _context.AnswersInfoModel.Where(model => model.PersonId == personId);
        //    foreach (var answersInfoModel in answersInfoModels)
        //    {
        //        _context.AnswersInfoModel.Remove(answersInfoModel);
        //    }
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool AnswersInfoModelExists(int personId)
        {
            return _context.AnswersInfoModel.Any(e => e.PersonId == personId);
        }
    }
}