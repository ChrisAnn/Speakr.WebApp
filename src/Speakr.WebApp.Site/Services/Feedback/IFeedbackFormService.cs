using Speakr.WebApp.Site.ViewModels.Feedback;
using System.Threading.Tasks;

namespace Speakr.WebApp.Site.Services.Feedback
{
    public interface IFeedbackFormService
    {
        Task<FeedbackFormViewModel> GetReviewFormForTalkId(string talkId);
        Task PostReviewForm(FeedbackFormViewModel model);
    }
}