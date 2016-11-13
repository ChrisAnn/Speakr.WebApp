using System;
using System.Collections.Generic;

namespace Speakr.WebApp.Site.Clients.TalksApi.DTO
{
    public class FeedbackResponse
    {
        public int TalkId { get; set; }
        public string ReviewerId { get; set; }

        public IList<Question> Questionnaire { get; set; }

        public DateTime SubmissionTime { get; set; }
    }
}
