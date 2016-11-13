using Speakr.WebApp.Site.Clients.TalksApi.DTO;
using System.Diagnostics;
using System.Net.Http;

namespace Speakr.WebApp.Site.Clients.TalksApi
{
    public class TalksApi : RestClient, ITalksApi
    {
        public TalksApi(string baseUrl) : base(baseUrl)
        {
            Debug.WriteLine("Created TalksApi Http Client");
        }

        public void PostTalk(Talk requestBody)
        {
            // POST ~/talks
        }

        public Talk GetTalkById(int talkId)
        {
            return Get<Talk>($"talks?talkid={talkId}");
        }

        public FeedbackForm GetFeedbackFormByEasyAccessKey(string easyAccessKey)
        {
            return Get<FeedbackForm>($"talks/{easyAccessKey}/FeedbackForm");
        }

        public HttpResponseMessage PostFeedbackForm(string easyAccessKey, FeedbackResponse response)
        {
            return Post($"talks/{easyAccessKey}/Reviews", response);
        }
    }
}
