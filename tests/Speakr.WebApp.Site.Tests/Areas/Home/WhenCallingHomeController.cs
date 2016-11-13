using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Speakr.WebApp.Controllers;
using Speakr.WebApp.Site.ViewModels.Home;

namespace Speakr.WebApp.Site.Tests.Areas.Home
{
    public class WhenCallingHomeController
    {
        [Test]
        public void Index_Returns200AndTheCorrectView()
        {
            var controller = new HomeController();
            var actionResult = (ViewResult)controller.Index();

            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actionResult.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void TalkNotFound_Returns200AndModelWithErrorMessage()
        {
            var controller = new HomeController();
            var actionResult = (ViewResult)controller.TalkNotFound("12345");
            var modelResult = (GetFeedbackFormViewModel)actionResult.Model;

            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actionResult.ViewName, Is.EqualTo("Index"));
            Assert.That(modelResult.EasyAccessKey, Is.EqualTo("12345"));
            Assert.That(modelResult.EasyAccessKeyErrorMessage, Is.EqualTo("Talk not found"));
        }
    }
}
