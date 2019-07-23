using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedbackWebsite.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FeedbackWebsite.Models;
using Microsoft.AspNetCore.Authorization;

namespace FeedbackWebsite.Controllers
{
    public class FeedbackController : Controller
    {
        //private readonly FeedbackWebsiteContext _context;

        //public FeedbackController(FeedbackWebsiteContext context)
        //{
        //    _context = context;
        //}

        //// GET: Feedback
        ////[Authorize(Roles = "admin")]
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.EventInfoModel.ToListAsync());
        //}

        //// GET: Feedback/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var eventInfoModel = await _context.EventInfoModel
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (eventInfoModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(eventInfoModel);
        //}

        //// GET: Feedback/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Feedback/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,EmployeeName,Department,Position,EventName,EventOrg,PresentersName,EventLocation,EventStartDate,EventEndDate")] EventInfoModel eventInfoModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(eventInfoModel);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("AddOrEdit", "Questions", new { personId = eventInfoModel.Id});
        //    }
        //    return View(eventInfoModel);
        //}

        //// GET: Feedback/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var eventInfoModel = await _context.EventInfoModel.FindAsync(id);
        //    if (eventInfoModel == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(eventInfoModel);
        //}

        //// POST: Feedback/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeName,Department,Position,EventName,EventOrg,PresentersName,EventLocation,EventStartDate,EventEndDate")] EventInfoModel eventInfoModel)
        //{
        //    if (id != eventInfoModel.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(eventInfoModel);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!EventInfoModelExists(eventInfoModel.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction("AddOrEdit", "Questions", new {personId = eventInfoModel.Id });
        //    }
        //    return View(eventInfoModel);
        //}

        //// GET: Feedback/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var eventInfoModel = await _context.EventInfoModel
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (eventInfoModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(eventInfoModel);
        //}

        //// POST: Feedback/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var eventInfoModel = await _context.EventInfoModel.FindAsync(id);
        //    _context.EventInfoModel.Remove(eventInfoModel);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Delete", "Questions", new { personId = eventInfoModel.Id });
        //}

        //private bool EventInfoModelExists(int id)
        //{
        //    return _context.EventInfoModel.Any(e => e.Id == id);
        //}
    }
}
