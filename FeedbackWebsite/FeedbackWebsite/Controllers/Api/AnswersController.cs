using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedbackWebsite.Data;
using FeedbackWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FeedbackWebsite.Controllers.Api
{
    public class AnswersController : Controller
    {
        private readonly FeedbackWebsiteContext _context;

        public AnswersController(FeedbackWebsiteContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public async Task<IActionResult> GetAnswerwTask(int? personId)
        {
            var answersInfoModel = _context.AnswersInfoModel.Where(model => model.PersonId == personId);
            if (!answersInfoModel.Any())
            {
                return NotFound(new List<AnswersInfoModel>());
            }

            return Json(answersInfoModel);
        }
    }
}
