
using Microsoft.AspNetCore.Mvc;

using Pkv.View.Models;

using System;

namespace Pkv.View.Components
{
    public class BlogIntroInPageViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(BlogIntroModel blogIntroModel)
        {
            Console.Out.WriteLine($"Components/BlogIntroInPageViewComponent/Invoke/");
            return View(blogIntroModel);
        }
    }
}
