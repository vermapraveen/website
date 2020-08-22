
using Microsoft.AspNetCore.Mvc;

using Pkv.View.Pages.Shared.ViewModels;

namespace Pkv.View.Components
{
    public class BlogLinkViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string uniqueName)
        {
            return View("Default", new ContextViewModel { BlogUniqueName = uniqueName });
        }
    }
}
