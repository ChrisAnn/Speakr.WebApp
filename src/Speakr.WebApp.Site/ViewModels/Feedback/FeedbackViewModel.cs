using System.Collections.Generic;

namespace Speakr.WebApp.Site.ViewModels.Feedback
{
    public class FeedbackFormViewModel
    {
        public int TalkId { get; set; }
        public string EasyAccessKey { get; set; }
        public string TalkName { get; set; }
        public string Description { get; set; }
        public string SpeakerName { get; set; }
        public IList<QuestionViewModel> Questionnaire { get; set; }

        public string ErrorMessage { get; set; }

        public bool HasErrors { get { return !string.IsNullOrEmpty(ErrorMessage); } }
    }
}
