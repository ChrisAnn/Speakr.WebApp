namespace Speakr.WebApp.Site.Clients.TalksApi.DTO
{
    public class Question
    {
        public string QuestionId { get; set; }
        public bool IsRequired { get; set; }
        public string QuestionText { get; set; }
        public AnswerTypes AnswerType { get; set; }
        public string Answer { get; set; }
    }
}
