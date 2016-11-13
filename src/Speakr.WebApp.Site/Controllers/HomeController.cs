using Microsoft.AspNetCore.Mvc;
using Speakr.WebApp.Site.ViewModels.Home;

namespace Speakr.WebApp.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View("Index");
        }

        [Route("TalkNotFound")]
        public IActionResult TalkNotFound(string easyAccessKey)
        {
            var model = new GetFeedbackFormViewModel()
            {
                EasyAccessKey = easyAccessKey,
                EasyAccessKeyErrorMessage = "Talk not found"
            };

            return View("Index", model);
        }

        [HttpPost]
        public IActionResult CheckEasyAccessKey(GetFeedbackFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "feedback", new { EasyAccessKey = model.EasyAccessKey });
            }

            return View("Index", model);
        }
    }
}