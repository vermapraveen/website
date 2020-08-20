
using Microsoft.AspNetCore.Mvc;
using Pkv.View.Pages.Shared.ViewModels;
using System;
using System.Threading.Tasks;

namespace Pkv.View.Components
{
    public class BlogLinkViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string uniqueName)
        {
            Console.Out.WriteLine($"Components/BlogLinkViewComponent/Invoke/{uniqueName}");
            return View("Default", new ContextViewModel { BlogUniqueName = uniqueName });
        }
    }
}
