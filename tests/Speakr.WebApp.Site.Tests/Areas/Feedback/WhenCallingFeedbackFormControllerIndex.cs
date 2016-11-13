using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Speakr.WebApp.Site.Clients.TalksApi;
using Speakr.WebApp.Site.Clients.TalksApi.DTO;
using Speakr.WebApp.Site.Controllers;
using Speakr.WebApp.Site.ViewModels.Feedback;
using System.Collections.Generic;

namespace Speakr.WebApp.Site.Tests.Areas.Feedback
{
    [TestFixture]
    public class WhenCallingFeedbackFormControllerIndex
    {
        private ITalksApi _talksApi;

        [SetUp]
        public void Setup()
        {
            _talksApi = A.Fake<ITalksApi>();
        }

        [Test]
        public void AndTalkIsFound_ThenIndexShouldReturnView()
        {
            A.CallTo(() => _talksApi.GetFeedbackFormByEasyAccessKey("12345"))
                .Returns(new FeedbackForm() {
                    Questionnaire = new List<Question>()
                        {
                            new Question()
                        }
                });

            var controller = new FeedbackController(_talksApi);
            var actionResult = controller.Index("12345");
            var viewResult = (ViewResult)actionResult;

            Assert.That(viewResult, Is.Not.Null);
            Assert.That(viewResult.ViewName, Is.EqualTo("Index"));
            Assert.That(viewResult.Model, Is.TypeOf<FeedbackFormViewModel>());
        }


        [Test]
        public void AndTalkFoundButQuestionnaireIsEmpty_ThenIndexRedirectsToTalkNotFound()
        {
            A.CallTo(() => _talksApi.GetFeedbackFormByEasyAccessKey("12345"))
                .Returns(new FeedbackForm());

            var controller = new FeedbackController(_talksApi);
            var actionResult = controller.Index("12345");
            var viewResult = (RedirectToActionResult)actionResult;

            Assert.That(viewResult, Is.Not.Null);
            Assert.That(viewResult.ActionName, Is.EqualTo("TalkNotFound"));
            Assert.That(viewResult.ControllerName, Is.EqualTo("Home"));
            Assert.That(viewResult.RouteValues["EasyAccessKey"], Is.EqualTo("12345"));
        }

        [Test]
        public void AndTalkIsNotFound_ThenIndexRedirectsToTalkNotFound()
        {
            var notFoundKey = "not the key you are looking for";

            A.CallTo(() => _talksApi.GetFeedbackFormByEasyAccessKey(notFoundKey))
                .Returns(null);

            var controller = new FeedbackController(_talksApi);
            var actionResult = controller.Index(notFoundKey);
            var viewResult = (RedirectToActionResult)actionResult;

            Assert.That(viewResult, Is.Not.Null);
            Assert.That(viewResult.ActionName, Is.EqualTo("TalkNotFound"));
            Assert.That(viewResult.ControllerName, Is.EqualTo("Home"));
            Assert.That(viewResult.RouteValues["EasyAccessKey"], Is.EqualTo(notFoundKey));
        }
    }
}
