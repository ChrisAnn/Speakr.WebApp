//using Speakr.WebApp.Site.Clients.TalksApi;
//using Speakr.WebApp.Site.Clients.TalksApi.DTO;
//using Speakr.WebApp.Site.Services.Feedback;
//using Speakr.WebApp.Site.ViewModels.Feedback;
//using System;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Speakr.WebApp.Site.Services.ReviewForm
//{
//    public class FeedbackFormService : IFeedbackFormService
//    {
//        private ITalksApi _talksApiClient;

//        public FeedbackFormService(ITalksApi talksApiClient)
//        {
//            _talksApiClient = talksApiClient;
//        }

//        public async Task<FeedbackFormViewModel> GetReviewFormForTalkId(string talkId)
//        {
//            var talk = await _talksApiClient.GetFeedbackFormByEasyAccessKey(talkId);
//            var viewModel = MapToViewModel(talk);

//            return viewModel;
//        }

//        public async Task PostReviewForm(FeedbackFormViewModel submittedForm)
//        {
//            var response = MapFeedbackViewModelToRevieForm(submittedForm);

//            await _talksApiClient.PostFeedbackForm(submittedForm.TalkId, response);
//        }

//        private FeedbackFormViewModel MapToViewModel(FeedbackForm talk)
//        {
//            var viewModel = new FeedbackFormViewModel();
//            viewModel.TalkId = talk.TalkId;
//            viewModel.TalkName = talk.TalkName;
//            viewModel.SpeakerName = talk.SpeakerName;

//            viewModel.Questionnaire = talk.Questionnaire.Select(x => new QuestionViewModel
//            {
//                QuestionId = x.QuestionId,
//                IsRequired = x.IsRequired,
//                QuestionText = x.QuestionText,
//                AnswerType = x.AnswerType,
//                Answer = x.Answer
//            }).ToList();

//            return viewModel;
//        }

//        private FeedbackResponse MapFeedbackViewModelToRevieForm(FeedbackFormViewModel submittedForm)
//        {
//            return new FeedbackResponse
//            {
//                TalkId = submittedForm.TalkId,
//                ReviewerId = "",
//                Questionnaire = submittedForm.Questionnaire.Select(x => new Question
//                {
//                    QuestionId = x.QuestionId,
//                    IsRequired = x.IsRequired,
//                    QuestionText = x.QuestionText,
//                    AnswerType = x.AnswerType,
//                    Answer = x.Answer
//                }).ToList(),
//                SubmissionTime = DateTime.Now
//            };
//        }
//    }
//}
