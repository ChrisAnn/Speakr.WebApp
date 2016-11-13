using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Speakr.WebApp.Controllers;
using Speakr.WebApp.Site.Tests.Helpers;
using Speakr.WebApp.Site.ViewModels.Home;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Speakr.WebApp.Site.Tests.Areas.Home
{
    public class WhenCallingCheckEasyAccessKey
    {
        [TestCase("12345")]
        [TestCase("aaaaa")]
        [TestCase("aaa_aaa")]
        [TestCase("aaa.aaa")]
        public void ThenItRedirectsForValidEasyAccessKeyIds(string easyAccessKey)
        {
            var model = new GetFeedbackFormViewModel() { EasyAccessKey = easyAccessKey };

            var controller = new HomeController();
            var actionResult = (RedirectToActionResult)controller.CheckEasyAccessKey(model);

            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actionResult.ActionName, Is.EqualTo("Index"));
            Assert.That(actionResult.ControllerName, Is.EqualTo("feedback"));
            Assert.That(actionResult.RouteValues["easyAccessKey"], Is.EqualTo(easyAccessKey));
        }

        [TestCase("", "Please enter your talk's ID")]
        [TestCase("123", "Talk ID's have at least 4 characters")]
        [TestCase("aaa", "Talk ID's have at least 4 characters")]
        [TestCase("!!!!!", "There are invalid characters on your TalkId")]
        public void OtherwiseItHandlesInvalidValues(string easyAccessKey, string expectedMessage)
        {
            var model = new GetFeedbackFormViewModel() { EasyAccessKey = easyAccessKey };
            var controller = new HomeController();

            var validationErrors = CheckForValidationErrors(controller, model);

            var actionResult = (ViewResult)controller.CheckEasyAccessKey(model);
            var modelState = controller.ModelState;

            Assert.That(validationErrors.Count, Is.EqualTo(1));
            Assert.That(validationErrors[0].ErrorMessage, Is.EqualTo(expectedMessage));

            Assert.That(actionResult.ViewName, Is.EqualTo("Index"));
        }

        private static IList<ValidationResult> CheckForValidationErrors(HomeController controller, GetFeedbackFormViewModel model)
        {
            var validationErrors = ViewModelValidation.Validate(model);

            if (validationErrors.Count > 0)
            {
                controller.ViewData.ModelState.AddModelError(nameof(model.EasyAccessKey), validationErrors[0].ErrorMessage);
            }

            return validationErrors;
        }
    }
}
