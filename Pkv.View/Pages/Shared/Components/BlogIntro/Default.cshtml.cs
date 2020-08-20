
using Microsoft.AspNetCore.Mvc;

using Pkv.View.Models;

using System;

namespace Pkv.View.Components
{
    public class BlogIntroViewComponent : ViewComponent
    {
        public BlogIntroViewComponent()
        {

        }
        public IViewComponentResult Invoke(BlogIntroModel blogIntroModel)
        {
            Console.Out.WriteLine($"Components/BlogIntroViewComponent/Invoke/");
            return View(blogIntroModel);
        }
    }
}
