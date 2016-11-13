using Speakr.WebApp.Site.Clients.TalksApi.DTO;
using System.Net.Http;

namespace Speakr.WebApp.Site.Clients.TalksApi
{
    public interface ITalksApi
    {
        FeedbackForm GetFeedbackFormByEasyAccessKey(string talkId);
        HttpResponseMessage PostFeedbackForm(string easyAccessKey, FeedbackResponse response);
    }
}