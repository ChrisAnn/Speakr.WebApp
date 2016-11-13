using Speakr.WebApp.Site.Clients.TalksApi.DTO;
using System.Collections.Generic;

namespace Speakr.WebApp.Site.ViewModels.Feedback
{
    public class QuestionViewModel
    {
        public string QuestionId { get; set; }
        public bool IsRequired { get; set; }
        public string QuestionText { get; set; }
        public AnswerTypes AnswerType { get; set; }
        public string Answer { get; set; }

        public class Option
        {
            public string Value { get; set; }
            public bool Selected { get; set; }
            public string Label { get; set; }
        }

        private Option MakeOption(string value, string label, string currentValue)
        {
            return (new Option
            {
                Value = value,
                Label = label,
                Selected = currentValue == value
            });
        }


        public IEnumerable<Option> EmojiOptions
        {
            get
            {
                yield return (MakeOption("nope", "I was falling asleep...", this.Answer));
                yield return (MakeOption("meh", "It was OK, I guess", this.Answer));
                yield return (MakeOption("ok", "I liked it.", this.Answer));
                yield return (MakeOption("wow", "I loved it", this.Answer));
            }
        }
    }
}
