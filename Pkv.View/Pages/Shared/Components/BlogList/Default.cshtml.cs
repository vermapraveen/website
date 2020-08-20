
using Microsoft.AspNetCore.Mvc;
using System;

namespace Pkv.View.Components
{
    public class BlogListViewComponent : ViewComponent
    {
        public BlogListViewComponent()
        {

        }
        public IViewComponentResult Invoke()
        {
            Console.Out.WriteLine($"Components/BlogListViewComponent/Invoke/");

            //var blogUrl = FileReader.GetFileUrl();
            return View();
        }
    }
}
