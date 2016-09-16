﻿using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Speakr.WebApp.Site.Clients.TalksApi;
using Speakr.WebApp.Site.Clients.TalksApi.DTO;
using Speakr.WebApp.Site.Controllers;
using Speakr.WebApp.Site.Services.ReviewForm;
using Speakr.WebApp.Site.Tests.Helpers;
using Speakr.WebApp.Site.ViewModels.Feedback;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Speakr.WebApp.Site.Tests.Areas.Feedback
{
    public class WhenSubmittingFeedbackForm
    {
        [Test]
        public void AndFeedbackIsPosted_ThenShouldRedirectSuccessfully()
        {
            var model = CreateFeedbackViewModelStub();
            var controller = new FeedbackController(new FeedbackFormService(new TalksApi()));

            var actionResult = (ViewResult)controller.Index(model).Result;

            Assert.That(actionResult.ViewName, Is.EqualTo("_feedbackSavedSuccessfully"));
        }

        //[Test]
        //[Ignore("MVC Validation via postback not implemented yet")]
        //public void AndFeedbackIsPosted_ThenViewModelIsValidatedCorrectly()
        //{
        //    var model = CreateFeedbackViewModelStub();
        //    var expectedMessage = "Please provide an answer to this question";

        //    var controller = new FeedbackController(new FeedbackFormService(new TalksApi()));

        //    var validationErrors = CheckForValidationErrors(controller, model);

        //    var actionResult = (ViewResult)controller.Index(model).Result;
        //    var modelState = controller.ModelState;

        //    Assert.That(validationErrors.Count, Is.EqualTo(1));
        //    Assert.That(validationErrors[0].ErrorMessage, Is.EqualTo(expectedMessage));

        //    Assert.That(actionResult.ViewName, Is.EqualTo("Index"));
        //}

        //[Test]
        //[Ignore("Need mocking framework")]
        //public void AndFeedbackIsPosted_ThenResponseMappedCorrectly()
        //{
        //    var model = new FeedbackViewModel();
        //    var expectedDTO = new FeedbackResponse();

        //    // Mock talksapi. Should have been called with expected DTO
        //    var controller = new FeedbackController(new FeedbackFormService(new TalksApi()));

        //    var validationErrors = CheckForValidationErrors(controller, model);

        //    var actionResult = (ViewResult)controller.Index(model).Result;
        //    var modelState = controller.ModelState;

        //    //Assert.That(TalksApi, WasCalledWithParam(expectedDTO));

        //    Assert.That(actionResult.ViewName, Is.EqualTo("Index"));
        //}

        private static FeedbackViewModel CreateFeedbackViewModelStub()
        {
            var temp = TalksApiStubResponse.GetTalkById("12345");
            var viewModel = new FeedbackViewModel();
            viewModel.TalkId = temp.TalkId;
            viewModel.TalkName = temp.TalkName;
            viewModel.SpeakerId = temp.SpeakerId;
            viewModel.SpeakerName = temp.SpeakerName;

            viewModel.Questionnaire = temp.Questionnaire.Select(x => new QuestionViewModel
            {
                QuestionId = x.QuestionId,
                IsRequired = x.IsRequired,
                QuestionText = x.QuestionText,
                ResponseType = x.ResponseType,
                Answer = x.Answer
            }).ToList();

            return viewModel;
        }

        private static IList<ValidationResult> CheckForValidationErrors(FeedbackController controller, FeedbackViewModel model)
        {
            var validationErrors = ViewModelValidation.Validate(model);

            if (validationErrors.Count > 0)
            {
                controller.ViewData.ModelState.AddModelError(nameof(model.TalkId), validationErrors[0].ErrorMessage);
            }

            return validationErrors;
        }
    }
}
