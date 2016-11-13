using Speakr.WebApp.Site.Clients.TalksApi.DTO;
using System.Collections.Generic;

namespace Speakr.WebApp.Site.Clients.TalksApi
{
    public static class TalksApiStubResponse
    {
        public static FeedbackForm GetTalkByEasyAccessKey(string easyAccessKey)
        {
            var questionList = new List<Question>
            {
                new Question
                {
                    QuestionId = "Question-1",
                    QuestionText = "How much did you enjoy the talk?",
                    Answer = "",
                    AnswerType = AnswerTypes.Emoji,
                    IsRequired = true
                },

                new Question
                {
                    QuestionId = "Question-2",
                    QuestionText = "How would you rate this talk?",
                    Answer = "",
                    AnswerType = AnswerTypes.Rating,
                    IsRequired = false
                },

                new Question
                {
                    QuestionId = "Question-3",
                    QuestionText = "Did you learn anything useful?",
                    Answer = "",
                    AnswerType = AnswerTypes.YesNo,
                    IsRequired = true
                },

                new Question
                {
                    QuestionId = "Question-4",
                    QuestionText = "Would you recommend this talk to a friend/colleague?",
                    Answer = "",
                    AnswerType = AnswerTypes.YesNo,
                    IsRequired = false
                },

                new Question
                {
                    QuestionId = "Question-5",
                    QuestionText = "Do you have any suggestions to improve this talk?",
                    Answer = "",
                    AnswerType = AnswerTypes.Text,
                    IsRequired = true
                },

                new Question
                {
                    QuestionId = "Question-6",
                    QuestionText = "Any other comments?",
                    Answer = "",
                    AnswerType = AnswerTypes.Text,
                    IsRequired = false
                }
            };

            return new FeedbackForm()
            {
                TalkId = 99999999,
                EasyAccessKey = easyAccessKey,
                TalkName = "My First Talk",
                SpeakerName = "J-Wow",
                Questionnaire = questionList
            };
        }
    }
}
