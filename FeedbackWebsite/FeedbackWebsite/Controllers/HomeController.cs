using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FeedbackWebsite.Models;
using Microsoft.AspNetCore.Authorization;

namespace FeedbackWebsite.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles = "admin, user")]
        public IActionResult Index()
        {
            if (User.IsInRole("admin"))
            {
                return RedirectToAction("AdminIndex");
            }

            return RedirectToAction("Index", "Feedback");
        }

        [Authorize(Roles = "admin")]
        public IActionResult AdminIndex()
        {
            return View();
        }

        //    public IActionResult Privacy()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
