using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Speakr.WebApp.Site.Controllers.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        public FooterViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("Footer");
        }
    }
}
