using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FeedbackWebsite.Models;


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
        public async Task<IActionResult> Index()
        {
            return View(await _context.QuestionsInfoModel.ToListAsync());
        }

        // GET: Feedback/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionsInfoModel = await _context.QuestionsInfoModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questionsInfoModel == null)
            {
                return NotFound();
            }

            return View(questionsInfoModel);
        }

        // GET: Feedback/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Feedback/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Question1,Question2,Question3,Question4,Question5,Question6,Question7,Question8,Question9,Comment")] QuestionsInfoModel questionsInfoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(questionsInfoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(questionsInfoModel);
        }

        // GET: Feedback/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionsInfoModel = await _context.QuestionsInfoModel.FindAsync(id);
            if (questionsInfoModel == null)
            {
                return NotFound();
            }
            return View(questionsInfoModel);
        }

        // POST: Feedback/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Question1,Question2,Question3,Question4,Question5,Question6,Question7,Question8,Question9,Comment")] QuestionsInfoModel questionsInfoModel)
        {
            if (id != questionsInfoModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questionsInfoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionsInfoModelExists(questionsInfoModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(questionsInfoModel);
        }

        // GET: Feedback/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionsInfoModel = await _context.QuestionsInfoModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questionsInfoModel == null)
            {
                return NotFound();
            }

            return View(questionsInfoModel);
        }

        // POST: Feedback/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questionsInfoModel = await _context.QuestionsInfoModel.FindAsync(id);
            _context.QuestionsInfoModel.Remove(questionsInfoModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionsInfoModelExists(int id)
        {
            return _context.QuestionsInfoModel.Any(e => e.Id == id);
        }
    }
}