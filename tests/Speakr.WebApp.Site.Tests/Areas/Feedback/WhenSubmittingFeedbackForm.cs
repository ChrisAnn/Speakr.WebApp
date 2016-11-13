using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Speakr.WebApp.Site.Clients.TalksApi;
using Speakr.WebApp.Site.Controllers;
using Speakr.WebApp.Site.Tests.Helpers;
using Speakr.WebApp.Site.ViewModels.Feedback;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System;
using Speakr.WebApp.Site.Clients.TalksApi.DTO;
using System.Net.Http;
using System.Net;

namespace Speakr.WebApp.Site.Tests.Areas.Feedback
{
    public class WhenSubmittingFeedbackForm
    {
        private ITalksApi _talksApi;
        private DateTime _expectedDateTime;
        private FeedbackFormViewModel _viewModel;
        private FeedbackResponse _expectedResponse;
        private string _easyAccessKey;

        [SetUp]
        public void Setup()
        {
            _talksApi = A.Fake<ITalksApi>();
            _expectedDateTime = DateTime.Now;
            _easyAccessKey = "talk_key";
            _viewModel = CreateFeedbackViewModelStub(_easyAccessKey);
            _expectedResponse = CreateFeedbackViewModelResponse(_viewModel);
        }

        [Test]
        public void AndFeedbackIsSuccessfullyPosted_ThenShouldRedirectSuccessfully()
        {
            A.CallTo(() => _talksApi.PostFeedbackForm(
                    _easyAccessKey,
                    A<FeedbackResponse>.Ignored))
             .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.Created});

            var controller = new FeedbackController(_talksApi);

            var viewResult = (ViewResult)controller.Index(_viewModel);

            Assert.That(viewResult, Is.Not.Null);
            Assert.That(viewResult.ViewName, Is.EqualTo("_feedbackSavedSuccessfully"));
            A.CallTo(() 
                => _talksApi.PostFeedbackForm(
                    _easyAccessKey, 
                    A<FeedbackResponse>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void AndTalksApiCannotSaveReview_ThenShouldRedirectWithError()
        {
            A.CallTo(() => _talksApi.PostFeedbackForm(
                    _easyAccessKey,
                    A<FeedbackResponse>.Ignored))
             .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.Conflict, ReasonPhrase = "Couldn't Find Talk" });

            var controller = new FeedbackController(_talksApi);

            var viewResult = (RedirectToActionResult)controller.Index(_viewModel);
            var errorMessage = (string)viewResult.RouteValues["ErrorMessage"];

            Assert.That(viewResult, Is.Not.Null);
            Assert.That(viewResult.ActionName, Is.EqualTo("Index"));
            Assert.That(viewResult.ControllerName, Is.EqualTo("Feedback"));
            Assert.That(errorMessage, Is.EqualTo("Couldn't Find Talk"));
        }

        [Test]
        [Ignore("Checking on Postback not implemented yet")]
        public void AndFeedbackIsPosted_ThenViewModelIsValidatedCorrectly()
        {
            var expectedMessage = "Please provide an answer to this question";

            var controller = new FeedbackController(_talksApi);

            var validationErrors = CheckForValidationErrors(controller, _viewModel);

            var actionResult = (ViewResult)controller.Index(_viewModel);
            var modelState = controller.ModelState;

            Assert.That(validationErrors.Count, Is.EqualTo(1));
            Assert.That(validationErrors[0].ErrorMessage, Is.EqualTo(expectedMessage));

            Assert.That(actionResult.ViewName, Is.EqualTo("Index"));
        }

        private static FeedbackFormViewModel CreateFeedbackViewModelStub(string _easyAccessKey)
        {
            var temp = TalksApiStubResponse.GetTalkByEasyAccessKey("12345");
            var viewModel = new FeedbackFormViewModel();
            viewModel.TalkId = temp.TalkId;
            viewModel.TalkName = temp.TalkName;
            viewModel.SpeakerName = temp.SpeakerName;
            viewModel.EasyAccessKey = _easyAccessKey;

            viewModel.Questionnaire = temp.Questionnaire.Select(x => new QuestionViewModel
            {
                QuestionId = x.QuestionId,
                IsRequired = x.IsRequired,
                QuestionText = x.QuestionText,
                AnswerType = x.AnswerType,
                Answer = x.Answer
            }).ToList();

            return viewModel;
        }

        private FeedbackResponse CreateFeedbackViewModelResponse(FeedbackFormViewModel model)
        {
            var response = new FeedbackResponse
            {
                TalkId = model.TalkId,
                ReviewerId = "",
                Questionnaire = model.Questionnaire.Select(x => new Question
                {
                    QuestionId = x.QuestionId,
                    IsRequired = x.IsRequired,
                    QuestionText = x.QuestionText,
                    AnswerType = x.AnswerType,
                    Answer = x.Answer
                }).ToList(),
                SubmissionTime = _expectedDateTime
            };

            return response;
        }

        private static IList<ValidationResult> CheckForValidationErrors(FeedbackController controller, FeedbackFormViewModel model)
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
