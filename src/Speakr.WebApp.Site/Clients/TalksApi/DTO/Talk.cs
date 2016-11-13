using System;

namespace Speakr.WebApp.Site.Clients.TalksApi.DTO
{
    public class Talk
    {
        public int Id { get; set; }
        public string EasyAccessKey { get; set; }
        public string Name { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public string SpeakerName { get; set; }
        public DateTime TalkCreationTime { get; set; }
        public DateTime TalkStartTime { get; set; }
        public int QuestionnaireId { get; set; }
    }
}
