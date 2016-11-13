using System.ComponentModel.DataAnnotations;

namespace Speakr.WebApp.Site.ViewModels.Home
{
    public class GetFeedbackFormViewModel
    {
        [MaxLength(24)]
        [Display(Name = "Talk Id:")]
        [Required(ErrorMessage = "Please enter your talk's ID")]
        [MinLength(4, ErrorMessage = "Talk ID's have at least 4 characters")]
        [RegularExpression(@"^[a-zA-Z0-9_.]+$", ErrorMessage = "There are invalid characters on your TalkId")]
        public string EasyAccessKey { get; set; }

        public string EasyAccessKeyErrorMessage { get; set; }

        public bool HasErrors { get { return !string.IsNullOrEmpty(EasyAccessKeyErrorMessage); } }
    }
}
